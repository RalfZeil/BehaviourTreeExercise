using System.Collections.Generic;
using UnityEngine;

public class HidingSpots : MonoBehaviour
{
    public static HidingSpots instance;
    public List<Transform> hidingObjects = new();

    void Awake()
    {
        instance = this;

        foreach (Transform child in transform)
        {
            hidingObjects.Add(child);
        }
    }
}
