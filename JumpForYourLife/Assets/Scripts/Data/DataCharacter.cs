using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CharacterElement
{
    public string name;
    public string description;
    public Sprite avatar;
    public Sprite sprite;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "NewDataCharacter", menuName = "ScriptableObject/DataCharacter")]
public class DataCharacter : ScriptableObject
{
    public List<CharacterElement> data;
}