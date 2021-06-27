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
        // �κ��丮�� �������� �������
        if (inBag && dragging)
        {
            transform.position = eventData.position;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // �κ��丮�� �������� �������
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
                //�������� ������� �κ��丮�� ���� ����.
                //�������� ������� ������ ����.
                Check(transform);
                if(!_return)
                {
                    transform.parent = Parent;
                    transform.position = transform.parent.position;
                    transform.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                }
                if(_return) // �������� �������
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
        // �κ��丮�� �������� �������
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

    // �巹�׸� �����ϰ� ������.
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
                    Manager.instance.inventory.itemInfoFrame.SetActive(false);// ������ ���ֱ�

                    Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
                    dragging = true; // �巹�� ������ �ٲ�
                    Manager.instance.inventory.curParent = transform.parent; // ���� �θ� ����
                    transform.SetParent(Manager.instance.inventory.parentOnDrag); // �θ� Slot_Box�� ����.

                    image.raycastTarget = false;

                    Manager.instance.inventory.Rect.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    void Check(Transform _obj)
    {
        GameObject obj = Manager.instance.inventory.parentOnDrag.gameObject; // ���Թڽ� �ޱ�.
        
        for (int i = 0; i < obj.transform.childCount; i++) // ���� ���� üũ
        {
            if (obj.transform.GetChild(i).childCount == 1 && _obj.name == obj.transform.GetChild(i).GetChild(0).name )
            {
                Parent = obj.transform.GetChild(i);
                _return = true;
                break;
            }
            else if(obj.transform.GetChild(i).childCount ==0 &&Parent ==null) // �κ� ������ ��������
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
