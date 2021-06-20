using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Effect : MonoBehaviour
{
    Transform target;
    public float speed;
    public GameObject hitEffect;
    public int index;
    float time = 0;
    private void OnEnable()
    {
        target = Manager.instance.characterMove.atkTarget;
        
        if(index == 2)
        {
            Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.Heal);
            StartCoroutine("HealSkill");
        }
        else
        {
            Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.FireSkill_Flying);
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
        }
    }
}
