using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    public AudioClip background;
    public AudioClip fire;
    public AudioClip damage;
    public AudioClip star;
    public AudioClip astreoid;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void GameSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
