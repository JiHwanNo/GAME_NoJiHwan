using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    [Header("Manager")]
    public Audio_Manager myaudio;
    public Inventory_Manager inventory;
    public CharacterMove characterMove;
    public ObjectPoolManager ObjectPool;
    public DropItem_Manager dropItem_Manager;
    public UI_Manager uI_Manager;
    public Manager_Dialog manager_Dialog;
    public MenuController menu_Controller;
    private void Awake()
    {
        if(instance  != this)
        {
            instance = this;
        }
    }
}
