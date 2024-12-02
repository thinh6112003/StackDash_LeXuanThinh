using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("--- Audio Source ----------")]
    public AudioSource musicSource;
    public AudioSource SoundSource;

    [Header("--- Audio Clip -----")]
    public AudioClip backgroundMusic;
    public AudioClip levelComplete;
    public AudioClip buttonClick;
    public AudioClip layBrick;
    public AudioClip pickBrick;
    public AudioClip bridgeClear;
    public AudioClip swipe;

    void Start()
    {
        //musicSource.clip = backgroundMusic;
        //musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SoundSource.pitch = 1;
        SoundSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float pitch)
    {
        SoundSource.pitch = pitch;
        SoundSource.PlayOneShot(clip);
    }
    public void PlayButtonClickSound()
    {
        SoundSource.PlayOneShot(buttonClick);
    }
    public void activeMusic()
    {
        musicSource.enabled = !musicSource.enabled;
    }
    public void activeSound()
    {
        SoundSource.enabled = !SoundSource.enabled;
    }
    public void SetSound(SoundType soundType) {
        if (!DataManager.Instance.dynamicData.GetSoundStatus()) return;
        switch (soundType) { 
            case SoundType.ButtonClick:
                PlayButtonClickSound();
                break;
            case SoundType.LayBrick:
                PlaySFX(layBrick);
                break;
            case SoundType.PickBrick:
                PlaySFX(pickBrick);
                break;
            case SoundType.BridgeClear:
                PlaySFX(bridgeClear);
                break;
            case SoundType.Swipe:
                PlaySFX(swipe);
                break;
            case SoundType.LevelComplete:
                PlaySFX(levelComplete);
                break;
        }
    }
    public void SetSound(SoundType soundType, float pitch)
    {
        if (!DataManager.Instance.dynamicData.GetSoundStatus()) return;
        switch (soundType)
        {
            case SoundType.ButtonClick:
                PlayButtonClickSound();
                break;
            case SoundType.LayBrick:
                PlaySFX(layBrick, pitch);
                break;
            case SoundType.PickBrick:
                PlaySFX(pickBrick, pitch);
                break;
            case SoundType.BridgeClear:
                PlaySFX(bridgeClear, pitch);
                break;
            case SoundType.Swipe:
                PlaySFX(swipe, pitch);
                break;
            case SoundType.LevelComplete:
                PlaySFX(levelComplete, pitch);
                break;
        }
    }
    public enum SoundType { ButtonClick, LayBrick, PickBrick, BridgeClear, Swipe , LevelComplete};
}