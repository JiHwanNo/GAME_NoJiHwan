using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Item_Manager : MonoBehaviour,IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public Image image;

    [Header("Location")]
    public bool inBag;

    float releaseTime;
    bool dragging;

    public void OnDrag(PointerEventData eventData)
    {
        if(inBag&&dragging)
        {
            transform.position = eventData.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image = GetComponent<Image>();
        Manager.instance.inventory.selectedItem = transform;
        if(inBag)
        {
            StartCoroutine("ReleaseTime");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(inBag)
        {
            StopCoroutine("ReleaseTime");
            transform.localScale = new Vector3(1, 1, 1);

            if(releaseTime >= 0.5f)
            {
                transform.SetParent(Manager.instance.inventory.curParent);
                transform.localPosition = Vector3.zero;
                image.raycastTarget = true;
                return;
            }
            if(releaseTime <0.5f)
            {
                Manager.instance.inventory.itemInfoFrame.SetActive(false);

                Manager.instance.inventory.itemInfoFrame.GetComponent<ItemInfo_Frame>().item=GetComponent<Items_Info>();
                Manager.instance.inventory.itemInfoFrame.SetActive(true);

                Manager.instance.inventory.Rect.position = transform.position;
                Manager.instance.inventory.Rect.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator ReleaseTime()
    {
        releaseTime = 0;
        dragging = false;

        while(true)
        {
            releaseTime += Time.deltaTime;
            if (releaseTime >= 0.5f)
            {
                transform.localScale = new Vector3(1.3f, 1.3f, 1);

                if (!dragging)
                {
                    Manager.instance.inventory.itemInfoFrame.SetActive(false);

                    Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
                    dragging = true;
                    Manager.instance.inventory.curParent = transform.parent;
                    transform.SetParent(Manager.instance.inventory.parentOnDrag);

                    image.raycastTarget = false;

                    Manager.instance.inventory.Rect.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }
}
