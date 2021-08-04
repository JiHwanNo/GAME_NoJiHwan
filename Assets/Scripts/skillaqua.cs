using System.Collections;
using TMPro;
using UnityEngine;
public class skillaqua : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject dmgText;

    float speed;
    float time = 0;
    float skillPower = 0.8f;
    int dmg;
    float dmgRange;

    CharacterMove character;
    Audio_Manager myaudio;
    PlayerState playerState;
    private void Awake()
    {
        character = Manager.instance.characterMove;
        myaudio = Manager.instance.myaudio;
        playerState = character.Player.GetComponent<PlayerState>();
    }
    private void Start()
    {
        speed = 10;
    }
    private void OnEnable()
    {
        hitEffect.SetActive(false);
        myaudio.audioSource.PlayOneShot(myaudio.AquaSkill_Flying);
        StartCoroutine("auqua_skill");
    }

    IEnumerator auqua_skill()
    {
        while (true)
        {
            time += Time.fixedDeltaTime * speed;
            if (time >= 2f)
            {
                time = 0;
                StopCoroutine("auqua_skill");
                gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
    void CalculateDmg()
    {
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
        if(hitEffect.activeSelf)
        {
            GameObject obj = Manager.instance.ObjectPool.GetEffect();
            obj.transform.position = hitPoint + new Vector3(0, 1, 0);
            obj.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            Vector3 hitPoint = (other.transform.position);
            OnHitEffect(hitPoint);

            CalculateDmg();

            if (!dmgText.activeSelf)
            {
                dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
                dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
                dmgText.transform.position = Manager.instance.characterMove.mycamera.WorldToScreenPoint(hitPoint + new Vector3(0, 1, 0));
                dmgText.SetActive(true);
            }
            else if (dmgText.activeSelf)
            {
                GameObject clone_dmgText = Manager.instance.ObjectPool.GetdmgText();
                clone_dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
                clone_dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
                clone_dmgText.transform.position = Manager.instance.characterMove.mycamera.WorldToScreenPoint(hitPoint + new Vector3(0, 1, 0));
                clone_dmgText.SetActive(true);
            }

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


}
