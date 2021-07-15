using UnityEngine;

public class Items_Info : MonoBehaviour
{
    [Header("Common Info")]
    public string type;
    public string DetailType;
    public string name_Item;
    public string info_Item;
    public int resalsePrice;
    public int count;
    [Header("Reinforce Info")]
    public int ReinforceCost;
    public int MaterialCount;
    public int ItemLv;

    [Header("Equipment Info")]
    public int hpBonus;
    public int atkBonus;
    public int defBonus;
    public float criBonus;
    public int price;

    public bool equipped;
    public int equipNum;
    public GameObject select_Equip; // 장비 착용 오브젝트 받기

    [Header("Obj Info")]
    public int HpRecovery;
    public int MpRecovery;
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

    public void SelfDestroy(Transform obj)
    {
        Destroy(obj.gameObject);
        Manager.instance.inventory.Inventory_List.Remove(obj.name);
    }
}
