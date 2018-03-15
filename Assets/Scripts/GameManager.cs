﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private float gameTime = 0;
    public float GameTime { get { return gameTime; } }

    public GameObject[] tanks;
    public GameObject aimIndicator;
    public GameObject playerSpawn;
    public GameObject enemySpawn;

    public GameObject highScoresPanel;
    public HighScores highScores;
    public Text highScoresText;
    public Text messageText;
    public Text timerText;
    public Button newGameButton;
    public Button highScoresButton;
    public Button backButton;

    public enum GameState
    {
        Start,
        Game,
        GameOver
    }
    private GameState gameState;
    public GameState State { get { return gameState; } }

    private void Awake()
    {
        gameState = GameState.Start;
    }
	
	void Update ()
    {
		switch(gameState)
        {
            case GameState.Start:
                foreach (GameObject tank in tanks)
                    tank.SetActive(false);

                Cursor.visible = true;
                aimIndicator.gameObject.SetActive(false);
                timerText.gameObject.SetActive(false);
                highScoresPanel.gameObject.SetActive(false);
                highScoresButton.gameObject.SetActive(false);
                newGameButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                messageText.text = "Get Ready";

                if (Input.GetKeyUp(KeyCode.Mouse0) == true)
                {
                    foreach (GameObject tank in tanks)
                    {
                        tank.SetActive(true);
                    }

                    Cursor.visible = false;
                    aimIndicator.gameObject.SetActive(true);
                    timerText.gameObject.SetActive(true);
                    messageText.text = "";

                    gameState = GameState.Game;
                }
                break;

            case GameState.Game:
                bool isGameOver = false;

                gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(gameTime);
                timerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                if (OneTankLeft() == true)
                    isGameOver = true;
                else if (IsPlayerDead() == true)
                    isGameOver = true;

                if (isGameOver == true)
                {
                    Cursor.visible = true;
                    aimIndicator.gameObject.SetActive(false);
                    timerText.gameObject.SetActive(false);
                    highScoresButton.gameObject.SetActive(true);
                    newGameButton.gameObject.SetActive(true);

                    if (IsPlayerDead() == true)
                    {
                        messageText.text = "TRY AGAIN";
                    }
                    else
                    {
                        messageText.text = "WINNER!";

                        highScores.AddScore(Mathf.RoundToInt(gameTime));
                        highScores.SaveScoresToFile();
                    }

                    gameState = GameState.GameOver;
                }

                //DEBUG TOOL
                if (Input.GetKeyUp(KeyCode.P) == true)
                {
                    highScores.AddScore(Mathf.RoundToInt(gameTime));
                    highScores.SaveScoresToFile();
                }

                break;

            case GameState.GameOver:
                break;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
	}

    private void ResetTanks() //Resets every tank position and sets them as active, resets health.
    {
        foreach (GameObject tank in tanks)
        {
            if (tank.tag == "Player")
            {
                tank.transform.position = playerSpawn.transform.position;
                tank.transform.rotation = playerSpawn.transform.rotation;
            }
            else
            {
                tank.transform.position = enemySpawn.transform.position;
                tank.transform.rotation = enemySpawn.transform.rotation;
            }
            tank.SetActive(true);

            TankHealth health = tank.GetComponent<TankHealth>();
            health.Initialise();
        }
    }

    public void OnNewGame()
    {
        ResetTanks();
        gameTime = 0;
        messageText.text = "";
        aimIndicator.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        highScoresButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        Cursor.visible = false;
        gameState = GameState.Game;
    }

    public void OnHighScores()
    {
        messageText.gameObject.SetActive(false);
        highScoresButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        highScoresPanel.SetActive(true);

        string text = "";
        for (int i = 0; i < highScores.scores.Length; i++)
        {
            int seconds = highScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        highScoresText.text = text;
    }

    public void OnBackButton()
    {
        highScoresPanel.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(true);
        highScoresButton.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
    }

    private bool OneTankLeft() //If only one tank remains, returns true.
    {
        int numberOfTanks = 0;

        foreach (GameObject tank in tanks)
        {
            if (tank.activeSelf == true)
            {
                numberOfTanks++;
            }
        }

        return (numberOfTanks <= 1);
    }

    private bool IsPlayerDead() //Checks for inactive tanks, returns true if it's the player tank.
    {
        foreach (GameObject tank in tanks)
        {
            if (tank.activeSelf == false)
            {
                if (tank.tag == "Player")
                {
                    return true;
                }
            }
        }

        return false;
    }
}
