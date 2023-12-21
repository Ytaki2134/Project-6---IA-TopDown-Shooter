using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSource; 
    public AudioClip sound; 


    private void Start()
    {
        AudioSource.volume = 0.1f;

        AudioSource.clip = sound;
        AudioSource.loop = false; 
    }

    public void PlaySound()
    {
        if (AudioSource != null && !AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
    }
    public void StopSound()
    {
        AudioSource.Stop();
    }



     // Appeler cette méthode pour diminuer progressivement le volume
     public void FadeOutSound(AudioSource audioSource, float fadeTime)
     {
         StartCoroutine(FadeOutCoroutine(audioSource, fadeTime));
     }

     private IEnumerator FadeOutCoroutine(AudioSource audioSource, float fadeTime)
     {
         float startVolume = audioSource.volume;

         while (audioSource.volume > 0)
         {
             audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
             yield return null;
         }

         audioSource.Stop();
         audioSource.volume = startVolume;
     }
}
