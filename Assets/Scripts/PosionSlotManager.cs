using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PosionSlotManager : MonoBehaviour
{
    public GameObject Hp_Button;
    public GameObject Mp_Button;

    Dictionary<string, GameObject> inventory_list;
    Equip_Manager using_Manager;

    Transform usedobj;
    Transform InvenSlot;

    public Transform Hp_Posion;
    public Transform Mp_Posion;

    float cooltime;
    float cooldown;
    bool[] cool;
    Image[] posion_image;
    

    private void Awake()
    {
        Hp_Button.SetActive(false);
        Mp_Button.SetActive(false);
    }
    private void Start()
    {
        InvenSlot = Manager.instance.inventory.parentOnDrag;
        using_Manager = Manager.instance.inventory.equip_Manager;

        posion_image = new Image[2];
        posion_image[0] = Hp_Button.GetComponent<Image>();
        posion_image[1] = Mp_Button.GetComponent<Image>();
        cooltime = 0.5f;

        cool = new bool[2];
        cool[0] = false;
        cool[1] = false;
    }

    public void Check_Posion()
    {
        inventory_list = Manager.instance.inventory.Inventory_List;
        
        if (inventory_list.ContainsKey("Hp Posion"))
        {
            Hp_Button.SetActive(true);
        }
        else
        {
            Hp_Button.SetActive(false);
        }

        if (inventory_list.ContainsKey("Mp Posion"))
        {
            Mp_Button.SetActive(true);
        }
        else
        {
            Mp_Button.SetActive(false);
        }
    }

    public void HpButton_press()
    {
        if(!cool[0])
        {
            cool[0] = true;
            StartCoroutine(CoolDown(0));
            Manager.instance.inventory.selectedItem = GetHpPosion();
            using_Manager.UsedPosion();
        }
    }
    public void MpButton_Press()
    {
        if(!cool[1])
        {
            cool[1] = true;
            StartCoroutine(CoolDown(1));
            Manager.instance.inventory.selectedItem = GetMpPosion();
            using_Manager.UsedPosion();
        }
    }

    Transform GetHpPosion()
    {
        for (int i = 0; i < InvenSlot.childCount; i++)
        {
            if (InvenSlot.GetChild(i).childCount == 1 && InvenSlot.GetChild(i).GetChild(0).name == Hp_Posion.name)
            {
                
                usedobj = InvenSlot.GetChild(i).GetChild(0);
            }
        }
        return usedobj;
    }
    Transform GetMpPosion()
    {
        for (int i = 0; i < InvenSlot.childCount; i++)
        {
            if (InvenSlot.GetChild(i).childCount == 1 && InvenSlot.GetChild(i).GetChild(0).name == Mp_Posion.name)
            {
                usedobj = InvenSlot.GetChild(i).GetChild(0);
            }
        }
        return usedobj;
    }
    IEnumerator CoolDown(int index)
    {
        cooldown = 0;
        while (true)
        {
            cooldown += Time.deltaTime;
            posion_image[index].fillAmount = cooldown / cooltime;

            if (cooldown >= cooltime)
            {
                cool[index] = false;
                break;
            }
            yield return null;
        }
    }
}
