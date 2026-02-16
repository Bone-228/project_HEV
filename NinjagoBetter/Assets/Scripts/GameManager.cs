using NUnit.Framework;
using UnityEngine;
using UnityEditor.UI;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text resultText;

    public static GameManager instance;

    private List<BeyBlade> aliveBeyBlades = new List<BeyBlade>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BeyBlade[] blades = FindObjectsOfType<BeyBlade>();

        foreach(BeyBlade b in blades)
        {
            aliveBeyBlades.Add(b);
            b.OnDeath += HandleBeyBladeDeath;
        }

        resultText.text = "";

    }

    private void HandleBeyBladeDeath(BeyBlade blade)
    {
        aliveBeyBlades.Remove(blade);

        if (aliveBeyBlades.Count == 1)
        {
            resultText.text = $"Winner: {aliveBeyBlades[0].name}";
        } else if (aliveBeyBlades.Count == 0)
        {
            resultText.text = "Draw!";
        }
    }
}
