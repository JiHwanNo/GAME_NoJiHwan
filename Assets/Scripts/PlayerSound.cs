using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void PlaySound(AudioClip clip)
    {
        Manager.instance.myaudio.audioSource.PlayOneShot(clip);
    }
}
