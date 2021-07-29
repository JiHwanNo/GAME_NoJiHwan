using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Questdata : MonoBehaviour // ������ ������ �����ڷ� Ŭ���� ����.
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
        // ��ǥġ �ޱ�.
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

        // ����Ʈ �Ϸ� X / ����Ʈ�� ���� / ����Ʈ ������ �������� ����..
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName) && goal > currentGoal)
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[4];

        }
        //����Ʈ �Ϸ� X / ����Ʈ ���� / ����Ʈ ������ ������.
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName) && goal <= currentGoal)
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[5];
        }
        // ����Ʈ �Ϸ� x / ����Ʈ�� ���� ����.
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && !Manager.instance.questManager.questList.ContainsValue(QuestName))
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[2];

        }

        // ����Ʈ �Ϸ� �߰�, ����Ʈ�� �޾��� 
        else if (Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName))
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[3];
        }
        Questtext.text = QuestText;
        Questtitle.text = QuestName;

    }

    public void Ok_Button()
    {
        if (!Manager.instance.questManager.questList.ContainsValue(QuestName)) // ����Ʈ�� �ȹ޾�����.
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
        //����Ʈ �Ϸ�. ����ޱ�.
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
