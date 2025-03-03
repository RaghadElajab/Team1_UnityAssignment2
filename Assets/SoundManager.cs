using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClip cheerSound;
    public AudioClip booSound;
    public AudioClip powerupSound;
    public AudioClip shieldSound;

    public float sfxVolume = 0.2f;  // General SFX volume
    public float shieldVolume = 0.2f;  // Shield sound volume

    private AudioSource sfxSource;
    private AudioSource shieldSource;
    private Coroutine booCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        shieldSource = gameObject.AddComponent<AudioSource>();

        shieldSource.loop = true;
    }

    public void PlayCheerSound()
    {
        PlaySFX(cheerSound);
    }

    public void PlayBooSound()
    {
        if (booCoroutine != null) StopCoroutine(booCoroutine);
        booCoroutine = StartCoroutine(PlayBooForDuration(2f));
    }

    private IEnumerator PlayBooForDuration(float duration)
    {
        sfxSource.volume = sfxVolume;
        sfxSource.clip = booSound;
        sfxSource.Play();
        yield return new WaitForSeconds(duration);
        sfxSource.Stop();
    }

    public void PlayPowerupSound()
    {
        PlaySFX(powerupSound);
    }

    public void PlayShieldSound()
    {
        if (!shieldSource.isPlaying)
        {
            shieldSource.clip = shieldSound;
            shieldSource.volume = shieldVolume;
            shieldSource.Play();
        }
    }

    public void StopShieldSound()
    {
        if (shieldSource.isPlaying)
        {
            shieldSource.Stop();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.volume = sfxVolume;
        sfxSource.PlayOneShot(clip);
    }
}
