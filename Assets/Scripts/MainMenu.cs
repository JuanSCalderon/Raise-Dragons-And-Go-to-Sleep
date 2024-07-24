using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Nombre de la música del menú principal y la música del juego
    public string mainMenuMusic = "MainMenuMusic";
    public string gameMusic = "GameMusic";
    public string sFxStart = "StartButton";

    private void Start()
    {
        // Reproducir la música del menú principal al inicio
        AudioManager.Instance.PlayMusic(mainMenuMusic);
    }

    public void PlayGame()
    {
        // Cambiar la música cuando se inicie el juego
        SceneManager.LoadSceneAsync(2);
        AudioManager.Instance.PlaySFX(sFxStart);
        AudioManager.Instance.PlayMusic(gameMusic);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
