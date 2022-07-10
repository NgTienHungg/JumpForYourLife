using UnityEngine;

public class PlatformScore : MonoBehaviour
{
    [SerializeField] private int score = 1;
    [SerializeField] private float perfectRange = 0.05f;
    private bool scoreAdded = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (scoreAdded)
            return;

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerControl>().OnPlatform())
            {
                // nhay chinh giua
                if (Mathf.Abs(collision.gameObject.GetComponent<BoxCollider2D>().bounds.center.x - transform.position.x) <= perfectRange)
                {
                    LevelManager.comboPerfect = Mathf.Clamp(LevelManager.comboPerfect + 1, 0, 9);
                    LevelManager.score += score + LevelManager.comboPerfect;

                    EffectManager.instance.Play("Perfect", transform.position);
                    AudioManager.instance.PlaySound("Effect");
                    if (LevelManager.comboPerfect >= 2)
                    {
                        EffectManager.instance.Play("X", transform.position + new Vector3(-0.3f, -0.6f, 0f));
                        EffectManager.instance.Play(LevelManager.comboPerfect.ToString(), transform.position + new Vector3(0.3f, -0.6f, 0f));
                    }

                    scoreAdded = true;
                    return;
                }

                // nhay 2 bac vao ShortPlatform
                if (collision.gameObject.GetComponent<PlayerControl>().JumpingDistance() == 6f && gameObject.name == "ShortPlatform(Clone)")
                {
                    EffectManager.instance.Play("Excellent", transform.position);
                    AudioManager.instance.PlaySound("Effect");
                    score *= 2;
                }

                LevelManager.comboPerfect = 0;
                LevelManager.score += score;
                scoreAdded = true;
            }
        }
    }
}