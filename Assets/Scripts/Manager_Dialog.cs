using TMPro;
using UnityEngine;
public class Manager_Dialog : MonoBehaviour
{
    public GameObject dialog_Frame;
    public TextMeshProUGUI npc_Name;
    public TextMeshProUGUI npc_Dialog;

    [Header("Talk")]
    public GameObject BuyButton;
    public GameObject ReinforceButton;
    public GameObject YesButton;
    public GameObject CancelButton;
    public GameObject Talk_Button;
    public void OpenDialog(string npc, string dialog)
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);

        npc_Name.text = npc;
        npc_Dialog.text = dialog;
        dialog_Frame.SetActive(true);
    }

    public void TalkButton()
    {
        dialog_Frame.SetActive(false);
        if (BuyButton.activeSelf)
        {
            BuyButton.SetActive(false);
            ReinforceButton.SetActive(false);
            Talk_Button.SetActive(false);
        
        }
    }
}
