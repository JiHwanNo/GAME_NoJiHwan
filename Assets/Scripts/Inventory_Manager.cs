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

    [Header("Inventory")]
    public int gold;
    public TextMeshProUGUI goldAmount;
    public Transform Rect;

    [Header("Drag&Drop")]
    public Transform selectedItem;
    public Transform curParent;
    public Transform parentOnDrag;
    


   public Dictionary<string, GameObject> Inventory_List;
    void Awake()
    {
        Inventory_List = new Dictionary<string, GameObject>();
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
        Destroy(selectedItem.gameObject);
        gold += selectedItem.GetComponent<Items_Info>().resalsePrice;
        GetInvenInfo();
        SaleBox.gameObject.SetActive(false);
        EquipInfoFrame.gameObject.SetActive(false);
        Rect.gameObject.SetActive(false);

    }
}
