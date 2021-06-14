using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Info : MonoBehaviour
{
    [Header("Common Info")]
    public string type;
    public string name_Item;
    public string info_Item;
    public int resalsePrice;
    public int count;

    [Header("Equipment Info")]
    public int hpBonus;
    public int atkBonus;
    public int defBonus;
    public float criBonus;

    public bool equipped;
    public int equipNum;
    public GameObject select_Equip;

    public void Select_Text()
    {
        if (equipped)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
