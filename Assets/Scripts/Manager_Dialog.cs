using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Manager_Dialog : MonoBehaviour
{
    public GameObject dialog_Frame;
    public TextMeshProUGUI npc_Name;
    public TextMeshProUGUI npc_Dialog;

    public void OpenDialog(string npc, string dialog)
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        npc_Name.text = npc;
        npc_Dialog.text = dialog;
        dialog_Frame.SetActive(true);
    }
}
