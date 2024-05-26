using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
       audioSource.volume = 1.5f;
    }

    public void PlayStart()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
