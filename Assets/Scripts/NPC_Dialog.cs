using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
    public int dialogStep;

    [TextArea]
    public string[] dialog;
    //����Ʈ�� �������. dectionary
    //�����z �ȹ��� ���.



    public void Dialog()
    {
        dialogStep = Random.Range(0, 2);
        string npc_Name = GetComponent<Obj_Info>().Obj_Name;

        Manager.instance.manager_Dialog.OpenDialog(npc_Name, dialog[dialogStep]);
    }


}
