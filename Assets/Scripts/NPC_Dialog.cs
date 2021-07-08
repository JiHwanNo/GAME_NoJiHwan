using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
    public int dialogStep;

    [TextArea]
    public string[] dialog;
    //퀘스트를 받은경우. dectionary
    //퀘스틑 안받을 경우.
    public string QuestTile;
    public int goal; //퀘스트 목표
    public bool isSucess;

    private void Start()
    {
        isSucess = false;
    }
    public void Dialog()
    {
        dialogStep = Random.Range(0, 2);
        string npc_Name = GetComponent<Obj_Info>().Obj_Name;

        Manager.instance.manager_Dialog.OpenDialog(npc_Name, dialog[dialogStep]);
    }


}
