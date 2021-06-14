using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoFrame : MonoBehaviour
{
    public PlayerState player;

    public TextMeshProUGUI lev;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI cri;

    public Image expBar;

    private void OnEnable()
    {
        lev.text = player.Lv.ToString();
        hp.text = player.hp.ToString();
        atk.text = player.atk.ToString();
        def.text = player.def.ToString();
        cri.text = string.Format("{0:0.00}", player.cri);

        expBar.fillAmount = player.exp_Cur / player.exp_Max;
    }
}
