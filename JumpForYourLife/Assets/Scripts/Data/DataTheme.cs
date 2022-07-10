using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class ThemeElement
{
    public string name;
    public Sprite avatar;
    public Sprite background;
    public Sprite wall;
    public Sprite[] entirePlatforms;
    public Sprite[] crackPlatforms;
}

[CreateAssetMenu(fileName = "NewDataTheme", menuName = "ScriptableObject/DataTheme")]
public class DataTheme : ScriptableObject
{
    public List<ThemeElement> data;
}