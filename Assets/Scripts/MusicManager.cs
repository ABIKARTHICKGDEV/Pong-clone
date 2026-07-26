using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
 
    public TMP_Text musicButtonText;
    public static MusicManager instance;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
    }
    void MakeSingleton()
    {
        if (instance != null) {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    public void ToogleMusic()
    {
        if (audioSource.isPlaying == true)
        {
            audioSource.Stop();
            musicButtonText.text = "Music On";
        }
        else 
        { 
            audioSource.Play();
            musicButtonText.text = "Music Off";
        }
    }
}
