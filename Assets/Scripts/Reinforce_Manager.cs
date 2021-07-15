using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforce_Manager : MonoBehaviour
{
    public GameObject ReinforceFrame;
    public Transform Item_Slot;
    public GameObject Item; // ��ȭ ���Կ� ���� ��ü ������.
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
