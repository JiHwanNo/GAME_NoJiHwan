using System.Collections;
using TMPro;
using UnityEngine;
public class ReinforceFrame : MonoBehaviour
{
    public TextMeshProUGUI Text;
    Transform Slot;
    public Transform MaterialSlot;
    public Transform MaterialCoin;

    GameObject Item;
    Items_Info items_Info;
    private void Awake()
    {
        Slot = transform.GetChild(3);
    }

    private void OnEnable()
    {
        Text.text = Manager.instance.characterMove.target.GetComponent<Obj_Info>().Obj_Name;
        StartCoroutine("checkItem");
    }
    private void OnDisable()
    {
        MaterialSlot.GetChild(1).gameObject.SetActive(false);
        MaterialSlot.GetChild(0).gameObject.SetActive(false);
        MaterialCoin.GetChild(0).gameObject.SetActive(false);
    }
    void getmaterial()
    {
        if (items_Info.DetailType == "Weapon")
        {
            MaterialSlot.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = items_Info.MaterialCount.ToString();
            MaterialSlot.GetChild(1).gameObject.SetActive(true);
        }

        if (items_Info.DetailType == "Armor")
        {
            MaterialSlot.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = items_Info.MaterialCount.ToString();
            MaterialSlot.GetChild(0).gameObject.SetActive(true);
        }

        MaterialCoin.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = items_Info.ReinforceCost.ToString();
        MaterialCoin.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator checkItem()
    {
        while (true)
        {
            if (Slot.childCount == 1)
            {
                Item = Slot.GetChild(0).gameObject;
                items_Info = Slot.GetChild(0).GetComponent<Items_Info>();
                StopCoroutine("checkItem");
                StartCoroutine("UpdateMaterial");

                break;

            }
            yield return null;
        }
    }
    IEnumerator UpdateMaterial()
    {
        getmaterial();
        StopCoroutine("UpdateMaterial");
        yield return null;
    }
}
