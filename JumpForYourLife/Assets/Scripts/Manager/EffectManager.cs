using System;
using UnityEngine;

[Serializable]
public class Effect
{
    public string name;
    public GameObject effect;
}

public class EffectManager : MonoBehaviour
{
    #region SINGLETON
    public static EffectManager instance;

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

    public Effect[] effects;

    public void Play(string name, Vector3 position)
    {
        Effect effect = Array.Find(effects, effect => effect.name == name);
        if (effect == null)
        {
            Debug.LogWarning("Can't find effect with name: " + name);
            return;
        }

        Instantiate(effect.effect, position, Quaternion.identity);
    }
}