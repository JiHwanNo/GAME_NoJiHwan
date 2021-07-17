using TMPro;
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
    Inventory_Manager inventroy;

    private void Start()
    {
        Matrial = null;
        inventroy = Manager.instance.inventory;
    }
    public void ReinforceButton()
    {
        ReinforceFrame.SetActive(true);

        Manager.instance.manager_Dialog.dialog_Frame.SetActive(false);
        inventroy.InvenFrame.SetActive(true);

    }


    public void ReinforceGameStart()
    {
        // �κ��丮 �� ������ ī���͸� ���δ�.
        Items_Info items_Info = Item.GetComponent<Items_Info>(); // ���� ������ ���� �ޱ�.

        Matrial = FindMatrialItem();
        //��ᰡ �������.
        if (Matrial == null) 
        {
            WarmingBox.SetActive(true);
        }
        else
        { 
            //��� ������ ���� �ޱ�.
            Items_Info Matrial_Info = Matrial.GetComponent<Items_Info>();
            //��ᰡ ���ڶ� ���
            if (Matrial_Info.count < items_Info.MaterialCount || inventroy.gold < items_Info.MaterialCount)
            {
                WarmingBox.SetActive(true);
            }
            // ��ᰡ �����Ǵ� ���.
            else
            {
                inventroy.gold -= items_Info.ReinforceCost;
                Matrial_Info.count -= items_Info.MaterialCount;


                Matrial_Info.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Matrial_Info.count.ToString();
                inventroy.goldAmount.text = inventroy.gold.ToString();
                if (Matrial_Info.count <= 0)
                {
                    Matrial_Info.SelfDestroy(Matrial);
                }

                ReinforceGameFrame.SetActive(true);
                inventroy.InvenFrame.SetActive(false);
                inventroy.InvenFrame.SetActive(true);
            }
        }
    }

    Transform FindMatrialItem()
    {
        Inventory = Manager.instance.inventory.parentOnDrag;
        for (int i = 0; i < Inventory.childCount; i++)
        {
            if (Inventory.GetChild(i).childCount == 1 && Inventory.GetChild(i).GetChild(0).gameObject.name == Item.GetComponent<Items_Info>().MaterialName)
            {
                return Inventory.GetChild(i).GetChild(0).transform;
            }
        }
        return null;
    }
}
