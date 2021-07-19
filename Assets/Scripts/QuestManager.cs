using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public Transform QuestBox;
    Transform[] QuestBox_Childs;
    public string[] QuestName;
    int questCount;

    public Hashtable questList;
    public GameObject Warming;
    private void Awake()
    {
        questList = new Hashtable();
        QuestName = new string[questCount];
        QuestBox_Childs = new Transform[QuestBox.childCount];
    }
    private void Start()
    {
        questCount = 100;
        for (int i = 0; i < QuestBox.childCount; i++)
        {
            QuestBox_Childs[i] = QuestBox.GetChild(i);
        }
    }

    public void GetQuest(string questText, int goal)
    {
        //퀘스트 창 비어있는지 체크.
        for (int i = 0; i < QuestBox.childCount; i++)
        {
            if (!QuestBox_Childs[i].gameObject.activeSelf)
            {
                questCount = i;
                break;
            }
        }

        //퀘스트 리스트에 아무것도 없고, 퀘스트 창이 비어있다면.
        if (!questList.ContainsValue(questText) && questCount != 100)
        {
            questList.Add(questCount, questText);
            QuestBox_Childs[questCount].GetChild(0).GetComponent<Text>().text = questText;
            QuestBox_Childs[questCount].GetChild(1).GetComponent<Text>().text = Manager.instance.characterMove.Skeleton.ToString();
            QuestBox_Childs[questCount].GetChild(2).GetComponent<Text>().text = string.Format("/ {0}", goal.ToString());
            QuestBox_Childs[questCount].gameObject.SetActive(true);
        }

        else if (!questList.ContainsValue(questText) && questCount == 100)
        {
            Warming.SetActive(true);
        }
        //초기값.
        questCount = 100;
    }

}
