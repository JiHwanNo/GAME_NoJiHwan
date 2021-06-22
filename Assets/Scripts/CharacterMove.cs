using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using TMPro;
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
    NavMeshAgent PlayerNav;

    [Header("Targeting")]
    public Transform target;
    public GameObject target_Tool;
    public TextMeshProUGUI name_Target;
    public GameObject hpBar_Target;

    [Header("Casting")]
    public bool onCasting;
    public Image castingBar;
    float castingTime;
    string SkillName;
    public Transform atkTarget;
    GameObject skillObj;
    
    private void Awake()
    {
        PlayerAni = Player.GetComponent<Animator>();
        PlayerNav = Player.GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        PlayerNav.speed = moveSpeed;
        PlayerNav.angularSpeed = rotateSpeed;
    }
    private void FixedUpdate()
    {
       PlayerAni.SetBool("Walk", PlayerNav.velocity != Vector3.zero);

        OnTarget();
      
    }
    //Ŭ�� �̺�Ʈ ���� 
    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0))
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
                    if(!onCasting)
                    PlayerNav.SetDestination(hit.point);
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
            }
        }
      
       
    }

    public void Casting(float time, string name, GameObject Obj)
    {
        castingTime = time;
        SkillName = name;
        atkTarget = target;
        skillObj = Obj;

        StartCoroutine("OnCasting");
    }

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

        while(true)
        {
            time += Time.deltaTime;
            castingBar.fillAmount = time / castingTime;

            if(time >= castingTime)
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
    // Ÿ���� ���� �Լ�
    void Targeting()
    {
        target = hit.transform;
        name_Target.text = target.GetComponent<Obj_Info>().Obj_Name;
        hpBar_Target.SetActive(target.GetComponent<Obj_Info>().type == "Enemy");
        target_Tool.SetActive(true);
    }
    //Ÿ�� üũ �Լ�
    void OnTarget()
    {
        if(target != null)
        {
            float targetDis = (target.position - Player.position).magnitude;

            if(targetDis > Range)
            {
                target = null;
                target_Tool.SetActive(false);
            }

            if(target_Tool.activeSelf)
            {
                target_Tool.transform.position = mycamera.WorldToScreenPoint(target.position+new Vector3(0,1,0));
            }
        }
    }

    //��ư �Է� �Լ� 2��
    public void OpenInventory()
    {
        Manager.instance.inventory.InvenFrame.SetActive(true);
    }
    public void CloseInventory()
    {
        Manager.instance.inventory.InvenFrame.SetActive(false);
    }



}
