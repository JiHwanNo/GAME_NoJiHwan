using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforce_Manager : MonoBehaviour
{
    public GameObject ReinforceFrame;
    public Transform Item_Slot;
    public GameObject Item; // 강화 슬롯에 생긴 모체 아이템.
    public GameObject ReinforceGameFrame;
   
    public void ReinforceButton()
    {
        ReinforceFrame.SetActive(true);
        Manager.instance.manager_Dialog.dialog_Frame.SetActive(false);
        Manager.instance.inventory.InvenFrame.SetActive(true);
    }


    public void ReinforceGameStart()
    {
        ReinforceGameFrame.SetActive(true);
    }


}
