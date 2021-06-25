using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPoolManager : MonoBehaviour
{
    // 리스폰 장소
    [SerializeField] Vector3[] RespawnPoint;
    float spawnTime;

    public GameObject[] enemyPrefab;
    [SerializeField] List<GameObject> enemies;

    public GameObject[] ItemPrefabs;
    [SerializeField]Dictionary<string, GameObject> Items;

    private void Awake()
    {
        // 장소는 ObjPoolManager 밑에 오브젝트 위치를 받는다.
        RespawnPoint = new Vector3[transform.childCount]; // manager 밑에 생성하자.

        //리스폰 위치를 받는다.
        for (int i = 0; i < RespawnPoint.Length; i++)
        {
            RespawnPoint[i] = transform.GetChild(i).position;
        }

        enemies = new List<GameObject>();

        Items = new Dictionary<string, GameObject>();
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
        if(enemies.Count >10)
        {
            return null;
        }
        else
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.activeSelf)
                {
                    return enemy;
                }
            }

            GameObject obj = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length - 1)]);
            enemies.Add(obj);
            obj.SetActive(false);

            return obj;
        }
    }

    public GameObject GetItem()
    {
        GameObject obj = Instantiate(ItemPrefabs[Random.Range(0, ItemPrefabs.Length - 1)]);
        Items.Add(obj.name, obj);
        obj.SetActive(true);

        return obj;
    }
}
