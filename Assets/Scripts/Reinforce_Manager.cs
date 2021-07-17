using TMPro;
using UnityEngine;

public class Reinforce_Manager : MonoBehaviour
{
    public GameObject ReinforceFrame;
    public Transform Item_Slot;
    public GameObject Item; // 강화 슬롯에 생긴 모체 아이템.
    public GameObject ReinforceGameFrame;
    public GameObject WarmingBox;

    [Header("Matrial")]
    Transform Inventory;
    Transform Matrial; // 재료 값 받기.
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
        // 인벤토리 내 보석의 카운터를 줄인다.
        Items_Info items_Info = Item.GetComponent<Items_Info>(); // 무기 아이템 정보 받기.

        Matrial = FindMatrialItem();
        //재료가 없을경우.
        if (Matrial == null) 
        {
            WarmingBox.SetActive(true);
        }
        else
        { 
            //재료 아이템 정보 받기.
            Items_Info Matrial_Info = Matrial.GetComponent<Items_Info>();
            //재료가 모자랄 경우
            if (Matrial_Info.count < items_Info.MaterialCount || inventroy.gold < items_Info.MaterialCount)
            {
                WarmingBox.SetActive(true);
            }
            // 재료가 충족되는 경우.
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
