using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip music;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(audioSource != null && music != null)
        {
            audioSource.clip = music;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}