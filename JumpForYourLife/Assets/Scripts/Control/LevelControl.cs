using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject prepareScreen;
    [SerializeField] private GameObject playScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private SceneTransition sceneTransition;

    private void Awake()
    {
        Time.timeScale = 1f;
        LevelManager.score = 0;
        LevelManager.isPlaying = false;
        AudioManager.instance.PlayMusic("Gameplay");
    }

    private void Start()
    {
        prepareScreen.SetActive(true);
        playScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    public void OnPlay()
    {
        LevelManager.isPlaying = true;
    }

    public void OnGameOver()
    {
        playScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void OnPause()
    {
        AudioManager.instance.PlaySound("Tap");

        Time.timeScale = 0;
        LevelManager.isPlaying = false;
        pauseScreen.SetActive(true);
    }

    public void OnReplay()
    {
        AudioManager.instance.StopMusic("GameOver");
        AudioManager.instance.StopMusic("NewRecord");
        AudioManager.instance.PlaySound("Tap");
        sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnHome()
    {
        AudioManager.instance.StopMusic("Gameplay");
        AudioManager.instance.PlaySound("Tap");
        sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}