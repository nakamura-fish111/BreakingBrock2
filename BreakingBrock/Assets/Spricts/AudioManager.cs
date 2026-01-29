using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("SE Lists")]
    public AudioClip[] hitBlockSEs;
    public AudioClip[] hitPaddleSEs;
    public AudioClip[] hitWallSEs;
    public AudioClip[] gameOverSEs;

    private AudioSource source;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        source = GetComponent<AudioSource>();
    }

    public void PlayRandomSE(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;


        source.pitch = Random.Range(0.95f, 1.05f);
        source.volume = Random.Range(0.8f, 1.0f);

        int index = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[index]);
    }

}
