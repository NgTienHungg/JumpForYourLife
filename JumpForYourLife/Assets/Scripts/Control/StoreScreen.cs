using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] private GameObject characterTab;
    [SerializeField] private GameObject themeTab;

    private void OnEnable()
    {
        characterTab.SetActive(true);
        themeTab.SetActive(false);

        characterTab.transform.localScale = Vector3.zero;
        themeTab.transform.localScale = Vector3.one;

        LeanTween.scale(characterTab, Vector3.one, 0.6f).setEaseOutBack();
    }

    public void OnClose()
    {
        AudioManager.instance.PlaySound("Tap");

        if (characterTab.activeInHierarchy)
            LeanTween.scale(characterTab, Vector3.zero, 0.5f).setEaseInBack().setOnComplete(OnComplete);
        else if (themeTab.activeInHierarchy)
            themeTab.transform.LeanScale(Vector3.zero, 0.5f).setEaseInBack().setOnComplete(OnComplete);
    }

    private void OnComplete()
    {
        gameObject.SetActive(false);
    }

    public void OnSelectCharacter()
    {
        AudioManager.instance.PlaySound("Click");
        characterTab.SetActive(true);
        themeTab.SetActive(false);
    }

    public void OnSelectTheme()
    {
        AudioManager.instance.PlaySound("Click");
        characterTab.SetActive(false);
        themeTab.SetActive(true);
    }
}