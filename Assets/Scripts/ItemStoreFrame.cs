using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemStoreFrame : MonoBehaviour
{
    public Transform[] Items;
    public Transform[] Slot_ItemBox;
    public void OpenStore()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(Slot_ItemBox[i].childCount ==2)
            {
                Slot_ItemBox[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("{0} G", Slot_ItemBox[i].GetChild(1).GetComponent<Items_Info>().price);
            }
        }
        Manager.instance.manager_Dialog.dialog_Frame.SetActive(false);
        Manager.instance.inventory.InvenFrame.SetActive(true);
        gameObject.SetActive(true);
    }
}
