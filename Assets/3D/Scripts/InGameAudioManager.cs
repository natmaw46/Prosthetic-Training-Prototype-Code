using UnityEngine;

public class InGameAudioManager : MonoBehaviour
{
    // Background music clip
    public AudioClip backgroundMusic;
    
    // Sound effects clips
    public AudioClip slicingSound;
    public AudioClip popSound;
    public AudioClip bombSound;
    public AudioClip readySetSound;
    public AudioClip goSound;
    public AudioClip gameOverSound;
    public AudioClip gameFinishSound;
    public AudioClip fruitDropSound;
    public AudioClip comboSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PlaySlicingSound()
    {
        if (audioSource != null && slicingSound != null)
        {
            audioSource.PlayOneShot(slicingSound);
        }
    }

    public void PlayPopSound()
    {
        if (audioSource != null && popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }
    }

    public void PlayBombSound()
    {
        if (audioSource != null && bombSound != null)
        {
            audioSource.PlayOneShot(bombSound);
        }
    }

    public void PlayReadySetSound()
    {
        if (audioSource != null && readySetSound != null)
        {
            audioSource.PlayOneShot(readySetSound);
        }
    }

    public void PlayGoSound()
    {
        if (audioSource != null && goSound != null)
        {
            audioSource.PlayOneShot(goSound);
        }
    }

    public void PlayGameOverSound()
    {
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void PlayGameFinishedSound()
    {
        if (audioSource != null && gameFinishSound != null)
        {
            audioSource.PlayOneShot(gameFinishSound);
        }
    }

    public void PlayFruitDropSound()
    {
        if (audioSource != null && fruitDropSound != null)
        {
            audioSource.PlayOneShot(fruitDropSound);
        }
    }

    public void PlayComboSound()
    {
        if (audioSource != null && comboSound != null)
        {
            audioSource.PlayOneShot(comboSound);
        }
    }
}