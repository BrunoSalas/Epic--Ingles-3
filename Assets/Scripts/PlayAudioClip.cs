using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    public AudioSource audioSourceGeneral;

    public void PlayAudio(AudioClip clip)
    {
        audioSourceGeneral.Stop();
        audioSourceGeneral.clip = clip;
        audioSourceGeneral.Play();
    }
}
