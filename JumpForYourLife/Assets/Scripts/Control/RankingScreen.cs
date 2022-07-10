using UnityEngine;

public class RankingScreen : MonoBehaviour
{
    [SerializeField] private GameObject tab;

    private void OnEnable()
    {
        tab.transform.localScale = Vector3.zero;
        LeanTween.scale(tab, Vector3.one, 0.6f).setEaseOutBack();
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