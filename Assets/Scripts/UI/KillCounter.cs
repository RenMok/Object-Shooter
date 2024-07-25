using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int killCount = 0;

    private void Start()
    {
       SetScore(killCount);
    }

    public void IncreaseScore()
    {
        killCount++;
        counterText.text = killCount.ToString("000");
        SetScore(killCount);
    }
    public void SetScore(int score)
    {
        PlayerPrefs.SetInt("CurrentScore", score);
        Debug.Log("current score " + PlayerPrefs.GetInt("CurrentScore"));
    }
}
