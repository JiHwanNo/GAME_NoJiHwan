using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public GameObject LevelUp_effect;
    public TextMeshProUGUI Player_Lv;
    public GameObject PlayerHp;
    public GameObject PlayerMp;

    public int Lv;
    public float hp;
    public float Mp;
    public int atk;
    public int def;
    public float cri;
    public float exp_Max;


    public float exp_Cur;
    public float hp_Cur;
    public float Mp_Cur;
    float time;
    private void OnEnable()
    {
        hp_Cur = hp;
        Mp_Cur = Mp;
        Player_Lv.text = Lv.ToString();
    }

    public void LevelUp()
    {
        LevelUp_effect.SetActive(true);
        exp_Max *= 2;
        hp *= 1.1f;
        Mp *= 1.1f;
        atk += 10;
        def += 5;
        hp_Cur = hp;
        Mp_Cur = Mp;
        PlayerHp.GetComponent<Image>().fillAmount = hp_Cur / hp;
        PlayerMp.GetComponent<Image>().fillAmount = Mp_Cur / Mp;
        StartCoroutine("Effect_Time");
        Player_Lv.text = Lv.ToString();
        Manager.instance.characterMove.PlayerUI();

    }

    IEnumerator Effect_Time()
    {
        while (true)
        {
            LevelUp_effect.transform.position = transform.position;
            time += Time.fixedDeltaTime;
            if (time >= 1f)
            {
                time = 0;
                StopCoroutine("Effect_Time");
                LevelUp_effect.SetActive(false);
                break;
            }
            yield return null;
        }

    }
}
