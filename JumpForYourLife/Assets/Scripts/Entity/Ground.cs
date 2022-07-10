using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private LevelControl levelControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            levelControl.OnGameOver();
        }
    }
}