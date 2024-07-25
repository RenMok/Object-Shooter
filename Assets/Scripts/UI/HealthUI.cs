using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private Image bar;
    [SerializeField] private Player player;

    private float healthIncrement;
    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();

        }
        player.healthPoints.OnHealthChanged.AddListener(ChangeHealth);
        healthIncrement = 1.0f / player.maxHealth;
        Debug.Log("healthIncrement = " + healthIncrement);
    }

    private void ChangeHealth(int health)
    {
        counterText.text = health.ToString("000");
        bar.fillAmount = healthIncrement * health;
    }
}
