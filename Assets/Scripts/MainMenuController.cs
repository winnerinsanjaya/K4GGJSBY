using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;

    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject gameplayScreen;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private TMP_Text winText;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    private void Start()
    {
        AudioPlayer.instance.PlayBGM(1);
    }

    public void PauseGame()
    {
        AudioPlayer.instance.PlaySFX(1);
        Time.timeScale = 0;
        gameplayScreen.SetActive(false);
        pauseScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }
    public void ResumeGame()
    {
        AudioPlayer.instance.PlaySFX(1);
        gameplayScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver(string Wtext)
    {
        Time.timeScale = 0;

        winText.text = Wtext;
        gameplayScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void RetryGame()
    {
        AudioPlayer.instance.PlaySFX(1);
        Time.timeScale = 1;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        AudioPlayer.instance.PlaySFX(1);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
