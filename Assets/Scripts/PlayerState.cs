using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public GameObject LevelUp_effect;

    public int Lv;
    public float hp;
    public float Mp;
    public int atk;
    public int def;
    public float cri;
    public float exp_Max;


    public float exp_Cur;
    public float hp_Cur;
    public float Mp_Cur;
    float time;
    private void OnEnable()
    {
        hp_Cur = hp;
        Mp_Cur = Mp;
    }

    public void LevelUp()
    {
        LevelUp_effect.SetActive(true);
        exp_Max *= 2;
        hp *= 1.1f;
        Mp *= 1.1f;
        atk += 10;
        def += 5;
        StartCoroutine("Effect_Time");
    }

    IEnumerator Effect_Time()
    {
        while (true)
        {
            LevelUp_effect.transform.position = transform.position;
            time += Time.fixedDeltaTime;
            if (time >= 1f)
            {
                time = 0;
                StopCoroutine("Effect_Time");
                LevelUp_effect.SetActive(false);
                break;
            }
            yield return null;
        }

    }
}
