using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CharacterMove : MonoBehaviour, IPointerDownHandler
{
    Animator PlayerAni;

    [Header("Camera")]
    public Camera mycamera;
    public GameObject touchEffect;
    RaycastHit hit;

    [Header("Player")]
    public Transform Player;
    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;
    public float Range;
    public NavMeshAgent PlayerNav;
    public bool Talking;

    [Header("Targeting")]
    public Transform target;
    public GameObject target_Tool;
    public TextMeshProUGUI name_Target;
    public GameObject hpBar_Target;
    public GameObject DropBox;
    public GameObject Inventory;
    public bool ClickNPC;
    float targetDis;

    [Header("Casting")]
    public bool onCasting;
    public Image castingBar;
    float castingTime;
    string SkillName;
    public Transform atkTarget;
    GameObject skillObj;

    [Header("Player_UI")]
    public Image player_hpbar;
    public Image player_mpbar;

    [Header("PlayerInfoUpate")]
    PlayerState playerState;

    [Header("QuestCount")]
    public int Skeleton;

    private void Awake()
    {
        PlayerAni = Player.GetComponent<Animator>();
        PlayerNav = Player.GetComponent<NavMeshAgent>();
        playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
        Talking = false;
    }

    void Start()
    {
        PlayerNav.speed = moveSpeed;
        PlayerNav.angularSpeed = rotateSpeed;
        Skeleton = 0;
    }
    private void FixedUpdate()
    {
        PlayerAni.SetBool("Walk", PlayerNav.velocity != Vector3.zero);

        OnTarget();
    }
    public void GetPlayerExp()
    {
        playerState.exp_Cur += target.gameObject.GetComponent<EnemyState>().exp;

        if (playerState.exp_Cur >= playerState.exp_Max)
        {
            playerState.Lv++;
            playerState.exp_Cur -= playerState.exp_Max;
            playerState.LevelUp();
        }
        //퀘스트 중 목표 얻기.
        if (target.GetComponent<Obj_Info>().Obj_Name == "Skeleton" && Manager.instance.questManager.questList.ContainsValue("Get Skeleton Born"))
        {
            Skeleton++;
            for (int i = 0; i < Manager.instance.questManager.questList.Count; i++)
            {
                if (Manager.instance.questManager.QuestBox.GetChild(i).GetChild(0).GetComponent<Text>().text.Contains("Get Skeleton Born"))
                {
                    Manager.instance.questManager.QuestBox.GetChild(i).GetChild(1).GetComponent<Text>().text = Skeleton.ToString();

                }
            }
        }
    }
    //플레이어 상태창 동기화 (HP,MP)
    public void PlayerUI()
    {
        PlayerState playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
        player_hpbar.fillAmount = playerState.hp_Cur / playerState.hp;
        player_mpbar.fillAmount = playerState.Mp_Cur / playerState.Mp;
    }
    //클릭 이벤트 생성 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) || Input.touchCount == 1)

        {
            Ray ray = mycamera.ScreenPointToRay(eventData.position);
            Physics.Raycast(ray, out hit);

            if (hit.transform != null)
            {
                touchEffect.SetActive(false);
                touchEffect.transform.position = mycamera.WorldToScreenPoint(hit.point);
                touchEffect.SetActive(true);

                if (hit.transform.gameObject.tag == "Ground")
                {
                    if (!onCasting)
                    {
                        PlayerNav.SetDestination(hit.point);
                    }
                    DropBox.SetActive(false);
                    if(target_Tool.activeSelf)
                    {
                        target_Tool.SetActive(false);
                    }
                }
                if (hit.transform.gameObject.tag == "Player")
                {
                    Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
                    Manager.instance.inventory.charInfoFrame.SetActive(true);
                }

                if (hit.transform.gameObject.tag == "Enemy")
                {
                    Targeting();
                }
                if (hit.transform.gameObject.tag == "Dead")
                {
                    target = hit.transform;
                    DropBox.transform.position = mycamera.WorldToScreenPoint(hit.point);
                    DropBox.SetActive(true);

                }
                if (hit.transform.gameObject.tag == "Npc")
                {
                    Targeting();

                    if (!onCasting)
                    {
                        target.GetComponent<NPC_Dialog>().Dialog();
                        Manager.instance.manager_Dialog.dialog_Frame.SetActive(true);
                    }
                }


            }
        }
    }
    //캐스팅 실행
    public void Casting(float time, string name, GameObject Obj)
    {
        castingTime = time;
        SkillName = name;
        atkTarget = target;
        skillObj = Obj;

        StartCoroutine("OnCasting");
    }
    //캐스팅 실행 중
    IEnumerator OnCasting()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.Casting);
        onCasting = true;
        float time = 0;
        Player.LookAt(atkTarget);
        PlayerAni.Play("Player_Casting");
        PlayerNav.enabled = false;
        castingBar.transform.parent.gameObject.SetActive(true);
        castingBar.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SkillName;

        while (true)
        {
            time += Time.deltaTime;
            castingBar.fillAmount = time / castingTime;

            if (time >= castingTime)
            {
                Manager.instance.myaudio.audioSource.Stop();
                StopCoroutine("OnCasting");
                castingBar.transform.parent.gameObject.SetActive(false);
                PlayerAni.Play("Player_Shot");
                PlayerNav.enabled = true;
                onCasting = false;

                skillObj.transform.position = Player.position + new Vector3(0, 2, 0);
                skillObj.SetActive(true);
            }
            yield return null;
        }
    }
    // 타켓팅 설정 함수
    void Targeting()
    {
        target = hit.transform;
        name_Target.text = target.GetComponent<Obj_Info>().Obj_Name;
        if (target.tag == "Enemy")
        {
            hpBar_Target.SetActive(target.GetComponent<Obj_Info>().type == "Enemy");
            hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = target.GetComponent<EnemyState>().cur_Hp / target.GetComponent<EnemyState>().hp;
        }
        else if (target.tag == "Npc")
        {
            hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = 100f;
        }
        target_Tool.SetActive(true);
    }
    //타켓 체크 함수
    void OnTarget()
    {
        if (target != null)
        {
            targetDis = (target.position - Player.position).magnitude;

            if (targetDis > Range)
            {
                target = null;
                target_Tool.SetActive(false);
            }

            if (target_Tool.activeSelf)
            {
                target_Tool.transform.position = mycamera.WorldToScreenPoint(target.position + new Vector3(0, 1, 0));
            }
        }
    }

    //버튼 입력 함수 2개
    public void OpenInventory()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        if (Manager.instance.inventory.InvenFrame.activeSelf)
        {
            Manager.instance.inventory.InvenFrame.SetActive(false);
        }
        else
        {
            Manager.instance.inventory.InvenFrame.SetActive(true);
        }
    }
    public void CloseInventory()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
        Manager.instance.inventory.InvenFrame.SetActive(false);
    }
    public void OpenInfo()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
        if (Manager.instance.inventory.charInfoFrame.activeSelf)
        {
            Manager.instance.inventory.charInfoFrame.SetActive(false);
        }
        else
        {
            Manager.instance.inventory.charInfoFrame.SetActive(true);
        }
    }

}
