using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float hp;
    public float cur_Hp;
    private void OnEnable()
    {
        cur_Hp = hp;
    }
}
