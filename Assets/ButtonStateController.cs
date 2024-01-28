using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStateController : MonoBehaviour
{

    private void Start()
    {
        AudioPlayer.instance.PlayBGM(0);
    }

    public void GoToScene(string name)
    {
        AudioPlayer.instance.PlaySFX(1);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        AudioPlayer.instance.PlaySFX(1);
        Application.Quit();
    }
}
