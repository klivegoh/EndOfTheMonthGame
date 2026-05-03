using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip buttonHover;
    [SerializeField] public AudioClip buttonClick;
    [SerializeField] public AudioClip paperHover;
    [SerializeField] public AudioClip paperClick;

    private void Awake()
    {
        I = this;
    }

    public void PlayOneShot(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        // You can assign a click sound in the inspector and play it here
        audioSource.PlayOneShot(buttonClick);
    }

    public void PlayPaperClick()
    {
        audioSource.PlayOneShot(paperClick);
    }
}
