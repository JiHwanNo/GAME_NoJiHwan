using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip_Manager : MonoBehaviour
{
    [Header("Player")]
    public PlayerState player;

    [Header("Charecter Info")]
    public Transform[] slot_Equip; // ĳ���� â�� ����
    public Items_Info[] cur_Equip; // ���� ĳ���� â�� ���Կ� �ִ� ������ ����.

    public void UsedPosion()
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        Items_Info item = Manager.instance.inventory.selectedItem.GetComponent<Items_Info>();
        
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
}
