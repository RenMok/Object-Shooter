using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int totalScore;
    private int highestScore;

    public static ScoreManager singleton;
    // Start is called before the first frame update
    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        highestScore = PlayerPrefs.GetInt("HScore");
        totalScore = 0;
    }
    public void RegisterHighScore()
    {
        if (totalScore > highestScore)
        {
            highestScore = totalScore;
            PlayerPrefs.SetInt("HScore", highestScore);
        }
    }
    public void IncreaseScore() {
        totalScore += 1;
    }
}
