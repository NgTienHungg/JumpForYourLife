using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private GameObject startPlatform;
    private PlayerControl playerControl;

    [Header("Spawner")]
    [SerializeField] private int numberOfPlatforms = 5;
    [SerializeField] private float verticalDistance = 3f; // khoang cach giua cac Platform
    [SerializeField] private float limitPositionY = 5f; // vi tri destroy Platform
    [SerializeField] private float limitPositionX = 1.5f; // vi tri gioi han random x
    private List<GameObject> platforms;

    private void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        platforms = new List<GameObject>();
        platforms.Add(startPlatform);
        UpdatePlatforms();
    }

    private void Update()
    {
        if (playerControl.OnPlatform() && playerControl.JustJumped())
        {
            float jumpingDistance = playerControl.JumpingDistance();

            foreach (var platform in platforms)
                platform.GetComponent<PlatformMovement>().MoveUp(jumpingDistance);

            foreach (var wall in walls)
                wall.GetComponent<Wall>().MoveUp(jumpingDistance);
        }

        UpdatePlatforms();
    }

    private void UpdatePlatforms()
    {
        while (platforms[0].transform.position.y > limitPositionY)
        {
            Destroy(platforms[0]);
            platforms.Remove(platforms[0]);
        }

        while (platforms.Count < numberOfPlatforms)
        {
            // lay thong so cua Platform cuoi cung
            GameObject lastPlatform = platforms[platforms.Count - 1];
            float positionX = lastPlatform.transform.position.x;
            float pivotY = lastPlatform.GetComponent<PlatformMovement>().PivotY;

            // thay doi thong so moi
            if (positionX >= 0)
                positionX = Mathf.Clamp(-positionX + Random.Range(-0.2f, 0.2f), -limitPositionX, 0f); // random cach 1 doan < 0.2f (doi xung qua 0)
            else
                positionX = Mathf.Clamp(-positionX + Random.Range(-0.2f, 0.2f), 0f, limitPositionX);
            pivotY -= verticalDistance;
            Vector3 spawnPosition = new Vector3(positionX, pivotY, 0f);

            // sinh Platform
            GameObject newPlatform = Instantiate(RandomPrefab(lastPlatform), spawnPosition, Quaternion.identity, transform);
            RandomSpeed(lastPlatform, newPlatform);
            RandomAttribute(lastPlatform, newPlatform);
            platforms.Add(newPlatform);
        }
    }

    private GameObject RandomPrefab(GameObject lastPlatform)
    {
        PlatformStatus lastStatus = lastPlatform.GetComponent<PlatformStatus>();
        int maxScore = LevelManager.score + platforms.Count;

        if (maxScore <= 5)
            return platformPrefabs[2];
        else if (maxScore <= 12 || lastStatus.Type == EPlatformPrefabType.Short)
        {
            // Khong cho 2 ShortPlatform ke nhau
            return platformPrefabs[Random.Range(1, 3)];
        }
        else
            return platformPrefabs[Random.Range(0, 3)];
    }

    private void RandomSpeed(GameObject lastPlatform, GameObject newPlatform)
    {
        PlatformMovement lastMovement = lastPlatform.GetComponent<PlatformMovement>();
        PlatformMovement newMovement = newPlatform.GetComponent<PlatformMovement>();

        newMovement.IsMovingRight = !lastMovement.IsMovingRight; // 2 platform ke nhau di chuyen nguoc huong

        int maxScore = LevelManager.score + platforms.Count;
        newMovement.HorizontalSpeed += (maxScore / 5) * 0.1f; // cong thuc random speed
    }

    private void RandomAttribute(GameObject lastPlatform, GameObject newPlatform)
    {
        if (lastPlatform.name == "StartPlatform")
            return; // StartPlatform khong co PlatformAttriBute;

        PlatformAttribute lastAttribute = lastPlatform.GetComponent<PlatformAttribute>();
        PlatformAttribute newAttribute = newPlatform.GetComponent<PlatformAttribute>();

        int maxScore = LevelManager.score + platforms.Count - 1; // khong tinh StartPlatform
        int randomNum = Random.Range(0, 10);

        if (lastAttribute.Type == EPlatformAttributeType.Normal)
        {
            if (maxScore < 5)
                newAttribute.Type = EPlatformAttributeType.Normal;
            else if (maxScore >= 5 && maxScore < 8)
            {
                // < 5 diem => chi co cross
                if (randomNum < 8)
                {
                    newAttribute.Type = EPlatformAttributeType.Cross;
                    Debug.Log(maxScore + " cross");
                }
            }
            else if (maxScore >= 8 && maxScore < 12)
            {
                // < 10 diem => chi co cross va ziczac
                if (randomNum < 2)
                {
                    newAttribute.Type = EPlatformAttributeType.Cross;
                    Debug.Log(maxScore + " cross");
                }
                else if (randomNum < 8)
                {
                    newAttribute.Type = EPlatformAttributeType.Ziczac;
                    Debug.Log(maxScore + " ziczac");
                }
            }
            else
            {
                if (randomNum < 2)
                {
                    Debug.Log(maxScore + " cross");
                    newAttribute.Type = EPlatformAttributeType.Cross;
                }
                else if (randomNum < 5)
                {
                    Debug.Log(maxScore + " ziczac");
                    newAttribute.Type = EPlatformAttributeType.Ziczac;
                }
                else if (randomNum < 9)
                {
                    Debug.Log(maxScore + " invisible");
                    newAttribute.Type = EPlatformAttributeType.Invisible;
                }
            }
        }
        else if (lastAttribute.Type == EPlatformAttributeType.Cross || lastAttribute.Type == EPlatformAttributeType.Ziczac)
        {
            if (randomNum < 7 && maxScore >= 12)
            {
                Debug.Log(maxScore + " invisible");
                newAttribute.Type = EPlatformAttributeType.Invisible;
            }
        }
        else if (lastAttribute.Type == EPlatformAttributeType.Invisible)
        {
            if (randomNum < 4)
            {
                Debug.Log(maxScore + " cross");
                newAttribute.Type = EPlatformAttributeType.Cross;
            }
            else if (randomNum < 8)
            {
                Debug.Log(maxScore + " ziczac");
                newAttribute.Type = EPlatformAttributeType.Ziczac;
            }
        }
    }
}