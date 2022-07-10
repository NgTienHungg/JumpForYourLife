using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotCharacterInfo : MonoBehaviour
{
    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI description;

    private void Update()
    {
        CharacterElement character = GameManager.instance.dataCharacter.data[PlayerPrefs.GetInt("IDCharacter")];
        sprite.sprite = character.sprite;
        characterName.text = character.name;
        description.text = character.description;
    }
}