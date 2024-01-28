using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private Vector2 spawnPoint;

    [SerializeField]
    private Image healthBar;


    public int MusuhPlayer;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        MoveToSpawnPoint();
        SetHealthBar(health);
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void SetHealthBar(int currentHealth)
    {
        float a = currentHealth;
        float crnt = a / 100f;
        healthBar.fillAmount = crnt;
    }

    private void MoveToSpawnPoint()
    {
        transform.position = spawnPoint;
    }

    private void GameOver()
    {
        MainMenuController.instance.GameOver("Player " + MusuhPlayer + " WIN");
    }
}
