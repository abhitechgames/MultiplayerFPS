using System.Collections;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image blueHealthBar;
    [SerializeField] private Image redHealthBar;

    [SerializeField] private TMP_Text blueScore;
    [SerializeField] private TMP_Text redScore;

    public int blueLife = 10;
    public int redLife = 10;

    [SerializeField] private TMP_Text blueWins;
    [SerializeField] private TMP_Text redWins;

    public void BlueHealthBarUpdate(float health)
    {
        blueHealthBar.fillAmount = health / 100f;
        if (health < 0)
        {
            blueHealthToZero();
        }
    }
    public void RedHealthBarUpdate(float health)
    {
        redHealthBar.fillAmount = health / 100f;
        if (health < 0)
        {
            blueHealthToZero();
        }
    }

    public void blueHealthToZero()
    {
        blueLife -= 1;
        if (blueLife <= 0)
        {
            blueWins.gameObject.SetActive(true);
            PlayerHealthSystem[] playerHealthSystems = FindObjectsOfType<PlayerHealthSystem>();
            foreach (var P in playerHealthSystems)
            {
                P.enabled = false;
            }
            this.enabled = false;
        }

        UpdateScore();
    }

    public void redHealthToZero()
    {
        redLife -= 1;
        if (redLife <= 0)
        {
            redWins.gameObject.SetActive(true);
            PlayerHealthSystem[] playerHealthSystems = FindObjectsOfType<PlayerHealthSystem>();
            foreach (var P in playerHealthSystems)
            {
                P.enabled = false;
            }
            this.enabled = false;
        }
        UpdateScore();
    }

    public void UpdateScore()
    {
        blueScore.text = blueLife.ToString();
        redScore.text = redLife.ToString();
    }
}
