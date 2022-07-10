using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotTheme : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image themeAvatar;
    [SerializeField] private TextMeshProUGUI themeName;
    [SerializeField] private GameObject tickIcon;

    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private void Start()
    {
        ThemeElement theme = GameManager.instance.dataTheme.data[id];
        themeAvatar.sprite = theme.avatar;
        themeName.text = theme.name;
        tickIcon.SetActive(false);
    }

    private void Update()
    {
        if (id == PlayerPrefs.GetInt("IDTheme"))
        {
            button.interactable = false;
            tickIcon.SetActive(true);
        }
        else
        {
            button.interactable = true;
            tickIcon.SetActive(false);
        }
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySound("Tap");
        PlayerPrefs.SetInt("IDTheme", id);
    }
}