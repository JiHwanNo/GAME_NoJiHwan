using UnityEngine;
using UnityEngine.UI;

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
    public string MaterialName;
    public int ReinforceCost;
    public int MaterialCount;
    public int ItemLv;

    [Header("Equipment Info")]
    public float hpBonus;
    public float atkBonus;
    public float defBonus;
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

    public void ItemLevelUp()
    {
        ItemLv++;
        Colorset(ItemLv);
        hpBonus *= 1.2f;
        atkBonus *= 1.2f;
        defBonus *= 1.2f;
        criBonus *= 1.2f;

        ReinforceCost *= 2;
        MaterialCount *= 2;
    }

    void Colorset(int ltemlevel)
    {
        switch (ltemlevel)
        {
            case 0:
                break;
            case 1:
                transform.GetComponent<Image>().color = new Color32(255,0,0,255);
                break;
            case 2:
                transform.GetComponent<Image>().color = new Color32(255, 160, 0, 255);
                break;
            case 3:
                transform.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
                break;
            case 4:
                transform.GetComponent<Image>().color = new Color32(0, 255, 41, 255);
                break;
            case 5:
                transform.GetComponent<Image>().color = new Color32(0, 255, 231, 255);
                break;
            case 6:
                transform.GetComponent<Image>().color = new Color32(0, 153, 255, 255);
                break;
            default:
                break;
        }
    }
}
