using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text resultText;
    [SerializeField] private Text countdownText;

    public static GameManager instance;

    private List<BeyBlade> aliveBeyBlades = new List<BeyBlade>();

    private bool gameEnded = false;
    private bool isPaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BeyBlade[] blades = UObject.FindObjectsByType<BeyBlade>(FindObjectsSortMode.None);

        foreach (BeyBlade b in blades)
        {
            aliveBeyBlades.Add(b);
            b.OnDeath += HandleBeyBladeDeath;
        }

        resultText.text = "";
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        Time.timeScale = 0f;

        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void HandleBeyBladeDeath(BeyBlade blade)
    {
        aliveBeyBlades.Remove(blade);

        if (aliveBeyBlades.Count == 1)
        {
            resultText.text = $"Winner: {aliveBeyBlades[0].name}";
            gameEnded = true;
        }
        else if (aliveBeyBlades.Count == 0)
        {
            resultText.text = "Draw!";
            gameEnded = true;
        }
    }

    private void TogglePause()
    {
        if (gameEnded) return;

        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            resultText.text = "PAUSED";
        }
        else
        {
            Time.timeScale = 1f;
            resultText.text = "";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}