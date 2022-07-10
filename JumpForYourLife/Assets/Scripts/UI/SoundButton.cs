using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private GameObject soundOnIcon;
    [SerializeField] private GameObject soundOffIcon;

    private void Start()
    {
        if (PlayerPrefs.GetInt("OnSound") == 1)
        {
            soundOnIcon.SetActive(true);
            soundOffIcon.SetActive(false);
        }
        else
        {
            soundOnIcon.SetActive(false);
            soundOffIcon.SetActive(true);
        }
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt("OnSound") == 1)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        PlayerPrefs.SetInt("OnSound", 1);
        AudioManager.instance.PlaySound("Tap");

        soundOnIcon.SetActive(true);
        soundOffIcon.SetActive(false);
    }

    private void TurnOff()
    {
        PlayerPrefs.SetInt("OnSound", 0);
        
        soundOnIcon.SetActive(false);
        soundOffIcon.SetActive(true);
    }
}