using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    // ������ ���
    [SerializeField] Vector3[] RespawnPoint;
    float spawnTime;

    public GameObject[] enemyPrefab;
    [SerializeField] List<GameObject> enemies;
    public bool shouldExtend = false;

    private void Awake()
    {
        // ��Ҵ� ObjPoolManager �ؿ� ������Ʈ ��ġ�� �޴´�.
        RespawnPoint = new Vector3[transform.childCount]; // manager �ؿ� ��������.

        for (int i = 0; i < RespawnPoint.Length; i++)
        {
            RespawnPoint[i] = transform.GetChild(i).position;
        }

        enemies = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        spawnTime += Time.fixedDeltaTime;
        if (spawnTime > 5f)
        {
            GameObject obj = GetEnemy();
            if (obj != null)
            {
                obj.transform.position = RespawnPoint[Random.Range(0, RespawnPoint.Length)];
                obj.SetActive(true);

            }
            spawnTime = 0f;
        }
    }
    public GameObject GetEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (!enemy.activeSelf)
            {
                return enemy;
            }
        }

        if (shouldExtend) // �����Ѵٸ�.
        {
            GameObject obj = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length-1)]);
            enemies.Add(obj);
            obj.SetActive(false);

            return obj;
        }

        return null;
    }
}
