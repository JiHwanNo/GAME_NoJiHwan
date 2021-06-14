using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    [Header("Manager")]
    public Audio_Manager myaudio;
    public Inventory_Manager inventory;

    private void Awake()
    {
        if(instance  != this)
        {
            instance = this;
        }
    }
}
