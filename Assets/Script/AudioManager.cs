using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip Slain;

    public AudioClip Over;
    [Header ("Jump")]
    public AudioClip Jump1;
    public AudioClip Jump2;
    public AudioClip Jump3;

    [Header ("Coin")]
    public AudioClip Coin1;
    
    public void JumpSound(){
        int randomnumber = Random.Range(1, 3);

        switch(randomnumber){
            case 1 :    audioSource.clip = Jump1;
                        audioSource.Play();
                break;
            case 2 :    audioSource.clip = Jump2;
                        audioSource.Play();
                break;
            case 3 :    audioSource.clip = Jump3;
                        audioSource.Play();
                break;
            default :   audioSource.clip = Jump1;
                        audioSource.Play();
                break;
        }
    }

    public void CoinSound(){
        audioSource.clip = Coin1;
        audioSource.Play();
    }

    public void SlainSound(){
        audioSource.clip = Slain;
        audioSource.Play();
    }

    public void OverSound(){
        audioSource.clip = Over;
        audioSource.Play();
    }


}

