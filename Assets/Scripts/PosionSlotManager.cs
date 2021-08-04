using System.Collections.Generic;
using UnityEngine;
public class PosionSlotManager : MonoBehaviour
{
    public GameObject Hp_Button;
    public GameObject Mp_Button;

    Dictionary<string, GameObject> inventory_list;
    Equip_Manager using_Manager;

    private void Awake()
    {
        Hp_Button.SetActive(false);
        Mp_Button.SetActive(false);
    }
    private void Start()
    {
        using_Manager = Manager.instance.inventory.equip_Manager;
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
        using_Manager.UsedPosion();
    }
    public void MpButton_Press()
    {
        using_Manager.UsedPosion();
    }
}
