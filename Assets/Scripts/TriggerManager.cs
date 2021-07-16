using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public bool trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "RangeBar")
        {
            trigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }
}
