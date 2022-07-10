using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject storeButton;
    [SerializeField] private GameObject rankingButton;
    [SerializeField] private GameObject settingButton;

    [Header("Screens")]
    [SerializeField] private GameObject storeScreen;
    [SerializeField] private GameObject rankingScreen;
    [SerializeField] private GameObject settingScreen;

    [Header("Scene Transition")]
    [SerializeField] private SceneTransition sceneTransition;

    private void Awake()
    {
        AudioManager.instance.PlayMusic("Background");
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        playButton.transform.localScale = Vector3.zero;
        storeButton.transform.localScale = Vector3.zero;
        rankingButton.transform.localScale = Vector3.zero;
        settingButton.transform.localScale = Vector3.zero;

        LeanTween.scale(playButton, Vector3.one, 0.8f).setEaseOutCubic().setDelay(0.2f).setOnComplete(OnPlayButtonComplete);
        LeanTween.scale(storeButton, Vector3.one, 0.6f).setEaseOutCubic().setDelay(0.4f);
        LeanTween.scale(rankingButton, Vector3.one, 0.6f).setEaseOutCubic().setDelay(0.4f);
        LeanTween.scale(settingButton, Vector3.one, 0.6f).setEaseOutCubic().setDelay(0.4f);
    }

    public void OnPlay()
    {
        AudioManager.instance.PlaySound("Pop");
        AudioManager.instance.StopMusic("Background");
        sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnStore()
    {
        AudioManager.instance.PlaySound("Pop");
        storeScreen.SetActive(true);
    }

    public void OnRanking()
    {
        AudioManager.instance.PlaySound("Pop");
        rankingScreen.SetActive(true);
    }

    public void OnSetting()
    {
        AudioManager.instance.PlaySound("Pop");
        settingScreen.SetActive(true);
    }

    private void OnPlayButtonComplete()
    {
        LeanTween.scale(playButton, Vector3.one * 1.1f, 0.5f).setEaseInOutQuad().setLoopPingPong();
    }
}