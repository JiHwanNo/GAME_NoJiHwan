using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public float speed_Noncombat;
    public float speed_Combat;

    public int moveRange;
    public int trackingRange;

    public bool inCombat;
    bool inAtk;
    public bool Isdead;
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
        Isdead = false;
        originPosition = transform.position;
        Nav = GetComponent<NavMeshAgent>();
        enemyAni = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
        inCombat = false;
        inAtk = false;
        onMove = false;

        StartCoroutine("Boss_AI");

    }
    //전투중 움직임 실행
    void Move_Combat()
    {
        target = Manager.instance.characterMove.Player.position;
        if (enemyState.cur_Hp <= 0)
        {
            StopCoroutine("Boss_AI");
            Nav.speed = 0;
            Isdead = true;
            Nav.SetDestination(transform.position);

        }

        if (!inAtk && enemyState.cur_Hp > 0)
        {
            Nav.speed = speed_Combat;
            Nav.SetDestination(target);

            if (targetDis <= 4f)
            {
                Nav.SetDestination(transform.position);
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
        if (targetDis <= 4f)
        {
            onMove = false;

        }
        if (!onMove || Nav.speed == 0)
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

    public void Boss_Atk() // 공격이 일어날때. 에니메이션에서 실행시켜준다.
    {
        
        if (targetDis <= 5f)
        {
            transform.LookAt(target);
            enemyAni.Play("Boss_Attack");
        }
        if (targetDis > 5f)
        {
            inAtk = false;
            enemyAni.Play("Idle");
        }
    }
    IEnumerator Boss_AI()
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

