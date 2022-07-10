using System.Collections.Generic;
using UnityEngine;

public class SlotCharacterManager : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject model;
    private List<GameObject> slots;

    private void Start()
    {
        slots = new List<GameObject>();
        int index = 0;
        while (index < GameManager.instance.dataCharacter.data.Count)
        {
            // sinh cac Slot trong Content
            slots.Add(Instantiate(model, content));

            // set Id cho Slot
            slots[index].GetComponent<SlotCharacter>().Id = index;

            index++;
        }
    }
}
