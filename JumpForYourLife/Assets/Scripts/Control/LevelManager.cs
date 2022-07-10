using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int score = 0;
    public static bool isPlaying = false;
    public static int comboPerfect = 0;

    private void Start()
    {
        score = 0;
        isPlaying = false;
        comboPerfect = 0;
    }
}