using UnityEngine;
using UnityEngine.UI;

public class Questdata : MonoBehaviour // ������ ������ �����ڷ� Ŭ���� ����.
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
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && !Manager.instance.questManager.questList.ContainsValue(QuestName)) // ����Ʈ �Ϸ����� �ʾҴٸ�.
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[2];

        }
        else if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess && Manager.instance.questManager.questList.ContainsValue(QuestName)) // ����Ʈ �Ϸ����� �ʾҴٸ�.
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[4];

        }
        else // �Ϸ��ߴٸ�.
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
