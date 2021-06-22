using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Atk : MonoBehaviour
{
    public Transform enemy;
    
    int dmg;
    float dmgRange;
    public GameObject dmgText;

    void CalculateDmg()
    {
        EnemyState enemyState = enemy.GetComponent<EnemyState>();

        dmgRange = Random.Range(0.8f, 1.2f);
        dmg = (int)(enemyState.atk * dmgRange);

        int critical = Random.Range(0, 100);
        if (critical < enemyState.cri * 100)
        {
            dmgRange = Random.Range(2f, 2.5f);
        }
    }

    void Hit(int _dmg)
    {
        PlayerState playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
        playerState.hp_Cur -= _dmg;
        Debug.Log(playerState.hp_Cur);
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
           
        }
    }

}
