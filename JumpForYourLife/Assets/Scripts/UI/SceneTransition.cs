using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator animator;
    private int nextSceneIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene(int sceneIndex)
    {
        animator.SetTrigger("FadeOut");
        nextSceneIndex = sceneIndex;
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}