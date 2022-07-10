using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject normalTab;
    [SerializeField] private GameObject newRecordTab;

    private void OnEnable()
    {
        LevelManager.isPlaying = false;
        AudioManager.instance.StopMusic("Gameplay");

        if (PlayerPrefs.GetInt("HighScore") < LevelManager.score)
        {
            AudioManager.instance.PlayMusic("NewRecord");
            PlayerPrefs.SetInt("HighScore", LevelManager.score);

            newRecordTab.SetActive(true);
            normalTab.SetActive(false);

            LeanTween.moveLocal(newRecordTab, Vector3.zero, 0.2f).setEaseOutExpo();
            LeanTween.scale(transform.GetChild(0).GetChild(2).gameObject, Vector3.one * 1.1f, 0.5f).setEaseInOutQuad().setLoopPingPong(); // ReplayButton
        }
        else
        {
            AudioManager.instance.PlayMusic("GameOver");

            normalTab.SetActive(true);
            newRecordTab.SetActive(false);

            LeanTween.moveLocal(normalTab, Vector3.zero, 0.2f).setEaseOutExpo();
            LeanTween.scale(transform.GetChild(1).GetChild(2).gameObject, Vector3.one * 1.1f, 0.5f).setEaseInOutQuad().setLoopPingPong();
        }
    }
}