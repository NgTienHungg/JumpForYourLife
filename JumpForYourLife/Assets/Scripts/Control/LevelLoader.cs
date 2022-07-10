using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private GameObject startPlatform;

    private ThemeElement theme;
    private CharacterElement character;

    private void Awake()
    {
        // Theme
        if (PlayerPrefs.GetInt("IDTheme") == 0)
        {
            int randomIndex = Random.Range(1, GameManager.instance.dataTheme.data.Count);
            theme = GameManager.instance.dataTheme.data[randomIndex];
        }
        else
            theme = GameManager.instance.dataTheme.data[PlayerPrefs.GetInt("IDTheme")];

        background.GetComponent<SpriteRenderer>().sprite = theme.background;

        for (int i = 0; i < platforms.Length; ++i)
        {
            platforms[i].GetComponent<PlatformStatus>().spriteEntire = theme.entirePlatforms[i];
            platforms[i].GetComponent<PlatformStatus>().spriteCrack = theme.crackPlatforms[i];
        }

        // wall
        foreach (var wall in walls)
            wall.GetComponent<SpriteRenderer>().sprite = theme.wall;

        if (Screen.height / Screen.width == 2)
        {
            walls[0].transform.position = new Vector3(-2.6f, walls[0].transform.position.y, 0f);
            walls[1].transform.position = new Vector3(2.6f, walls[1].transform.position.y, 0f);
        }
        else
        {
            walls[0].transform.position = new Vector3(-2.9f, walls[0].transform.position.y, 0f);
            walls[1].transform.position = new Vector3(2.9f, walls[1].transform.position.y, 0f);
        }

        // Character
        if (PlayerPrefs.GetInt("IDCharacter") == 0)
        {
            int randomIndex = Random.Range(1, GameManager.instance.dataCharacter.data.Count);
            character = GameManager.instance.dataCharacter.data[randomIndex];
        }
        else
            character = GameManager.instance.dataCharacter.data[PlayerPrefs.GetInt("IDCharacter")];

        Instantiate(character.prefab, new Vector3(0, 4.5f, 0), Quaternion.identity);
    }

    private void Start()
    {
        startPlatform.GetComponent<SpriteRenderer>().sprite = theme.entirePlatforms[2];
    }
}