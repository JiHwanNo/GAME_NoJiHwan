using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
    int dialogStep;

    [TextArea]
    public string[] dialog;

    public void Dialog()
    {
        string npc_Name = GetComponent<Obj_Info>().Obj_Name;
        Manager.instance.manager_Dialog.OpenDialog(npc_Name, dialog[Random.Range(0,2)]);
    }
}
