using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Effect : MonoBehaviour
{
    Transform target;
    public float speed;
    public GameObject hitEffect;
    public int index;
    float time = 0;

    public GameObject dmgText;
    public float skillPower;
    int dmg;
    float dmgRange;
    private void OnEnable()
    {
        target = Manager.instance.characterMove.atkTarget;
        
        if(index == 2)
        {
            skillPower = 0.8f;
            Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.Heal);
            StartCoroutine("HealSkill");

            CalculateDmg();
            Vector3 hitPoint = (transform.position) * 0.5f;
            dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
            dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
            dmgText.transform.position = Manager.instance.characterMove.mycamera.WorldToScreenPoint(Manager.instance.characterMove.Player.transform.position + new Vector3(0, 1, 0));
            dmgText.SetActive(true);
        }
        else if(index == 0)
        {
            skillPower = 1.25f;
            Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.FireSkill_Flying);
            StartCoroutine("SkillShot");
            
        }
        else if (index == 1)
        {
            skillPower = 1.5f;
            Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.AquaSkill_Flying);
            StartCoroutine("SkillShot");
        }
    }
    IEnumerator SkillShot()
    {
        while (true)
        {
            Vector3 dir = target.position + new Vector3(0, 2, 0);
            transform.LookAt(dir);
            transform.Translate(Vector3.forward * speed*Time.deltaTime);
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
        PlayerState playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();

        dmgRange = Random.Range(0.8f, 1.2f);
        dmg = (int)(playerState.atk * skillPower * dmgRange);

        int critical = Random.Range(0, 100);
        if(critical < playerState.cri*100)
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
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Dead")
        {
            StopCoroutine("SkillShot");
            gameObject.SetActive(false);

            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;
            OnHitEffect(hitPoint);

            CalculateDmg();

            dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
            dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
            dmgText.transform.position = Manager.instance.characterMove.mycamera.WorldToScreenPoint(hitPoint+new Vector3(0,1,0));
            dmgText.SetActive(true);

            if (other.gameObject.tag == "Enemy")
            {
              other.GetComponent<EnemyHit>().Hit(dmg);

            }
        }
    }
}
