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
        // 인벤토리 내 보석의 카운터를 줄인다.

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
