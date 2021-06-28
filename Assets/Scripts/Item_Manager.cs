using System.Collections;
using System.Collections.Generic;
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
                if (Manager.instance.inventory.Inventory_List.ContainsKey(transform.name)) // �������� �������
                {
                    transform.gameObject.SetActive(false);
                    //�ڱ� �ڽ� �ޱ�
                    GameObject Parent_obj = Manager.instance.inventory.parentOnDrag.gameObject;
                    for (int i = 0; i < Parent_obj.transform.childCount; i++)
                    {
                        if (Parent_obj.transform.GetChild(i).childCount == 1 && Parent_obj.transform.GetChild(i).GetChild(0).name == transform.name)
                        {
                            Parent = Parent_obj.transform.GetChild(i).GetChild(0); // �ڱ� �ڽ��� �޴´�. (Parent ��Ȱ��)
                        }
                    }
                    Parent.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                    Parent.GetComponentInChildren<TextMeshProUGUI>().text = Parent.transform.GetComponent<Items_Info>().count.ToString();
                }

                if (!Manager.instance.inventory.Inventory_List.ContainsKey(transform.name)) // �κ��丮�� �������� �������
                {
                    transform.gameObject.SetActive(false);
                    //�θ���
                    GameObject Parent_obj = Manager.instance.inventory.parentOnDrag.gameObject; // SlotBox�ޱ�
                    for (int i = 0; i < Parent_obj.transform.childCount; i++)
                    {
                        if (Parent_obj.transform.GetChild(i).childCount == 0)
                        {
                            Parent = Parent_obj.transform.GetChild(i);
                            break;
                        }
                    }
                    //������Ʈ ���� ����
                    GameObject obj = Instantiate(transform.gameObject);
                    obj.name = transform.name;
                    obj.transform.SetParent(Parent.transform);
                    obj.SetActive(true);
                    obj.transform.localPosition = Vector3.zero;
                    obj.GetComponent<Item_Manager>().inBag = true;
                    //�κ��丮 ��Ͽ� ����
                    Manager.instance.inventory.Inventory_List.Add(transform.name, obj.gameObject);

                    obj.transform.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = obj.transform.GetComponent<Items_Info>().count.ToString();
                }
               

                Parent = null; // �ʱ�ȭ
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

   
}
