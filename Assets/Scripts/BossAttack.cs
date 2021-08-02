using System.Collections;
using System.Collections.Generic;
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
    Vector3 dir;

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
    public void Attack()
    { 
        transform.position = transform.parent.position + new Vector3(0, 2, 0);
        gameObject.SetActive(true);
        
        dir = character.Player.position + new Vector3(0, 2, 0);
        StartCoroutine("SkillShot");
        
    }

    IEnumerator SkillShot()
    {
        while (true)
        {
            transform.LookAt(dir);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if((transform.position- dir).magnitude < 0.5f)
            {
                StopCoroutine("SkillShot");
                gameObject.SetActive(false);
                OnHitEffect(dir);
                bossAI.BossAni.Play("Idle");
                break;
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
        if(other.gameObject.tag =="Ground" && other.gameObject.tag == "Skill")
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
