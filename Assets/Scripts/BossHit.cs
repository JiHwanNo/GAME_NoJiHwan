using System.Collections;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    float time;
    CharacterMove character;

    private void Start()
    {
        time = 0;
        character = Manager.instance.characterMove;
    }
    public void Hit(int dmg)
    {
        GetComponent<BossAI>().inCombat = true;
        EnemyState BossState = GetComponent<EnemyState>();
        BossState.cur_Hp -= dmg;

        character.Boss_Targeting(transform);

        if (BossState.cur_Hp <= 0)
        {
            if (character.target == transform)
            {
                character.target = null;
                character.target_Tool.SetActive(false);
                character.taget_Boss.SetActive(false);
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
            if (time > 5f)
            {
                time = 0;
                gameObject.SetActive(false);
                gameObject.tag = "Boss";
                transform.GetComponent<BoxCollider>().isTrigger = false;
                StopCoroutine("SetActity");
                break;
            }
            yield return null;
        }
    }
}

