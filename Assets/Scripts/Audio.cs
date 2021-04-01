using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    //Lance un clip audio lorsqu'appelé
    public void AudioActivation(AudioSource audioSource)
    {
        audioSource.Play;
    }
}
