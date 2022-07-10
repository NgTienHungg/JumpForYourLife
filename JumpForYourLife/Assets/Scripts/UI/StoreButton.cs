using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    [SerializeField] private Image icon;

    private void Update()
    {
        icon.sprite = GameManager.instance.dataCharacter.data[PlayerPrefs.GetInt("IDCharacter")].avatar;
    }
}