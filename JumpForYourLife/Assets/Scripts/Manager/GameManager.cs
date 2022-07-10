using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#region PlayerPrefs
/*
 * IDCharacter
 * IDTheme
 * HighScore
 * OnMusic
 * OnSound
*/
#endregion

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public DataCharacter dataCharacter;
    public DataTheme dataTheme;

    public static Vector3 MousePositionWorldPoint()
    {
        // lay vi tri Mouse tu Screen, chuyen sang World Point
        Vector3 screen = Input.mousePosition;
        return Camera.main.WorldToScreenPoint(screen);
    }

    public static bool IsMouseOverUI()
    {
        // kiem tra xem co dang click chuot vao UI khong
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}