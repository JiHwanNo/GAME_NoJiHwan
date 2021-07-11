using UnityEngine;
using UnityEngine.UI;

public class Questdata : MonoBehaviour // 오케이 누르면 생성자로 클래스 생성.
{
    string QuestName;
    string QuestText;
    int goal;
    public Text Questtext;
    public Text Questtitle;
    public GameObject Warming;
   

    private void OnEnable()
    {
        QuestName = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().QuestTile;
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && !Manager.instance.questManager.questList.ContainsValue(QuestName)) // 퀘스트 완료하지 않았다면.
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[2];

        }
        else if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName)) // 퀘스트 완료하지 않았다면.
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[4];

        }
        else // 완료했다면.
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[3];
        }
        goal = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().goal;
        Questtext.text = QuestText;
        Questtitle.text = QuestName;

    }

    
    public void Ok_Button()
    {
        if(!Manager.instance.questManager.questList.ContainsValue(QuestName))
        {
            Manager.instance.questManager.GetQuest(QuestName, goal);
        }
        else if (Manager.instance.questManager.questList.ContainsValue(QuestName) && Manager.instance.questManager.QuestBox.GetChild(2).gameObject.activeSelf)
        {
            Warming.SetActive(true);
        }

        gameObject.SetActive(false);
    }
    public void CancelButon()
    {
        gameObject.SetActive(false);
    }
}
