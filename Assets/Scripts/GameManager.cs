using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private float gameTime = 0;
    public float GameTime { get { return gameTime; } }

    public CameraSwitch cameraSwitch;

    public GameObject aimIndicator;
    public GameObject playerTank;
    public GameObject[] enemyTanks;
    public GameObject playerSpawn;
    public GameObject[] sceneryProps;

    public HighScores highScores;
    public GameObject titlePanel;
    public GameObject highScoresPanel;
    public GameObject gameOverPanel;
    public Text highScoresText;
    public Text messageText;
    public Text timerText;

    public enum GameState
    {
        TitleScreen,
        PlayingGame,
        GameOver
    }
    private GameState gameState;
    public GameState State { get { return gameState; } }

    private void Awake()
    {
        gameState = GameState.TitleScreen;
    }

    void Update()
    {
		switch(gameState)
        {
            case GameState.TitleScreen:
                foreach (GameObject enemy in enemyTanks)
                    enemy.SetActive(false);
                playerTank.SetActive(false);

                Cursor.visible = true;
                aimIndicator.SetActive(false);
                cameraSwitch.GameCameraOff();

                titlePanel.SetActive(true);
                highScoresPanel.SetActive(false);
                gameOverPanel.SetActive(false);

                messageText.gameObject.SetActive(false);
                timerText.gameObject.SetActive(false);
                break;

            case GameState.PlayingGame:

                gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(gameTime);
                timerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

                bool isGameOver = false;
                if (NoEnemiesLeft() == true)
                    isGameOver = true;
                else if (IsPlayerDead() == true)
                    isGameOver = true;

                if (isGameOver == true)
                {
                    Cursor.visible = true;
                    aimIndicator.SetActive(false);
                    cameraSwitch.GameCameraOff();
                    gameOverPanel.SetActive(true);
                    messageText.gameObject.SetActive(true);
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

                    gameState = GameState.GameOver;
                }

                //DEBUG TOOL////////////////////////////////////////////////
                if (Input.GetKeyUp(KeyCode.P) == true)
                {
                    highScores.AddScore(Mathf.RoundToInt(gameTime));
                    highScores.SaveScoresToFile();
                }
                ////////////////////////////////////////////////////////////
                break;

            case GameState.GameOver:
                playerTank.SetActive(false);
                foreach (GameObject enemy in enemyTanks)
                    enemy.SetActive(false);
                break;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
	}

    private void ResetStage() //Resets moveable objects' transforms, reactivates tanks and health.
    {
        playerTank.transform.position = playerSpawn.transform.position;
        playerTank.transform.rotation = playerSpawn.transform.rotation;

        playerTank.SetActive(true);

        TankHealth playerHealth = playerTank.GetComponent<TankHealth>();
        playerHealth.Initialise();

        foreach (GameObject enemy in enemyTanks)
        {
            enemy.transform.localPosition = Vector3.zero;
            enemy.transform.localRotation = Quaternion.identity;
            TankHealth enemyHealth = enemy.GetComponent<TankHealth>();
            enemyHealth.Initialise();
            enemy.SetActive(true);
        }

        foreach (GameObject prop in sceneryProps)
        {
            prop.transform.localPosition = Vector3.zero;
            prop.transform.localRotation = Quaternion.identity;
        }
    }

    //BUTTONS///////////////////////////////////////////////////
    public void OnStart()
    {
        ResetStage();
        gameTime = 0;
        messageText.text = "";
        aimIndicator.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        titlePanel.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        cameraSwitch.GameCameraOn();
        Cursor.visible = false;
        gameState = GameState.PlayingGame;
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnReturnToTitle()
    {
        Cursor.visible = true;
        aimIndicator.SetActive(false);
        cameraSwitch.GameCameraOff();

        titlePanel.SetActive(true);
        highScoresPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        messageText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        foreach (GameObject enemy in enemyTanks)
            enemy.SetActive(false);
        playerTank.SetActive(false);

        gameState = GameState.TitleScreen;
    }

    public void OnHighScores()
    {
        messageText.gameObject.SetActive(false);
        titlePanel.SetActive(false);
        gameOverPanel.SetActive(false);
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
        gameOverPanel.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
    }

    //WIN CONDITIONS////////////////////////////////////////////
    private bool NoEnemiesLeft() //Returns true if all enemy tanks are inactive.
    {
        int numberOfTanks = 0;

        foreach (GameObject enemy in enemyTanks)
        {
            if (enemy.activeSelf == true)
                numberOfTanks++;
        }

        return (numberOfTanks == 0);
    }

    private bool IsPlayerDead() //Returns true if player tank is inactive.
    {
        if (playerTank.activeSelf == false)
            return true;

        return false;
    }
}
