using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
    public AudioClip collisionSound;
    public AudioClip modeChangeSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayJumpSound()
    {
        if (sfxSource != null && jumpSound != null)
        {
            sfxSource.PlayOneShot(jumpSound);
        }
    }

    public void PlayCollisionSound()
    {
        if (sfxSource != null && collisionSound != null)
        {
            sfxSource.PlayOneShot(collisionSound);
        }
    }

    public void PlayModeChangeSound()
    {
        if (sfxSource != null && modeChangeSound != null)
        {
            sfxSource.PlayOneShot(modeChangeSound);
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}