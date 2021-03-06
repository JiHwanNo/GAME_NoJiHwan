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
        //인벤토리 일때.
        if (inBag && !inStore)
        {
            StartCoroutine("ReleaseTime");
        }
        //득템할때.
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
                // 아이템이 있을경우
                if (inventory_list.ContainsKey(transform.GetComponent<Items_Info>().name_Item)) 
                {
                    transform.gameObject.SetActive(false);
                    //자기 자신 받기
                    GameObject Parent_obj = Manager.instance.inventory.parentOnDrag.gameObject;
                    for (int i = 0; i < Parent_obj.transform.childCount; i++)
                    {
                        if (Parent_obj.transform.GetChild(i).childCount == 1 && Parent_obj.transform.GetChild(i).GetChild(0).name == transform.name)
                        {
                            Parent = Parent_obj.transform.GetChild(i).GetChild(0); // 자기 자신을 받는다. (Parent 재활용)
                        }
                    }
                    Parent.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                    Parent.GetComponentInChildren<TextMeshProUGUI>().text = Parent.transform.GetComponent<Items_Info>().count.ToString();
                }
                // 인벤토리에 아이템이 없을경우
                if (!inventory_list.ContainsKey(transform.GetComponent<Items_Info>().name_Item)) 
                {
                    transform.gameObject.SetActive(false);
                    //부모설정
                    GameObject Parent_obj = Manager.instance.inventory.parentOnDrag.gameObject; // SlotBox받기
                    for (int i = 0; i < Parent_obj.transform.childCount; i++)
                    {
                        if (Parent_obj.transform.GetChild(i).childCount == 0)
                        {
                            Parent = Parent_obj.transform.GetChild(i);
                            break;
                        }
                    }
                    //오브젝트 새로 생성
                    GameObject obj = Instantiate(transform.gameObject);
                    obj.name = transform.name;
                    obj.transform.SetParent(Parent.transform);
                    obj.SetActive(true);
                    obj.transform.localPosition = Vector3.zero;
                    obj.GetComponent<Item_Manager>().inBag = true;
                    //인벤토리 목록에 저장
                    inventory_list.Add(transform.GetComponent<Items_Info>().name_Item, obj.gameObject);

                    obj.transform.GetComponent<Items_Info>().count += Manager.instance.ObjectPool.getItemCount;
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = obj.transform.GetComponent<Items_Info>().count.ToString();
                    Manager.instance.inventory.PosionSlot.GetComponent<PosionSlotManager>().Check_Posion();


                }


                Parent = null; // 초기화
            }
        }
        //인벤토리에 없고 스토어에 있을 경우.
        if (!inBag && inStore)
        {
            Manager.instance.inventory.ItemInfo_Store.SetActive(false);

            Manager.instance.inventory.ItemInfo_Store.GetComponent<ItemInfo_Store>().item = GetComponent<Items_Info>();
            Manager.instance.inventory.ItemInfo_Store.SetActive(true);
        }
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 인벤토리에 아이템이 있을경우
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
        //아이템 스토어 이벤트 발생.
        if (inBag && inStore &&!transform.GetComponent<Items_Info>().equipped)
        {
            Manager.instance.inventory.SaleBox.gameObject.SetActive(true);
            Manager.instance.inventory.SaleBox.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("ReSale : {0} G", transform.GetComponent<Items_Info>().resalsePrice);
            inStore = false;
        }
        //아이템 강화 이벤트 발생.
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
                    Manager.instance.inventory.EquipInfoFrame.SetActive(false);// 프레임 없애기

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


}
