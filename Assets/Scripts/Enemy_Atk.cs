using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Atk : MonoBehaviour
{
    EnemyState enemyState;
    PlayerState playerState;
    public Transform enemy;
    public GameObject dmgText;

    int dmg;
    float dmgRange;

    private void Awake()
    {
        enemyState = enemy.GetComponent<EnemyState>();
        playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
    }

    void CalculateDmg()
    {

        dmgRange = Random.Range(0.8f, 1.2f);
        dmg = (int)((enemyState.atk - playerState.def) * dmgRange);

        int critical = Random.Range(0, 100);
        if (critical < enemyState.cri * 100)
        {
            dmgRange = Random.Range(2f, 2.5f);
        }
    }

    void Hit(int _dmg)
    {
        playerState.hp_Cur -= _dmg;

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Vector3 hitPoint = (other.transform.position + transform.position) * 0.5f;

            CalculateDmg();

            dmgText.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
            dmgText.GetComponent<TextMeshProUGUI>().fontSize = 50 * dmgRange;
            dmgText.transform.position = Manager.instance.characterMove.mycamera.WorldToScreenPoint(hitPoint + new Vector3(0, 1, 0));
            dmgText.SetActive(true);
            Hit(dmg);
            Manager.instance.characterMove.PlayerUI();

        }
    }

}
