using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item_Manager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public Image image;

    [Header("Location")]
    public bool inBag;

    float releaseTime;
    bool dragging;
    Transform Parent;
    bool _return;
    public void OnDrag(PointerEventData eventData)
    {
        // 인벤토리에 아이템이 있을경우
        if (inBag && dragging)
        {
            transform.position = eventData.position;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 인벤토리에 아이템이 있을경우
        image = GetComponent<Image>();
        Manager.instance.inventory.selectedItem = transform;
        if (inBag)
        {
            StartCoroutine("ReleaseTime");
        }
        if (!inBag)
        {
            if(transform.name == "Gold_Image")
            {
                Manager.instance.inventory.gold +=Manager.instance.dropItem_Manager.getgoal;
                Manager.instance.inventory.GetInvenInfo();
                transform.gameObject.SetActive(false);
            }
            else
            {
                //아이템이 없을경우 인벤토리에 새로 생성.
                //아이템이 있을경우 갯수만 증가.
                Check(transform);
                if(!_return)
                {
                    transform.parent = Parent;
                    transform.position = transform.parent.position;
                    transform.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                }
                if(_return) // 아이템이 있을경우
                {
                    transform.gameObject.SetActive(false);
                    Transform obj = Parent.GetChild(0);
                    obj.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 인벤토리에 아이템이 있을경우
        if (inBag)
        {
            StopCoroutine("ReleaseTime");
            transform.localScale = new Vector3(1, 1, 1);

            if (releaseTime >= 0.5f)
            {
                transform.SetParent(Manager.instance.inventory.curParent);
                transform.localPosition = Vector3.zero;
                image.raycastTarget = true;
                return;
            }
            if (releaseTime < 0.5f)
            {
                Manager.instance.inventory.itemInfoFrame.SetActive(false);

                Manager.instance.inventory.itemInfoFrame.GetComponent<ItemInfo_Frame>().item = GetComponent<Items_Info>();
                Manager.instance.inventory.itemInfoFrame.SetActive(true);

                Manager.instance.inventory.Rect.position = transform.position;
                Manager.instance.inventory.Rect.gameObject.SetActive(true);
            }
        }
    }

    // 드레그를 시작하고 있을때.
    IEnumerator ReleaseTime()
    {
        releaseTime = 0;
        dragging = false;

        while (true)
        {
            releaseTime += Time.deltaTime;
            if (releaseTime >= 0.5f)
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1);

                if (!dragging)
                {
                    Manager.instance.inventory.itemInfoFrame.SetActive(false);// 프레임 없애기

                    Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
                    dragging = true; // 드레그 중으로 바꿈
                    Manager.instance.inventory.curParent = transform.parent; // 현재 부모 설정
                    transform.SetParent(Manager.instance.inventory.parentOnDrag); // 부모를 Slot_Box로 설정.

                    image.raycastTarget = false;

                    Manager.instance.inventory.Rect.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    void Check(Transform _obj)
    {
        GameObject obj = Manager.instance.inventory.parentOnDrag.gameObject; // 슬롯박스 받기.
        
        for (int i = 0; i < obj.transform.childCount; i++) // 슬롯 하위 체크
        {
            if (obj.transform.GetChild(i).childCount == 1 && _obj.name == obj.transform.GetChild(i).GetChild(0).name )
            {
                Parent = obj.transform.GetChild(i);
                _return = true;
                break;
            }
            else if(obj.transform.GetChild(i).childCount ==0 &&Parent ==null) // 인벤 슬롯이 비었을경우
            {
                Parent = obj.transform.GetChild(i);
                _return = false;
            }
            else if(obj.transform.GetChild(i).childCount ==0 && Parent !=null)
            {
                _return = false;
            }
           
        }
    }
}
