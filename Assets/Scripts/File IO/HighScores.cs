using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScores : MonoBehaviour {

    public int[] scores = new int[10];

    string currentDirectory;
    public string scoreFileName = "highscores.txt";

	void Start ()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Current directory is: " + currentDirectory);

        LoadScoresFromFile();
	}
	
	void Update ()
    {
		
	}

    //checks file exists, gets scores from score file
    public void LoadScoresFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);

        if(fileExists == true)
            Debug.Log("Found high score file " + scoreFileName);
        else
        {
            Debug.Log("The file " + scoreFileName + " does not exist. No scores will be loaded.");
            return;
        }

        scores = new int[scores.Length];
        int scoreCount = 0;

        StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFileName);

        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();
            int readScore = -1;
            bool didParse = int.TryParse(fileLine, out readScore);

            if (didParse)
                scores[scoreCount] = readScore;
            else
            {
                Debug.Log("Invalid line in scores file at " + scoreCount + ", using default value.", this);
                scores[scoreCount] = 0;
            }

            scoreCount++;
        }

        fileReader.Close();
        Debug.Log("High scores read from " + scoreFileName);
    }

    //writes scores to file
    public void SaveScoresToFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);

        for (int i = 0; i < scores.Length; i++)
            fileWriter.WriteLine(scores[i]);

        fileWriter.Close();
        Debug.Log("High scores written to " + scoreFileName);
    }

    //checks new score against old scores, finds place in index
    public void AddScore(int newScore)
    {
        int desiredIndex = -1;

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }

        if (desiredIndex < 0)
        {
            Debug.Log("Score of " + newScore + " was not a high score.", this);
            return;
        }

        for (int i = scores.Length - 1; i > desiredIndex; i--)
            scores[i] = scores[i - 1];

        scores[desiredIndex] = newScore;
        Debug.Log("Score of " + newScore + " is the new #" + (desiredIndex + 1) + " high score!", this);
    }
}
