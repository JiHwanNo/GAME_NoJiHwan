using System.Collections;
using UnityEngine;

public class ReinforceGameManager : MonoBehaviour
{
    GameObject RangeBar;
    GameObject Trigger;
    int ReinforceLv;

    Vector3 Range;
    private void OnEnable()
    {
        ReinforceLv = Manager.instance.reinforce_Manager.Item.GetComponent<Items_Info>().ItemLv;
        RangeBar = transform.GetChild(1).gameObject;
        Trigger = transform.GetChild(2).gameObject;

        Range = RangeBar.transform.localScale;
        Range.x -= (float)0.1 * ReinforceLv;
        RangeBar.transform.localScale = Range;
        RangeBar.SetActive(true);

        StartCoroutine("GameStart");
    }

    public void StopButton()
    {
        StopCoroutine("GameStart");
    }

    IEnumerator GameStart()
    {
    reStart:
        while (Trigger.transform.localPosition.x < 175f)
        {
            Vector3 StartPosition = Trigger.transform.localPosition;
            int speed;
            speed = 2 * (Manager.instance.reinforce_Manager.Item.GetComponent<Items_Info>().ItemLv + 1);
            StartPosition.x += 250 * speed * Time.fixedDeltaTime;
            Trigger.transform.localPosition = StartPosition;
            yield return null;
        }
        while (Trigger.transform.localPosition.x > -175f)
        {
            Vector3 StartPosition = Trigger.transform.localPosition;
            int speed;
            speed = 2 * (Manager.instance.reinforce_Manager.Item.GetComponent<Items_Info>().ItemLv + 1);
            StartPosition.x -= 250 * speed * Time.fixedDeltaTime;
            Trigger.transform.localPosition = StartPosition;
            yield return null;
        }
        if (Trigger.transform.localPosition.x < -175f)
        {
            goto reStart;
        }
    }

}
