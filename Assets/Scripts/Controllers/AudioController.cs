using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayDeathSound() {
        audioSource.Play();
    }
}
