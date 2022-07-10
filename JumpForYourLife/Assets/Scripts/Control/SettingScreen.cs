using UnityEngine;

public class SettingScreen : MonoBehaviour
{
    [SerializeField] private GameObject tab;
    [SerializeField] private GameObject musicButton, soundButton, creditButton;

    private void OnEnable()
    {
        tab.transform.localScale = Vector3.zero;
        musicButton.transform.localScale = Vector3.zero;
        soundButton.transform.localScale = Vector3.zero;
        creditButton.transform.localScale = Vector3.zero;

        LeanTween.scale(tab, Vector3.one, 0.6f).setEaseOutBack();
        LeanTween.scale(musicButton, Vector3.one, 0.4f).setEaseOutCubic().setDelay(0.2f);
        LeanTween.scale(soundButton, Vector3.one, 0.4f).setEaseOutCubic().setDelay(0.3f);
        LeanTween.scale(creditButton, Vector3.one, 0.4f).setEaseOutCubic().setDelay(0.4f);
    }

    public void OnCredit()
    {
        AudioManager.instance.PlaySound("Tap");
        Application.OpenURL("https://github.com/NgTienHungg");
    }

    public void OnClose()
    {
        AudioManager.instance.PlaySound("Tap");
        LeanTween.scale(tab, Vector3.zero, 0.5f).setEaseInBack().setOnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }
}