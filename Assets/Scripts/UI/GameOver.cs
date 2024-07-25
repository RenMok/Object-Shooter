using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private Animator newHighScore;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private Animator characterAnimator;

    [Header ("Timing For Flashing Title")]
    [SerializeField] private GameObject title;
    [SerializeField] private float timeOn;
    [SerializeField] private float timeOff;

    private void Start()
    {
        menuButtons.SetActive (false);
        characterAnimator.SetBool("Play", false);
        newHighScore.gameObject.SetActive(false);
        StartCoroutine(FlashTitle());
        StartCoroutine(EnableMenu());
        DisplayScores();
    }
    public void DisplayScores()
    {

        highScore.text = FormatString(PlayerPrefs.GetInt("HighScore").ToString());
        playerScore.text = FormatString(PlayerPrefs.GetInt("CurrentScore").ToString());
        if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("HighScore"))
        {
            newHighScore.gameObject.SetActive(true);
            newHighScore.SetTrigger("NewHighScore");
            RegisterHighScore(PlayerPrefs.GetInt("CurrentScore"));
        }
        else
        {
            characterAnimator.SetBool("Play", true);
        }
    }
    public void RegisterHighScore(int score)
    {
            PlayerPrefs.SetInt("HighScore", score);
            Debug.Log("high score" + PlayerPrefs.GetInt("HighScore"));
    }
    private IEnumerator FlashTitle()
    {
        while (true)
        {
            title.SetActive(true);
            yield return new WaitForSeconds(timeOn);
            title.SetActive(false);
            yield return new WaitForSeconds(timeOff);
        }
    }
    private string FormatString(string output)
    {
        while (output.Length < 6)
        {
            output = "0" + output;
        }
        return output;
    }
    IEnumerator EnableMenu()
    {
        yield return new WaitForSecondsRealtime(4);
        menuButtons.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
