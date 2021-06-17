using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Effect : MonoBehaviour
{
    public Transform target;
    public float speed;
    public GameObject hitEffect;

    private void OnEnable()
    {
        StartCoroutine("SkillShot");
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
    void OnHitEffect(Vector3 hitPoint)
    {
        hitEffect.transform.position = hitPoint + new Vector3(0, 1, 0);
        hitEffect.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy"|| other.gameObject.tag == "Dead")
        {
            StopCoroutine("SkillShot");
            gameObject.SetActive(false);

            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;
            OnHitEffect(hitPoint);
        }
    }
}
