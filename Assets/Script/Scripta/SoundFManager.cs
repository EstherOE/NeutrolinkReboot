using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundFManager : MonoBehaviour
{
    public static SoundFManager instance;

    [SerializeField] private AudioSource soundFx;
    private void Awake()
    {
        if (instance == null)
            instance = this;
      
            
    }

    public void PlaySoundSfxClip(AudioClip clip, Transform spawnTransform, float volume)

    {
        AudioSource source = Instantiate(soundFx, spawnTransform.position, Quaternion.identity);
        source.clip = clip;

        source.volume = volume;

        source.Play();

        float cliplenth = source.clip.length;
        Destroy(source.gameObject, cliplenth);


    }

}
