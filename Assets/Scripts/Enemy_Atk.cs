using TMPro;
using UnityEngine;

public class Enemy_Atk : MonoBehaviour
{
    EnemyState enemyState;
    Enemy_AI enemy_AI;
    PlayerState playerState;
    public Transform enemy;
    GameObject dmgText;

    int dmg;
    float dmgRange;

    private void Awake()
    {
        dmgText = Manager.instance.uI_Manager.Eenemy_Text;
        enemyState = enemy.GetComponent<EnemyState>();
        playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
        enemy_AI = enemy.GetComponent<Enemy_AI>();
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

        if (other.gameObject.tag == "Player" && enemy_AI.inCombat)
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
