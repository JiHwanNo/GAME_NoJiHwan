using TMPro;
using UnityEngine;

public class ItemInfo_Frame : MonoBehaviour
{
    public Items_Info item;

    public GameObject stateBonus;

    public TextMeshProUGUI name_Item;
    public TextMeshProUGUI level_Item;
    public TextMeshProUGUI info_Item;
    public TextMeshProUGUI resalePrice;

    public TextMeshProUGUI hpBonus;
    public TextMeshProUGUI atkBonus;
    public TextMeshProUGUI defBonus;
    public TextMeshProUGUI criBonus;

    [Header("Button")]
    public GameObject equipButton;
    public GameObject releaseButton;
    public GameObject UsedButton;
    private void OnEnable()
    {
        name_Item.text = item.name_Item;
        info_Item.text = item.info_Item;

        resalePrice.text = string.Format("Resale Price : {0}", item.resalsePrice);

        if (item.type == "Equipment")
        {
            stateBonus.SetActive(false);
            hpBonus.text = string.Format("HP + {0}", (int)item.hpBonus);
            atkBonus.text = string.Format("ATK +{0}", (int)item.atkBonus);
            defBonus.text = string.Format("DEF + {0}", (int)item.defBonus);
            criBonus.text = string.Format("CRI + {0}", item.criBonus);
            stateBonus.SetActive(true);
            if(item.ItemLv >0)
            {
                level_Item.text = string.Format("+ {0}", item.ItemLv);
                level_Item.gameObject.SetActive(true);
            }
            if (!item.equipped)
            {
                equipButton.SetActive(true);
            }
            if (item.equipped)
            {
                releaseButton.SetActive(true);
            }
        }
        if(item.DetailType == "Matrial")
        {
            UsedButton.SetActive(false);
        }


    }

    private void OnDisable()
    {
        if (item.type == "Equipment")
        {
            level_Item.gameObject.SetActive(false);
            equipButton.SetActive(false);
            releaseButton.SetActive(false);
        }
    }
}
