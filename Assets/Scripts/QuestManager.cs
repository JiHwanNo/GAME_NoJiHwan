using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;

    Dictionary<int, Questdata> questList;

    private void Awake()
    {
        questList = new Dictionary<int, Questdata>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10,new Questdata("NPC", new int[] { }));
    }
}
