using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour

{

    [Header("--------------- AudioSource ----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource ghostSource;
    [SerializeField] AudioSource blackHoleSource;

    [Header("--------------- AudioClip ----------------")]
    public AudioClip mainMenuBackground;
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip background3;
    public AudioClip background4;
    public AudioClip background5;

    public AudioClip walk;
    public AudioClip jump;
    public AudioClip doublejump;
    public AudioClip fall;
    public AudioClip buttonclick;
    public AudioClip menuopen;
    public AudioClip collectitem;
    public AudioClip grappling;
    public AudioClip freeze;
    public AudioClip crackingIce;
    public AudioClip mealtingclick;
    public AudioClip shieldactived;
    public AudioClip shieldbroken;
    public AudioClip rocketExplosion;
    public AudioClip laucher;
    public AudioClip collectcoin;
    public AudioClip changemapgate;
    public AudioClip ghost;
    public AudioClip blackHole;


    private bool isWalking;
    private bool isGhostSoundPlaying;
    private bool isBlackHoleSoundPlaying;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopMusic();
        PlayMusic(scene.name);
    }

    private void PlayMusic(string sceneName)
    {
        AudioClip musicClip = null;

        switch (sceneName)
        {
            case "MainMenu":
                musicClip = mainMenuBackground;
                break;
            case "RuinsMap":
                musicClip = background1;
                break;
            case "Redmap":
                musicClip = background2;
                break;
            case "JungleMap1":
                musicClip = background3;
                break;
            case "IceMap":
                musicClip = background4;
                break;
            case "CastleMap1_Final":
                musicClip = background5;
                break;
        }

        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }

    private void StopMusic()
    {
        musicSource.Stop();
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
            SFXSource.clip = walk;
            SFXSource.Stop();
            isWalking = false;
        }
    }

    public void PlayGhostSound()
    {
        if (!isGhostSoundPlaying)
        {
            ghostSource.clip = ghost;
            ghostSource.loop = true;
            ghostSource.Play();
            isGhostSoundPlaying = true;
        }
    }

    public void StopGhostSound()
    {
        ghostSource.Stop();
        isGhostSoundPlaying = false;
    }

    public void PlayBlackHoleSound()
    {
        if (!isBlackHoleSoundPlaying)
        {
            blackHoleSource.clip = blackHole;
            blackHoleSource.loop = true;
            blackHoleSource.Play();
            isBlackHoleSoundPlaying = true;
        }
    }

    public void StopBlackHoleSound()
    {
        blackHoleSource.Stop();
        isBlackHoleSoundPlaying = false;
    }

}