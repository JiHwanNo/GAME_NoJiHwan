using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    float time;
    CharacterMove  character;

    private void Start()
    {
        character = Manager.instance.characterMove;
    }
    public void Hit(int dmg)
    {
        GetComponent<Enemy_AI>().inCombat = true;
        EnemyState enemyState = GetComponent<EnemyState>();
        enemyState.cur_Hp -= dmg;

        character.hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = enemyState.cur_Hp / enemyState.hp;

        if (enemyState.cur_Hp <= 0)
        {
            character.GetPlayerExp();
            if (character.target == transform)
            {
                character.target = null;
                character.target_Tool.SetActive(false);
            }
            GetComponent<Animator>().Play("Death");
            gameObject.tag = "Dead";
            transform.GetComponent<BoxCollider>().isTrigger = true;
            StartCoroutine("SetActity");

        }
    }
    IEnumerator SetActity()
    {
        while (true)
        {
            time += Time.fixedDeltaTime;
            if (time > 5)
            {
                time = 0;
                gameObject.SetActive(false);
                gameObject.tag = "Enemy";
                transform.GetComponent<BoxCollider>().isTrigger = false;
                StopCoroutine("SetActity");
                break;
            }
            yield return null;
        }
    }
}
