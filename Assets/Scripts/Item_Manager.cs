using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item_Manager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler ,IDragHandler
{
    public Image image;

    [Header("Location")]
    public bool inBag;
    public bool inStore;
    public bool inReinforce;

    float releaseTime;
    bool dragging;
    Transform Parent;

    Dictionary<string, GameObject> inventory_list;

    void Awake()
    {
        inventory_list = Manager.instance.inventory.Inventory_List;
    }
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
        //�κ��丮 �϶�.
        if (inBag && !inStore)
        {
            StartCoroutine("ReleaseTime");
        }
        //�����Ҷ�.
        if (!inBag && !inStore)
        {
            if (transform.name == "Gold_Image")
            {
                Manager.instance.inventory.gold += Manager.instance.dropItem_Manager.getgoal;
                Manager.instance.inventory.GetInvenInfo();
                transform.gameObject.SetActive(false);
            }
            else
            {
                // �������� �������
                if (inventory_list.ContainsKey(transform.GetComponent<Items_Info>().name_Item)) 
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
                // �κ��丮�� �������� �������
                if (!inventory_list.ContainsKey(transform.GetComponent<Items_Info>().name_Item)) 
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
                    inventory_list.Add(transform.GetComponent<Items_Info>().name_Item, obj.gameObject);

                    obj.transform.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = obj.transform.GetComponent<Items_Info>().count.ToString();
                    Manager.instance.inventory.PosionSlot.GetComponent<PosionSlotManager>().Check_Posion();


                }


                Parent = null; // �ʱ�ȭ
            }
        }
        //�κ��丮�� ���� ���� ���� ���.
        if (!inBag && inStore)
        {
            Manager.instance.inventory.ItemInfo_Store.SetActive(false);

            Manager.instance.inventory.ItemInfo_Store.GetComponent<ItemInfo_Store>().item = GetComponent<Items_Info>();
            Manager.instance.inventory.ItemInfo_Store.SetActive(true);
        }
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �κ��丮�� �������� �������
        if (inBag&&!inStore)
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
            if (releaseTime < 0.5f && transform.GetComponent<Items_Info>().type == "Equipment")
            {
                Manager.instance.inventory.EquipInfoFrame.SetActive(false);

                Manager.instance.inventory.EquipInfoFrame.GetComponent<ItemInfo_Frame>().item = GetComponent<Items_Info>();
                Manager.instance.inventory.EquipInfoFrame.SetActive(true);

                Manager.instance.inventory.Rect.position = transform.position;
                Manager.instance.inventory.Rect.gameObject.SetActive(true);


            }
            if (releaseTime < 0.5f && transform.GetComponent<Items_Info>().type == "Obj")
            {
                Manager.instance.inventory.ObjInfoFrame.SetActive(false);

                Manager.instance.inventory.ObjInfoFrame.GetComponent<ItemInfo_Frame>().item = GetComponent<Items_Info>();
                Manager.instance.inventory.ObjInfoFrame.SetActive(true);

                Manager.instance.inventory.Rect.position = transform.position;
                Manager.instance.inventory.Rect.gameObject.SetActive(true);
            }

            if (Manager.instance.inventory.storeFrame.activeSelf)
            {
                inStore = true;
            }
            if(Manager.instance.reinforce_Manager.ReinforceFrame.activeSelf && transform.GetComponent<Items_Info>().type == "Equipment")
            {
                inReinforce = true;
            }
        }
        //������ ����� �̺�Ʈ �߻�.
        if (inBag && inStore &&!transform.GetComponent<Items_Info>().equipped)
        {
            Manager.instance.inventory.SaleBox.gameObject.SetActive(true);
            Manager.instance.inventory.SaleBox.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("ReSale : {0} G", transform.GetComponent<Items_Info>().resalsePrice);
            inStore = false;
        }
        //������ ��ȭ �̺�Ʈ �߻�.
        if (inBag && inReinforce && !transform.GetComponent<Items_Info>().equipped)
        {
            if(Manager.instance.reinforce_Manager.Item_Slot.childCount ==0)
            {
                GameObject obj = Instantiate(transform.gameObject);
                obj.transform.SetParent(Manager.instance.reinforce_Manager.Item_Slot);
                obj.SetActive(true);
                obj.transform.localPosition = Vector3.zero;
                Manager.instance.reinforce_Manager.Item = transform.gameObject;
            }
            else if(Manager.instance.reinforce_Manager.Item_Slot.childCount == 1)
            {
                GameObject obj = Instantiate(transform.gameObject);
                obj.name = transform.gameObject.name;
                obj.transform.SetParent(Manager.instance.reinforce_Manager.Item_Slot);
                obj.SetActive(true);
                obj.transform.localPosition = Vector3.zero;
                Manager.instance.reinforce_Manager.Item = transform.gameObject;
                Destroy(Manager.instance.reinforce_Manager.Item_Slot.GetChild(0).gameObject);
                Manager.instance.reinforce_Manager.ReinforceFrame.SetActive(false);
                Manager.instance.reinforce_Manager.ReinforceFrame.SetActive(true);
            }

            inReinforce = false;
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
                    Manager.instance.inventory.EquipInfoFrame.SetActive(false);// ������ ���ֱ�

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
