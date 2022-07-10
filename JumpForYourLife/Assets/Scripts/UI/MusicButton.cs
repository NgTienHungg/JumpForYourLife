using UnityEngine;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private GameObject musicOnIcon;
    [SerializeField] private GameObject musicOffIcon;

    private void Start()
    {
        if (PlayerPrefs.GetInt("OnMusic") == 1)
        {
            musicOnIcon.SetActive(true);
            musicOffIcon.SetActive(false);
        }
        else
        {
            musicOnIcon.SetActive(false);
            musicOffIcon.SetActive(true);
        }
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt("OnMusic") == 1)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        PlayerPrefs.SetInt("OnMusic", 1);
        AudioManager.instance.PlaySound("Tap");
        AudioManager.instance.ContinuePlayMusic();

        musicOnIcon.SetActive(true);
        musicOffIcon.SetActive(false);
    }

    private void TurnOff()
    {
        PlayerPrefs.SetInt("OnMusic", 0);
        AudioManager.instance.PauseAllMusic();

        musicOnIcon.SetActive(false);
        musicOffIcon.SetActive(true);
    }
}