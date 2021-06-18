using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObj : MonoBehaviour
{
    public float disableTime;
    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", disableTime);
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
