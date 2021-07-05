using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public float speed_Noncombat;
    public float speed_Combat;

    public int moveRange;
    public int trackingRange;

    public bool inCombat;
    bool inAtk;

    NavMeshAgent Nav;
    Animator enemyAni;
    EnemyState enemyState;

    bool onMove;
    Vector3 originPosition;
    float originDis;
    Vector3 target;
    float targetDis;

    private void OnEnable()
    {
        originPosition = transform.position;
        Nav = GetComponent<NavMeshAgent>();
        enemyAni = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();

        inCombat = true;
        StartCoroutine("EnemyAI");

    }
    //전투중 움직임 실행
    void Move_Combat()
    {
        target = Manager.instance.characterMove.Player.position;
        if (enemyState.cur_Hp <= 0)
        {
            Nav.speed = 0;
            Nav.SetDestination(Vector3.zero);
        }

        if (!inAtk && enemyState.cur_Hp > 0)
        {
            Nav.speed = speed_Combat;
            Nav.SetDestination(target);

            if (targetDis <= 3)
            {
                inAtk = true;
                enemyAni.Play("Idle");
            }
        }

        if (originDis >= trackingRange)
        {
            inCombat = false;
            onMove = true;
            target = originPosition;
            Nav.speed = 8;
            Nav.SetDestination(target);

            GetComponent<EnemyState>().cur_Hp = GetComponent<EnemyState>().hp;
        }
    }
    void Move_NonCombat()
    {
        if (targetDis <= 2)
        {
            onMove = false;
        }
        if (!onMove)
        {
            onMove = true;
            Nav.speed = speed_Noncombat;

            target = new Vector3(transform.position.x + Random.Range(-1 * moveRange, moveRange), 0,
                transform.position.z + Random.Range(-1 * moveRange, moveRange));
            
            Nav.SetDestination(target);
        }
        if (originDis >= moveRange)
        {
            onMove = true;
            target = originPosition;
            Nav.SetDestination(target);
        }
        
    }

    public void Enemy_Atk()
    {
        if (targetDis <= 2)
        {
            transform.LookAt(target);
            enemyAni.Play("EnemyAtk");
        }
        if (targetDis > 2)
        {
            inAtk = false;
            enemyAni.Play("Idle");
        }
    }
    IEnumerator EnemyAI()
    {

        while (true)
        {
            originDis = (originPosition - transform.position).magnitude;
            targetDis = (target - transform.position).magnitude;
            float speed = Nav.desiredVelocity.magnitude;
            enemyAni.SetFloat("Speed", speed);

            if (!inCombat)
            {
                Move_NonCombat();
            }
            if (inCombat)
            {
                Move_Combat();
            }
            yield return null;
        }
    }

}
