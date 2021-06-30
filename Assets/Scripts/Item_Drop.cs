using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Inventory_Manager inven = Manager.instance.inventory;

        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        if (transform.childCount != 0)
        {
            Transform item = transform.GetChild(0);
            item.SetParent(inven.curParent);
            item.localPosition = Vector3.zero;
        }
        inven.selectedItem.SetParent(transform);
        inven.selectedItem.localPosition = Vector3.zero;
    }
}
