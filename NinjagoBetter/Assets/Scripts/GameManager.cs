using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<GameObject> blades = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        blades.AddRange(GameObject.FindGameObjectsWithTag("BeyBlade"));
    }

    public void BeyBladeDestroyed(GameObject obj)
    {
        blades.Remove(obj);

        if (blades.Count == 1)
            Debug.Log($"Winner: {blades[0].name}");
    }
}
