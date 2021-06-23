using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
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

    private void OnEnable()
    {
        hp_Cur = hp;
        Mp_Cur = Mp;
    }

    public void LevelUp()
    {
        exp_Max *= 2;
        hp *= 1.1f;
        Mp *= 1.1f;
        atk += 10;
        def += 5;

    }
}
