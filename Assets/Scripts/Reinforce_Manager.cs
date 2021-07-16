using UnityEngine;

public class Reinforce_Manager : MonoBehaviour
{
    public GameObject ReinforceFrame;
    public Transform Item_Slot;
    public GameObject Item; // ��ȭ ���Կ� ���� ��ü ������.
    public GameObject ReinforceGameFrame;
    public GameObject WarmingBox;

    [Header("Matrial")]
    Transform Inventory;
    Transform Matrial; // ��� �� �ޱ�.


    private void Start()
    {
        Matrial = null;
    }
    public void ReinforceButton()
    {
        ReinforceFrame.SetActive(true);

        Manager.instance.manager_Dialog.dialog_Frame.SetActive(false);
        Manager.instance.inventory.InvenFrame.SetActive(true);

    }


    public void ReinforceGameStart()
    {
        // �κ��丮 �� ������ ī���͸� ���δ�.

        Matrial = FindMatrialItem();
        if (Matrial == null)
        {
            WarmingBox.SetActive(true);
        }
        else
        {
            Manager.instance.inventory.gold -= Item.GetComponent<Items_Info>().ReinforceCost;
            Matrial.GetComponent<Items_Info>().count -= Item.GetComponent<Items_Info>().MaterialCount;

            
            ReinforceGameFrame.SetActive(true);
        }

    }

    Transform FindMatrialItem()
    {
        Inventory = Manager.instance.inventory.parentOnDrag;
        for (int i = 0; i < Inventory.childCount; i++)
        {
            if (Inventory.GetChild(i).GetChild(0).gameObject.name == Item.GetComponent<Items_Info>().MaterialName)
            {
                return Inventory.GetChild(i).GetChild(0).transform;
            }
        }
        return null;
    }
}
