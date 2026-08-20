using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public AudioClip coinClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        if (bgmClip != null && bgmSource != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = 0.3f;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayCoinSound()
    {
        if (coinClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(coinClip, 0.8f);
        }
    }

    public void PlayWinSound()
    {
        if (winClip != null && sfxSource != null)
        {
            bgmSource.Stop();
            sfxSource.PlayOneShot(winClip, 1f);
        }
    }

    public void PlayLoseSound()
    {
        if (loseClip != null && sfxSource != null)
        {
            bgmSource.Stop();
            sfxSource.PlayOneShot(loseClip, 1f);
        }
    }
}