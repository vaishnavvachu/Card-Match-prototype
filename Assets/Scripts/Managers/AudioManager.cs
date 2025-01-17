using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    [SerializeField] private  AudioClip cardClickSound;
    [SerializeField] private  AudioClip cardMatchSound;
    [SerializeField] private  AudioClip cardMismatchSound;
    [SerializeField] private  AudioClip gameOverSound;

    private AudioSource _audioSource;

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

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCardClickSound()
    {
        PlaySound(cardClickSound);
    }

    public void PlayCardMatchSound()
    {
        PlaySound(cardMatchSound);
    }

    public void PlayCardMismatchSound()
    {
        PlaySound(cardMismatchSound);
    }

    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
