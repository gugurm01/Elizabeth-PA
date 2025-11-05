using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    AudioSource source;

    [Header("Sound UI")]
    [SerializeField] AudioClip clip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundUI()
    {
        source.PlayOneShot(clip);
    }
}
