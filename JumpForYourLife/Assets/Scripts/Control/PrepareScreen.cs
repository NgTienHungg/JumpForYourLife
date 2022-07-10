using UnityEngine;
using System.Collections;

public class PrepareScreen : MonoBehaviour
{
    [SerializeField] LevelControl levelControl;
    [SerializeField] private Animator[] animators;
    [SerializeField] private float waitToDisable = 0.25f; // thoi gian animation ket thuc

    private void Update()
    {
        if (!LevelManager.isPlaying && Input.GetMouseButton(0) && !GameManager.IsPointerOverUIObject())
            StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        AudioManager.instance.PlayMusic("Ready");
        levelControl.OnPlay();

        foreach (var animator in animators)
            animator.SetTrigger("tap");

        yield return new WaitForSeconds(waitToDisable);

        gameObject.SetActive(false);
    }
}