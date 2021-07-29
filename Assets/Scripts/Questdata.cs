using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Questdata : MonoBehaviour // 오케이 누르면 생성자로 클래스 생성.
{
    [Header("Quest Info")]
    string QuestName;
    string QuestText;
    int goal;
    int currentGoal;

    [Header("Quest UI")]
    public Text Questtext;
    public Text Questtitle;
    public GameObject GetQuestItem;

    PlayerState playerState;
    Transform TextBox;
    private void Awake()
    {
        playerState = Manager.instance.characterMove.Player.GetComponent<PlayerState>();
        TextBox = Manager.instance.questManager.QuestBox;
    }
    private void OnEnable()
    {
        goal = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().goal;
        QuestName = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().QuestTile;
        // 목표치 받기.
        if (Manager.instance.questManager.questList.ContainsValue(QuestName))
        {
            for (int i = 0; i < Manager.instance.questManager.QuestBox.childCount; i++)
            {
                if (Manager.instance.questManager.QuestBox.GetChild(i).GetChild(0).GetComponent<Text>().text.Contains(QuestName))
                {
                    string temp = Manager.instance.questManager.QuestBox.GetChild(i).GetChild(1).GetComponent<Text>().text;
                    currentGoal = Int32.Parse(temp);
                }
            }
        }

        // 퀘스트 완료 X / 퀘스트를 받음 / 퀘스트 조건이 충족되지 않음..
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName) && goal > currentGoal)
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[4];

        }
        //퀘스트 완료 X / 퀘스트 받음 / 퀘스트 조건이 충족됨.
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName) && goal <= currentGoal)
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[5];
        }
        // 퀘스트 완료 x / 퀘스트를 받지 않음.
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && !Manager.instance.questManager.questList.ContainsValue(QuestName))
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[2];

        }

        // 퀘스트 완료 했고, 퀘스트를 받았음 
        else if (Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName))
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[3];
        }
        Questtext.text = QuestText;
        Questtitle.text = QuestName;

    }

    public void Ok_Button()
    {
        if (!Manager.instance.questManager.questList.ContainsValue(QuestName)) // 퀘스트를 안받았을때.
        {
            Manager.instance.questManager.GetQuest(QuestName, goal);
        }
        else if (Manager.instance.questManager.questList.ContainsValue(QuestName) && goal <= currentGoal)
        {
            FinshQuest();
            for (int i = 0; i < Manager.instance.questManager.QuestBox.childCount; i++)
            {
                if (TextBox.GetChild(i).GetChild(0).GetComponent<Text>().text.Contains(QuestName))
                {
                    TextBox.GetChild(i).gameObject.SetActive(false);
                    Manager.instance.questManager.questList.Remove(i);
                }
            }

        }
        gameObject.SetActive(false);
    }
    public void CancelButon()
    {
        gameObject.SetActive(false);
    }

    void FinshQuest()
    {
        GetQuestItem.SetActive(true);
        //퀘스트 완료. 보상받기.
        Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess = true;
        GetQuestItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = string.Format("Coin : {0}", Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().GetCoin.ToString());
        GetQuestItem.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = string.Format("Exp : {0}", Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().GetExp.ToString());

        Manager.instance.inventory.gold += Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().GetCoin;
        Manager.instance.inventory.GetInvenInfo();
        playerState.exp_Cur += Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().GetExp;
        while (playerState.exp_Cur >= playerState.exp_Max)
        {
            playerState.LevelUp();
        }
    }
}
