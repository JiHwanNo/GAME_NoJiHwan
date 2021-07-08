using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Questdata : MonoBehaviour // 오케이 누르면 생성자로 클래스 생성.
{
    string QuestName;
    string NpcName;
    string QuestText;
    int goal;
    public Text Questtext;
    public Text Questtitle;



    private void OnEnable()
    {
        QuestName = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().QuestTile;
        NpcName = Manager.instance.characterMove.target.GetComponent<Obj_Info>().Obj_Name;
        if (!Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().isSucess)
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[2];
        }
        else
        {
            QuestText = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().dialog[3];
        }
        goal = Manager.instance.characterMove.target.GetComponent<NPC_Dialog>().goal;
        Questtext.text = QuestText;
        Questtitle.text = QuestName;
       
    }
   
    public void Ok_Button()
    {
        Manager.instance.questManager.questList.Push(QuestName);
        // 퀘스트 정보 오른쪽 상단 Text 생성.
        gameObject.SetActive(false);
        
    }
    public void CancelButon()
    {
        gameObject.SetActive(false);
    }
}
