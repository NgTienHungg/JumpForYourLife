using UnityEngine;
using UnityEngine.UI;

public class SlotCharacter : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image avatar;
    [SerializeField] private GameObject tickIcon;

    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    private void Start()
    {
        CharacterElement character = GameManager.instance.dataCharacter.data[id];
        avatar.sprite = character.avatar;
        tickIcon.SetActive(false);
    }

    private void Update()
    {
        if (id == PlayerPrefs.GetInt("IDCharacter"))
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
        AudioManager.instance.PlaySound("Buy");
        PlayerPrefs.SetInt("IDCharacter", id);
    }
}