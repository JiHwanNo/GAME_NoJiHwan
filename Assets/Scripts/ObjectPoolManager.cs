using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    [SerializeField] List<GameObject> enemies;
    public bool shouldExtend = false;

}
