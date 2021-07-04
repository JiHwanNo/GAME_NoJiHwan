using TMPro;
using UnityEngine;

public class ItemInfo_Store : MonoBehaviour
{
    public Items_Info item;
    public Transform Selectitem;
    public Transform WarmingBox;

    Transform Parent;

    public GameObject stateBonus;
    
    public TextMeshProUGUI name_Item;
    public TextMeshProUGUI info_Item;
    public TextMeshProUGUI Price;

    public TextMeshProUGUI hpBonus;
    public TextMeshProUGUI atkBonus;
    public TextMeshProUGUI defBonus;
    public TextMeshProUGUI criBonus;


    private void OnEnable()
    {
        name_Item.text = item.name_Item;
        info_Item.text = item.info_Item;
        Price.text = string.Format("Price : {0}", item.price);

        if (item.type == "Equipment")
        {
            stateBonus.SetActive(false);
            hpBonus.text = string.Format("HP + {0}", item.hpBonus);
            atkBonus.text = string.Format("ATK +{0}", item.atkBonus);
            defBonus.text = string.Format("DEF + {0}", item.defBonus);
            criBonus.text = string.Format("CRI + {0}", item.criBonus);
            stateBonus.SetActive(true);

        }
        WarmingBox.gameObject.SetActive(false);

    }
    public void CloseButton()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        gameObject.SetActive(false);
    }
    public void CloseWarming()
    {
        WarmingBox.gameObject.SetActive(false);
    }

    public void BuyButton()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

       if(Manager.instance.inventory.gold < item.price)
       {
           WarmingBox.gameObject.SetActive(true);
       }
       else
       {
            Manager.instance.inventory.gold -= item.price;

            BuyItem();

            Manager.instance.inventory.InvenFrame.SetActive(false);
            Manager.instance.inventory.GetInvenInfo();
            Manager.instance.inventory.InvenFrame.SetActive(true);
        }
       
    }

    void BuyItem()
    {
        Selectitem = Manager.instance.inventory.selectedItem; // 선택한 아이템

        GameObject Parent_obj = Manager.instance.inventory.parentOnDrag.gameObject; // SlotBox받기
        for (int i = 0; i < Parent_obj.transform.childCount; i++)
        {
            if (Parent_obj.transform.GetChild(i).childCount == 0)
            {
                Parent = Parent_obj.transform.GetChild(i);
                break;
            }
        }

        //오브젝트 새로 생성
        GameObject obj = Instantiate(Selectitem.gameObject);
        obj.name = Selectitem.name;
        obj.transform.SetParent(Parent.transform);
        obj.SetActive(true);
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<Item_Manager>().inBag = true;
        obj.GetComponent<Item_Manager>().inStore = false;

        Parent = null; // 초기화
    }
}




