using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    // 리스폰 장소
    [SerializeField] Vector3[] RespawnPoint;
    float spawnTime;

    public GameObject[] enemyPrefab;
    [SerializeField] List<GameObject> enemies;

    public GameObject Boss;

    public GameObject[] ItemPrefabs;
    public Dictionary<string, GameObject> Items;
    public int getItemCount;

    public GameObject dmgtext;
    List<GameObject> dmgtexts;
    public Transform dmgtextparent;

    public GameObject effect;
    List<GameObject> effects;
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
        dmgtexts = new List<GameObject>();
        effects = new List<GameObject>();
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
    //적 생성.
    public GameObject GetEnemy()
    {
        if (enemies.Count > 10)
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
            obj.GetComponent<EnemyState>().cur_Hp = obj.GetComponent<EnemyState>().hp;
            enemies.Add(obj); 
            obj.SetActive(false);

            return obj;
        }
    }

    public GameObject GetItem()
    {
        // List 받고. 아이템 체크를 dectionary
        getItemCount = Random.Range(1, 6); // 드랍 아이템 갯수.
        int temprandom = Random.Range(0, ItemPrefabs.Length - 1);
        GameObject obj = Instantiate(ItemPrefabs[temprandom]); // 아이템 생성   
        obj.name = ItemPrefabs[temprandom].name;
        obj.GetComponentInChildren<TextMeshProUGUI>().text = getItemCount.ToString(); // 아이템 갯수 생성.

        string objname = obj.name;
        if (!Items.ContainsKey(objname)) // 중복 아이템 방지
        {
            Items.Add(obj.name, obj);
            return obj;
        }
        GetItem();

        return null;
    }
    public GameObject GetdmgText()
    {
        foreach (var temptext in dmgtexts)
        {
            if(!temptext.activeSelf)
            {
                return temptext;
            }
        }
        GameObject text = Instantiate(dmgtext, dmgtext.transform.parent);
        dmgtexts.Add(text);
        text.SetActive(false);

        return text;

    }
    public GameObject GetEffect()
    {
        foreach (var effect in effects)
        {
            if (!effect.activeSelf)
            {
                return effect;
            }
        }
        GameObject obj = Instantiate(effect, effect.transform.parent);
        dmgtexts.Add(obj);
        obj.SetActive(false);

        return obj;

    }
    public void ClearItem()
    {
        Items.Clear();
    }
}
