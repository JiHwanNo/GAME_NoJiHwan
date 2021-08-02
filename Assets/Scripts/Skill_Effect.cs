using System.Collections;
using TMPro;
using UnityEngine;

public class Skill_Effect : MonoBehaviour
{
    Transform target;
    public float speed;
    public GameObject hitEffect;
    public int index;
    float time = 0;
    int sheildNum;
    public GameObject dmgText;
    public float skillPower;
    int dmg;
    float dmgRange;

    CharacterMove character;
    Audio_Manager myaudio;
    PlayerState playerState;
    private void Awake()
    {
        sheildNum = 0;
        character = Manager.instance.characterMove;
        myaudio = Manager.instance.myaudio;
        playerState = character.Player.GetComponent<PlayerState>();
    }
    private void OnEnable()
    {
        target = character.atkTarget;

        if (index == 2)
        {
            skillPower = 2f;
            myaudio.audioSource.PlayOneShot(myaudio.Heal);
            StartCoroutine("HealSkill");

            CalculateDmg();
            Vector3 hitPoint = (transform.position) * 0.5f;
            dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
            dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
            dmgText.transform.position = character.mycamera.WorldToScreenPoint(Manager.instance.characterMove.Player.transform.position + new Vector3(0, 1, 0));
            dmgText.SetActive(true);

            if (playerState.hp_Cur < playerState.hp)
            {
                if (playerState.hp_Cur + dmg < playerState.hp)
                {
                    playerState.hp_Cur += dmg;
                }
                else
                {
                    playerState.hp_Cur = playerState.hp;
                }
            }
            playerState.Mp_Cur -= 15;

        }
        else if (index == 4)
        {
            myaudio.audioSource.PlayOneShot(myaudio.Heal);

            Vector3 hitPoint = (transform.position) * 0.5f;
        }
        else if (index == 0)
        {
            skillPower = 1.25f;
            myaudio.audioSource.PlayOneShot(myaudio.FireSkill_Flying);
            StartCoroutine("SkillShot");
            playerState.Mp_Cur -= 5;


        }
        else if (index == 1)
        {
            skillPower = 2f;
            myaudio.audioSource.PlayOneShot(myaudio.AquaSkill_Flying);
            StartCoroutine("SkillShot");
            playerState.Mp_Cur -= 20;
        }
        else if(index ==3)
        {
            playerState.Mp_Cur -= 30;
        }

        character.PlayerUI();
    }
    IEnumerator SkillShot()
    {
        while (true)
        {
            Vector3 dir = target.position + new Vector3(0, 2, 0);
            transform.LookAt(dir);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator HealSkill()
    {
        while (true)
        {
            time += Time.fixedDeltaTime * speed;
            if (time >= 6f)
            {
                time = 0;
                StopCoroutine("HealSkill");
                gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    void CalculateDmg()
    {
        PlayerState playerState = character.Player.GetComponent<PlayerState>();

        dmgRange = Random.Range(0.8f, 1.2f);
        dmg = (int)(playerState.atk * skillPower * dmgRange);

        int critical = Random.Range(0, 100);
        if (critical < playerState.cri * 100)
        {
            dmgRange = Random.Range(2f, 2.5f);
        }
    }
    void OnHitEffect(Vector3 hitPoint)
    {
        hitEffect.transform.position = hitPoint + new Vector3(0, 1, 0);
        hitEffect.SetActive(true);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
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

            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<EnemyHit>().Hit(dmg);
            }
            if (other.gameObject.tag == "Boss")
            {
                other.GetComponent<BossHit>().Hit(dmg);
            }
        }
    }
    public void StartSheild()
    {
        if (sheildNum == 0)
        {
            StartCoroutine("Sheild");
            character.PlayerAni.Play("Player_Casting");
            character.onCasting = true;
            sheildNum = 1;
        }
        else if (sheildNum == 1)
        {
            StopSheild();
            sheildNum = 0;
        }
    }
    public void StopSheild()
    {
        StopCoroutine("Sheild");
        character.PlayerAni.Play("Idle");
        gameObject.SetActive(false);
        character.onCasting = false;

    }
    IEnumerator Sheild()
    {
        float currentHp = playerState.hp_Cur;
        while (true)
        {
            transform.position = character.Player.position;
            time += Time.fixedDeltaTime * speed;
            if (currentHp > playerState.hp_Cur && time > 1f) // 플레이어 체력에 변화가 있다면
            {
                time = 0;
                float diffrent = currentHp - playerState.hp_Cur;
                if (diffrent <= playerState.Mp_Cur)
                {
                    playerState.hp_Cur += diffrent;
                    playerState.Mp_Cur -= diffrent;
                    character.PlayerUI();
                }

            }
            yield return null;
        }
    }
}
