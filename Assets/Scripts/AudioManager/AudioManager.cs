using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{

    [Header("--------------- AudioSource ----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------------- AudioClip ----------------")]
    public AudioClip background;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip doublejump;
    public AudioClip fall;
    public AudioClip buttonclick;
    public AudioClip menuopen;
    public AudioClip collectitem;
    public AudioClip grappling;
    public AudioClip freeze;
    public AudioClip melting;
    public AudioClip crackingIce;
    public AudioClip mealtingclick;
    public AudioClip shieldactived;
    public AudioClip shieldbroken;
    public AudioClip rocketExplosion;
    public AudioClip laucher;





    /*    public AudioClip ghost;*/


    private bool isWalking;
  

    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
            SFXSource.PlayOneShot(clip);
    }

    public void PlayWalk()
    {
        if (!isWalking)
        {
            SFXSource.clip = walk;
            SFXSource.Play();
            isWalking = true;
        }
    }

    public void StopWalk()
    {
        if (isWalking)
        {
            SFXSource.Stop();
            isWalking = false;
        }
    }
}
