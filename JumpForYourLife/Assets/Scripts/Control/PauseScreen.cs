using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject tab;
    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject homeButton;
    [SerializeField] private GameObject countDown;

    private void OnEnable()
    {
        tab.SetActive(true);
        countDown.SetActive(false);
        gameObject.GetComponent<Image>().enabled = true; // panel

        tab.transform.localScale = Vector3.zero;
        musicButton.transform.localScale = Vector3.zero;
        soundButton.transform.localScale = Vector3.zero;
        replayButton.transform.localScale = Vector3.zero;
        homeButton.transform.localScale = Vector3.zero;

        LeanTween.scale(tab, Vector3.one, 0.6f).setEaseOutBack().setIgnoreTimeScale(true); // chay khi TimeScale = 0
        LeanTween.scale(musicButton, Vector3.one, 0.3f).setEaseOutCubic().setDelay(0.1f).setIgnoreTimeScale(true);
        LeanTween.scale(soundButton, Vector3.one, 0.3f).setEaseOutCubic().setDelay(0.2f).setIgnoreTimeScale(true);
        LeanTween.scale(replayButton, Vector3.one, 0.3f).setEaseOutCubic().setDelay(0.3f).setIgnoreTimeScale(true);
        LeanTween.scale(homeButton, Vector3.one, 0.3f).setEaseOutCubic().setDelay(0.4f).setIgnoreTimeScale(true);
    }

    public void OnResume()
    {
        AudioManager.instance.PlaySound("Tap");
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        gameObject.GetComponent<Image>().enabled = false;
        tab.SetActive(false);
        countDown.SetActive(true);

        for (int i = 1; i <= 3; ++i)
        {
            AudioManager.instance.PlaySound("TickTac");
            yield return new WaitForSecondsRealtime(1f);
        }

        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}