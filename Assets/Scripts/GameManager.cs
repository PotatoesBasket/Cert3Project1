using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] tanks;

    private float gameTime = 0;
    public float GameTime { get { return gameTime; } }

    public HighScores highScores;
    public Text messageText;
    public Text timerText;

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

    void Start ()
    {
		foreach (GameObject tank in tanks)
        {
            tank.SetActive(false);
        }

        timerText.gameObject.SetActive(false);
        messageText.text = "Get Ready";
    }
	
	void Update ()
    {
		switch(gameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Mouse0) == true)
                {
                    timerText.gameObject.SetActive(true);
                    messageText.text = "";
                    gameState = GameState.Game;

                    foreach (GameObject tank in tanks)
                    {
                        tank.SetActive(true);
                    }
                }
                break;

            case GameState.Game:
                bool isGameOver = false;

                gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(gameTime);
                timerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                if (OneTankLeft() == true)
                {
                    isGameOver = true;
                }
                else if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }

                if (isGameOver == true)
                {
                    gameState = GameState.GameOver;
                    timerText.gameObject.SetActive(false);

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
                }

                ///*
                if (Input.GetKeyUp(KeyCode.P) == true)
                {
                    highScores.AddScore(Mathf.RoundToInt(gameTime));
                    highScores.SaveScoresToFile();
                }
                //*/

                break;

            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Mouse0) == true)
                {
                    gameTime = 0;
                    gameState = GameState.Game;
                    messageText.text = "";
                    timerText.gameObject.SetActive(true);

                    foreach (GameObject tank in tanks)
                    {
                        tank.SetActive(true);
                    }
                }
                break;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    private bool OneTankLeft()
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

    private bool IsPlayerDead()
    {
        foreach (GameObject tank in tanks)
        {
            if(tank.activeSelf == false)
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
