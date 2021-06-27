using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInfo_Frame : MonoBehaviour
{
    public Items_Info item;

    public GameObject stateBonus;

    public TextMeshProUGUI name_Item;
    public TextMeshProUGUI info_Item;
    public TextMeshProUGUI resalePrice;

    public TextMeshProUGUI hpBonus;
    public TextMeshProUGUI atkBonus;
    public TextMeshProUGUI defBonus;
    public TextMeshProUGUI criBonus;

    [Header("Button")]
    public GameObject equipButton;
    public GameObject releaseButton;

    private void OnEnable()
    {
        stateBonus.SetActive(false);
        name_Item.text = item.name_Item;
        info_Item.text = item.info_Item;
        resalePrice.text = string.Format("Resale Price : {0}", item.resalsePrice);

        if(item.type == "Equipment")
        {
            hpBonus.text = string.Format("HP + {0}",item.hpBonus);
            atkBonus.text = string.Format("ATK +{0}", item.atkBonus);
            defBonus.text = string.Format("DEF + {0}", item.defBonus);
            criBonus.text = string.Format("CRI + {0}", item.criBonus);
            stateBonus.SetActive(true);

            if(!item.equipped)
            {
                equipButton.SetActive(true);
            }
            if(item.equipped)
            {
                releaseButton.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        equipButton.SetActive(false);
        releaseButton.SetActive(false);
    }
}
