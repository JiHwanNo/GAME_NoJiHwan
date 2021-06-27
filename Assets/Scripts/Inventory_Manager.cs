using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory_Manager : MonoBehaviour
{
    [Header("Frame")]
    public GameObject charInfoFrame;
    public GameObject InvenFrame;
    public GameObject itemInfoFrame;

    [Header("Inventory")]
    public int gold;
    public TextMeshProUGUI goldAmount;
    public Transform Rect;

    [Header("Drag&Drop")]
    public Transform selectedItem;
    public Transform curParent;
    public Transform parentOnDrag;


    public void GetInvenInfo()
    {
        goldAmount.text = gold.ToString();
    }
}
