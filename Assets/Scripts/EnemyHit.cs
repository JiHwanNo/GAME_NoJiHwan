using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    float time;
    public bool is_get;
    [Header("Player")]
    CharacterMove  character;
    PlayerState playerState;

    [Header("Self Info")]
    EnemyState enemyState;
    Obj_Info obj_Info;

    [Header("Quest")]
    Hashtable questlist;
    Transform qusetbox;

    BoxCollider boxCollider;
    private void Start()
    {
        character = Manager.instance.characterMove;
        playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
        enemyState = GetComponent<EnemyState>();
        obj_Info = GetComponent<Obj_Info>();
        questlist = Manager.instance.questManager.questList;
        qusetbox = Manager.instance.questManager.QuestBox;
        boxCollider = GetComponent<BoxCollider>();
        is_get = false;
    }
    public void Hit(int dmg)
    {
        GetComponent<Enemy_AI>().inCombat = true;
        enemyState.cur_Hp -= dmg;

        character.hpBar_Target.transform.GetChild(0).GetComponent<Image>().fillAmount = enemyState.cur_Hp / enemyState.hp;

        if (enemyState.cur_Hp <= 0)
        {
            GetPlayerExp();
            if (character.target == transform)
            {
                character.target = null;
                character.target_Tool.SetActive(false);
            }
            GetComponent<Animator>().Play("Death");
            gameObject.tag = "Dead";
            boxCollider.isTrigger = true;
            StartCoroutine("SetActity");

        }
    }
    IEnumerator SetActity()
    {
        while (true)
        {
            if(is_get)
            {
                gameObject.SetActive(false);
                gameObject.tag = "Enemy";
                boxCollider.isTrigger = false;
                StopCoroutine("SetActity");
                break;
            }
            time += Time.fixedDeltaTime;
            if (time > 5)
            {
                time = 0;
                gameObject.SetActive(false);
                gameObject.tag = "Enemy";
                boxCollider.isTrigger = false;
                StopCoroutine("SetActity");
                break;
            }
            yield return null;
        }
    }
    public void GetPlayerExp()
    {
        playerState.exp_Cur += enemyState.exp;

        if (playerState.exp_Cur >= playerState.exp_Max)
        {
            playerState.LevelUp();
        }
        //퀘스트 중 목표 얻기.
        if (obj_Info.Obj_Name == "Skeleton" && questlist.ContainsValue("Get Skeleton Born"))
        {
            character.Skeleton++;
            for (int i = 0; i < questlist.Count; i++)
            {
                if (qusetbox.GetChild(i).GetChild(0).GetComponent<Text>().text.Contains("Get Skeleton Born"))
                {
                    qusetbox.GetChild(i).GetChild(1).GetComponent<Text>().text = character.Skeleton.ToString();

                }
            }
        }
    }
}
