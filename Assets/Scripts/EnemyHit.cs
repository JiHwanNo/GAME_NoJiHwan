using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    float time;

    public void Hit(int dmg)
    {
        GetComponent<Enemy_AI>().inCombat = true;
        EnemyState enemyState = GetComponent<EnemyState>();
        enemyState.cur_Hp -= dmg;

        Manager.instance.characterMove.hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = enemyState.cur_Hp / enemyState.hp;

        if (enemyState.cur_Hp <= 0)
        {
            Manager.instance.characterMove.GetPlayerExp();
            if (Manager.instance.characterMove.target == transform)
            {
                Manager.instance.characterMove.target = null;
                Manager.instance.characterMove.target_Tool.SetActive(false);
            }
            GetComponent<Animator>().Play("Death");
            gameObject.tag = "Dead";
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
                StopCoroutine("SetActity");
                break;
            }
            yield return null;
        }
    }
}
