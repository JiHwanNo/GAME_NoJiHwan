using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    [Header("Frame")]
    public GameObject charInfoFrame;
    public GameObject InvenFrame;
    public GameObject EquipInfoFrame;
    public GameObject ObjInfoFrame;
    public GameObject storeFrame;
    public GameObject ItemInfo_Store;
    public Transform SaleBox;
    public GameObject PosionSlot;

    [Header("Inventory")]
    public int gold;
    public TextMeshProUGUI goldAmount;
    public Transform Rect;
    public Equip_Manager equip_Manager;

    [Header("Drag&Drop")]
    public Transform selectedItem;
    public Transform curParent;
    public Transform parentOnDrag;
    


   public Dictionary<string, GameObject> Inventory_List;
    void Awake()
    {
        Inventory_List = new Dictionary<string, GameObject>();
        equip_Manager = GetComponent<Equip_Manager>();
    }
    public void GetInvenInfo()
    {
        goldAmount.text = gold.ToString();
    }

    public void cancelbutton()
    {
        SaleBox.gameObject.SetActive(false);
    }

    public void SaleButton()
    {
        Items_Info items_Info = selectedItem.GetComponent<Items_Info>();
        if(items_Info.type == "Equipment")
        {
            Destroy(selectedItem.gameObject);
            gold += items_Info.resalsePrice;
            GetInvenInfo();
            SaleBox.gameObject.SetActive(false);
            EquipInfoFrame.gameObject.SetActive(false);
            Rect.gameObject.SetActive(false);
        }
        if (items_Info.type == "Obj")
        {
            items_Info.count--;
            selectedItem.GetComponentInChildren<TextMeshProUGUI>().text = selectedItem.GetComponent<Items_Info>().count.ToString();
            if (items_Info.count <=0)
            {
                Destroy(selectedItem.gameObject);
            }
            gold += items_Info.resalsePrice;
            GetInvenInfo();
            SaleBox.gameObject.SetActive(false);
            EquipInfoFrame.gameObject.SetActive(false);
            Rect.gameObject.SetActive(false);
        }
    }
}
