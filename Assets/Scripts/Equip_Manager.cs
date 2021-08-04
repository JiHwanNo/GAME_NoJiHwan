using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Equip_Manager : MonoBehaviour
{
    [Header("Player")]
    public PlayerState player;
    public GameObject PlayerHp;
    public GameObject PlayerMp;
    public CharacterMove character;

    [Header("Charecter Info")]
    public Transform[] slot_Equip; // ĳ���� â�� ����
    public Items_Info[] cur_Equip; // ���� ĳ���� â�� ���Կ� �ִ� ������ ����.

    PosionSlotManager posionSlot;
    private void Start()
    {
        character = Manager.instance.GetComponent<CharacterMove>();
        posionSlot = Manager.instance.inventory.PosionSlot.GetComponent<PosionSlotManager>();
    }
    public void UsedPosion()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Transform item = Manager.instance.inventory.selectedItem;
        Items_Info item_info = item.GetComponent<Items_Info>();

        Dictionary<string, GameObject> inven_list = Manager.instance.inventory.Inventory_List;

        if (item_info.name_Item == "Hp Posion")
        {
            UseHP_Posion(item_info);
            item.GetComponentInChildren<TextMeshProUGUI>().text = item_info.count.ToString();
            character.PlayerUI();
            if(item_info.count ==0)
            {
                item_info.SelfDestroy(item);
                inven_list.Remove(item_info.name_Item);
                posionSlot.Check_Posion();
            }
        }
        if (item_info.name_Item == "Mp Posion")
        {

            UseMP_Posion(item_info);
            item.GetComponentInChildren<TextMeshProUGUI>().text = item_info.count.ToString();
            character.PlayerUI();

            if (item_info.count == 0)
            {
                item_info.SelfDestroy(item);
                inven_list.Remove(item_info.name_Item);
                posionSlot.Check_Posion();
            }
        }

    }

    public void EquipBtn()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Items_Info item = Manager.instance.inventory.selectedItem.GetComponent<Items_Info>();

        GameObject item_Slot = Instantiate(item.gameObject, slot_Equip[item.equipNum]);// ���Զ��� ������ ����
        item_Slot.GetComponent<Item_Manager>().enabled = false;

        cur_Equip[item.equipNum] = item;
        item.equipped = true;
        item.Select_Text();

        cur_Equip[item.equipNum].select_Equip.SetActive(true); // ������Ʈ ����.

        IncreaseStates(cur_Equip[item.equipNum]);

        Reload_Frame();
    }


    public void ReleaseBtn()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Items_Info item = Manager.instance.inventory.selectedItem.GetComponent<Items_Info>();

        Destroy(slot_Equip[item.equipNum].GetChild(1).gameObject);// ������Ʈ ����.

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
        player.atk += (int)item.atkBonus;
        player.def += (int)item.defBonus;
        player.cri += item.criBonus;

        Reload_Frame();
    }

    void ReduceStates(Items_Info item)
    {

        player.hp -= item.hpBonus;
        player.hp_Cur -= item.hpBonus;
        player.atk -= (int)item.atkBonus;
        player.def -= (int)item.defBonus;
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
        }

        item.count--;
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
        }
        item.count--;
    }
}
