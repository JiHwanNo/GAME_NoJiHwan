using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    public string SkiilName;

    float cooldown;
    public float coolTime;
    public Image CTimage1;
    bool cool;

    public void OnClickBtn()
    {
        if(!cool)
        {
            cool = true;
            StartCoroutine("CoolDown");
        }
    }

    IEnumerator CoolDown()
    {
        cooldown = 0;

        while (true)
        {
            cooldown += Time.deltaTime;
            CTimage1.fillAmount = cooldown / coolTime;

            if (cooldown >= coolTime)
            {
                cool = false;
                StopCoroutine("CoolDown");
            }

            yield return null;
        }
    }

}
