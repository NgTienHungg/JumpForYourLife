using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}