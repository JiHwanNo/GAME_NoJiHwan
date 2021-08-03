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
    public Animator BossAni;
    EnemyState enemyState;

    bool onMove;
    Vector3 originPosition;
    float originDis;
    Vector3 target;
    float targetDis;
    [Header("NomalAttack")]
    Transform attack;
    CharacterMove Character;

    [Header("Pettern 1")]
    GameObject Summoner;
    List<GameObject> Summoners;
    int RandomRange;

    [Header("Pattern 2")]
    GameObject Bullet;
    List<GameObject> Bullets;
    GameObject[] BulletPrfab;
    public bool PatternOn;
    int bulletCount;
    private void Awake()
    {
        Bullets = new List<GameObject>();
        Nav = GetComponent<NavMeshAgent>();
        BossAni = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
        Summoners = new List<GameObject>();
        Character = Manager.instance.characterMove;
    }
    private void Start()
    {
        RandomRange = 5;
        bulletCount = 30;
        BulletPrfab = new GameObject[bulletCount];
    }
    private void OnEnable()
    {
        originPosition = transform.position;

        Summoner = Manager.instance.ObjectPool.enemyPrefab[0];
        Bullet = transform.GetChild(4).gameObject;
        attack = transform.GetChild(3);
        inCombat = false;
        inAtk = false;
        onMove = false;
        Isdead = false;
        PatternOn = false;

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
                BossAni.Play("Idle");
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
        int BossHp = (int)enemyState.cur_Hp;
        int BossPattern_percentage = Random.Range(0, 100);
        if (targetDis <= 5f)
        {
            transform.LookAt(target);
            BossAni.Play("Boss_Attack");
            if (BossPattern_percentage < 10 || BossPattern_percentage > 90)
            {
                Boss_Pattern(BossHp);
            }
            else
            {
                attack.position = transform.position + new Vector3(0, 2, 0);
                attack.GetComponent<BossAttack>().dir = Character.Player.position + new Vector3(0, 2, 0);
                attack.gameObject.SetActive(true);
            }
        }
        if (targetDis > 5f)
        {
            inAtk = false;
            BossAni.Play("Idle");
        }
    }

    IEnumerator Boss_AI()
    {
        while (true)
        {
            originDis = (originPosition - transform.position).magnitude;
            targetDis = (target - transform.position).magnitude;
            float speed = Nav.desiredVelocity.magnitude;
            BossAni.SetFloat("Speed", speed);

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

    void Boss_Pattern(int BossCurrent)
    {
        PatternOn = true;
        // 소환, 퍼지는 공격, 유도탄.
        if (BossCurrent > 4000)
        {
            recall_Summoner();
        }
        else if (BossCurrent > 2000 && BossCurrent <= 4000)
        {
            Spread_Bullet();
        }
        else if (BossCurrent > 0 && BossCurrent <= 2000)
        {
            // 유도탄
        }
    }

    void recall_Summoner()
    {
        CheckObject();
    }

    // 패턴 1 소환수 얻기.
    void CheckObject()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = GetObject();
            if (obj != null)
            {
                EnemyState enemy = obj.GetComponent<EnemyState>();
                obj.transform.position = transform.position + new Vector3(Random.Range(-1 * RandomRange, RandomRange), 0, Random.Range(-1 * RandomRange, RandomRange));
                enemy.atk /= 4;
                enemy.exp /= 3;
                enemy.cur_Hp /= 3;

                obj.SetActive(true);
                obj.GetComponent<Enemy_AI>().inCombat = true;

            }
        }

        PatternOn = false;

    }
    GameObject GetObject()
    {
        if (Summoners.Count > 2)
        {
            return null;
        }
        else
        {
            foreach (var enemy in Summoners)
            {
                if (!enemy.activeSelf)
                {
                    return enemy;
                }
            }

            GameObject obj = Instantiate(Summoner);
            obj.GetComponent<EnemyState>().cur_Hp = obj.GetComponent<EnemyState>().hp;
            Summoners.Add(obj);
            obj.SetActive(false);

            return obj;
        }
    }

    // 패턴 2 총알 퍼지기
    void Spread_Bullet()
    {
        BossAni.Play("Boss_Attack");
        CheckBullet();
    }
    // 오브젝트 풀로 bullet 받고. 이동 시키고.
    void CheckBullet()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            BulletPrfab[i] = GetBullet();
            BulletPrfab[i].transform.position = transform.position + new Vector3(0,2,0);
            Vector3 Randomdir = new Vector3(Random.Range(-365, 365), 0, Random.Range(-365, 365));
            BulletPrfab[i].GetComponent<BossAttack>().dir = Randomdir;

            BulletPrfab[i].SetActive(true);
        }

    }
    GameObject GetBullet()
    {
        if (Bullets.Count > bulletCount)
        {
            return null;
        }
        else
        {
            foreach (var enemy in Bullets)
            {
                if (!enemy.activeSelf)
                {
                    return enemy;
                }
            }

            GameObject obj = Instantiate(Bullet);
            obj.transform.SetParent(transform);
            Bullets.Add(obj);
            obj.SetActive(false);

            return obj;
        }
    }
  
}

