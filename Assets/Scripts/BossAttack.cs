using System.Collections;
using TMPro;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Boss")]
    BossAI bossAI;
    EnemyState BossState;
    [Header("AttackObject")]
    public GameObject Attack_Object;
    public GameObject hitEffect;
    GameObject dmgText;
    public Vector3 dir;

    [Header("Player")]
    CharacterMove character;
    PlayerState playerState;
    float speed;

    int dmg;
    float dmgRange;
    private void Awake()
    {
        BossState = transform.parent.GetComponent<EnemyState>();
        character = Manager.instance.characterMove;
        playerState = character.Player.GetComponent<PlayerState>();
        dmgText = Manager.instance.uI_Manager.Boss_Text;
        bossAI = transform.parent.GetComponent<BossAI>();

    }
    private void Start()
    {
        speed = 5;
    }
    private void OnEnable()
    {
        StartCoroutine(SkillShot(dir));
    }
    public IEnumerator SkillShot(Vector3 Dir)
    {
        while (true)
        {
            transform.LookAt(Dir);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
          if ((transform.position - Dir).magnitude < 0.5f && !bossAI.PatternOn)
          {
              StopCoroutine("SkillShot");
              gameObject.SetActive(false);
              OnHitEffect(Dir);
              bossAI.BossAni.Play("Idle");
              break;
          }
         else if (Mathf.Abs((transform.position - transform.parent.position).magnitude) > 10)
         {
             StopCoroutine("SkillShot");
             gameObject.SetActive(false);
             bossAI.BossAni.Play("Idle");
            }
            yield return null;
        }
    }

    void CalculateDmg()
    {
        dmgRange = Random.Range(0.8f, 1.2f);

        int critical = Random.Range(0, 100);
        if (critical < BossState.cri * 100)
        {
            dmgRange = Random.Range(2f, 2.5f);
        }

        dmg = (int)(BossState.atk * dmgRange);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine("SkillShot");
            gameObject.SetActive(false);

            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;
            OnHitEffect(hitPoint);

            CalculateDmg();

            dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
            dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
            dmgText.transform.position = Manager.instance.characterMove.mycamera.WorldToScreenPoint(hitPoint + new Vector3(0, 1, 0));
            dmgText.SetActive(true);

            playerState.hp_Cur -= dmg;
            character.PlayerUI();
        }
       if (other.gameObject.tag == "Ground")
       {
           StopCoroutine("SkillShot");
           gameObject.SetActive(false);
       
           Vector3 hitPoint = (other.transform.position) * 0.5f;
           OnHitEffect(hitPoint);
       }

        bossAI.BossAni.Play("Idle");
    }

    void OnHitEffect(Vector3 hitPoint)
    {
        hitEffect.transform.position = hitPoint + new Vector3(0, 1, 0);
        hitEffect.SetActive(true);

    }


}
