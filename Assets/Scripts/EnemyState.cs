using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float hp;
    public float cur_Hp;
    public int atk;
    public float cri;
    public int exp;

    private void OnEnable()
    {
        cur_Hp = hp;
    }
}
