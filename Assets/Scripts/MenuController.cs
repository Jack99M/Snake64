using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip menuMusic;
    public AudioClip buttonClickSFX;
    public AudioClip buttonHoverSFX;

    void Start()
    {
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void OnPlayButton()
    {
        musicSource.PlayOneShot(buttonClickSFX);
        Invoke(nameof(LoadGame), 0.2f);
    }

    public void OnButtonHover()
    {
        musicSource.PlayOneShot(buttonHoverSFX);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
