using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Transform QuestBox;

    public Stack<string> questList;
    public int questCount;

    private void Awake()
    {
        questList = new Stack<string>();
        GenerateData();
    }

    void GenerateData()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            QuestBox.GetChild(i).gameObject.SetActive(true);
            string text = questList.Pop();
            QuestBox.GetChild(i).GetChild(0).GetComponent<Text>().text = string.Format("{0} : {1}", text, questCount);            
        }
    }
}
