using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }
}
