using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equip_Manager : MonoBehaviour
{
    [Header("Player")]
    public PlayerState player;
    public GameObject PlayerHp;
    public GameObject PlayerMp;

    [Header("Charecter Info")]
    public Transform[] slot_Equip; // 캐릭터 창의 슬롯
    public Items_Info[] cur_Equip; // 현재 캐릭터 창의 슬롯에 있는 아이템 정보.

    public void UsedPosion()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Transform item = Manager.instance.inventory.selectedItem;
        Items_Info item_info = item.GetComponent<Items_Info>();
        if(item_info.name_Item == "Hp Posion")
        {
            UseHP_Posion(item_info);
            item.GetComponentInChildren<TextMeshProUGUI>().text = item_info.count.ToString();
            PlayerHp.GetComponent<Image>().fillAmount = player.hp_Cur / player.hp;
        }
        if(item_info.name_Item == "Mp Posion")
        {
            UseMP_Posion(item_info);
            item.GetComponentInChildren<TextMeshProUGUI>().text = item_info.count.ToString();
            PlayerMp.GetComponent<Image>().fillAmount = player.Mp_Cur / player.Mp;
        }
      
    }

    public void EquipBtn()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Items_Info item = Manager.instance.inventory.selectedItem.GetComponent<Items_Info>();
    
        GameObject item_Slot = Instantiate(item.gameObject, slot_Equip[item.equipNum]);// 슬롯란에 아이템 생성
        item_Slot.GetComponent<Item_Manager>().enabled = false;

        cur_Equip[item.equipNum] = item;
        item.equipped = true;
        item.Select_Text();

       cur_Equip[item.equipNum].select_Equip.SetActive(true); // 오브젝트 생성.

        IncreaseStates(cur_Equip[item.equipNum]);

        Reload_Frame();
    }


    public void ReleaseBtn()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Items_Info item = Manager.instance.inventory.selectedItem.GetComponent<Items_Info>();

        Destroy(slot_Equip[item.equipNum].GetChild(1).gameObject);// 오브젝트 없앰.

        item.equipped = false;
        item.Select_Text();

        ReduceStates(cur_Equip[item.equipNum]);

        cur_Equip[item.equipNum].select_Equip.SetActive(false);
        cur_Equip[item.equipNum] = null;

        Reload_Frame();
    }

    void IncreaseStates(Items_Info item)
    {
        player.hp += item.hpBonus;
        player.hp_Cur += item.hpBonus;
        player.atk += item.atkBonus;
        player.def += item.defBonus;
        player.cri += item.criBonus;

        Reload_Frame();
    }

    void ReduceStates(Items_Info item)
    {

        player.hp -= item.hpBonus;
        player.hp_Cur -= item.hpBonus;
        player.atk -= item.atkBonus;
        player.def -= item.defBonus;
        player.cri -= item.criBonus;

        Reload_Frame();
    }

    void Reload_Frame()
    {
        Manager.instance.inventory.EquipInfoFrame.SetActive(false);
        Manager.instance.inventory.EquipInfoFrame.SetActive(true);

        Manager.instance.inventory.charInfoFrame.SetActive(false);
        Manager.instance.inventory.charInfoFrame.SetActive(true);
    }

    void UseHP_Posion(Items_Info item)
    {
        if (player.hp_Cur < player.hp)
        {
            if (player.hp_Cur + item.HpRecovery < player.hp)
            {
                player.hp_Cur += item.HpRecovery;
            }
            else
            {
                player.hp_Cur = player.hp;
            }
            item.count--;
        }
        
    }
    void UseMP_Posion(Items_Info item)
    {
        if (player.Mp_Cur < player.Mp)
        {
            if (player.Mp_Cur + item.MpRecovery < player.Mp)
            {
                player.Mp_Cur += item.MpRecovery;
            }
            else
            {
                player.Mp_Cur = player.hp;
            }
            item.count--;
        }
    }
}
