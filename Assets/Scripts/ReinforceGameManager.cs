using System.Collections;
using UnityEngine;

public class ReinforceGameManager : MonoBehaviour
{
    GameObject RangeBar;
    GameObject Trigger;
    int ReinforceLv;
    int speed;
    Vector3 Range;
    bool IsTrigger;

    public GameObject iteminfoFrame;
    public GameObject success;
    public GameObject fail;
    ReinforceFrame reinforce;

    private void Start()
    {
        reinforce = Manager.instance.reinforce_Manager.ReinforceFrame.GetComponent<ReinforceFrame>();
    }
    private void OnEnable()
    {
        iteminfoFrame.SetActive(false);
        ReinforceLv = Manager.instance.reinforce_Manager.Item.GetComponent<Items_Info>().ItemLv;
        RangeBar = transform.GetChild(1).gameObject;
        Trigger = transform.GetChild(2).gameObject;

        RangeBar.SetActive(true);

        speed = 2 * (Manager.instance.reinforce_Manager.Item.GetComponent<Items_Info>().ItemLv + 1);

        StartCoroutine("GameStart");
    }

    private void OnDisable()
    {
        Manager.instance.reinforce_Manager.Item_Slot.GetChild(0).gameObject.SetActive(false);
        reinforce.MaterialCoin.GetChild(0).gameObject.SetActive(false);
        reinforce.MaterialSlot.GetChild(0).gameObject.SetActive(false);
        reinforce.MaterialSlot.GetChild(1).gameObject.SetActive(false);
        StopAllCoroutines();
    }
    public void StopButton()
    {
        success.SetActive(false);
        fail.SetActive(false);

        IsTrigger = Trigger.GetComponent<TriggerManager>().trigger;
        if (IsTrigger)
        {
            Manager.instance.reinforce_Manager.Item.GetComponent<Items_Info>().ItemLevelUp();
            Range = RangeBar.transform.localScale;
            Range.x -= (float)0.05 * ReinforceLv;
            RangeBar.transform.localScale = Range;
            success.SetActive(true);
        }
        else
        {
            fail.SetActive(true);
        }
        StartCoroutine("CloseFrame");
    }

    
    IEnumerator GameStart()
    {
    reStart:
        while (Trigger.transform.localPosition.x < 175f)
        {
            Vector3 StartPosition = Trigger.transform.localPosition;
            StartPosition.x += 250 * speed * Time.fixedDeltaTime;
            Trigger.transform.localPosition = StartPosition;
            yield return null;
        }
        while (Trigger.transform.localPosition.x > -175f)
        {
            Vector3 StartPosition = Trigger.transform.localPosition;
            StartPosition.x -= 250 * speed * Time.fixedDeltaTime;
            Trigger.transform.localPosition = StartPosition;

            yield return null;
        }
        if (Trigger.transform.localPosition.x < -175f)
        {
            goto reStart;
        }
    }
    IEnumerator CloseFrame()
    {
        float temptime = 0;
        while (true)
        {
            temptime += Time.fixedDeltaTime;
            if (temptime >= 0.5f)
            {
                break;
            }
            yield return null;
        }
        gameObject.SetActive(false);
        StopCoroutine("CloseFrame");
    }

}
