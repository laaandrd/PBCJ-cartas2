using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Botões para selecionar o nível de jogo
public class modeButtonsManager : MonoBehaviour
{
   public void StartEasyGame()
    {
        PlayerPrefs.SetInt("gameMode", 1);
        SceneManager.LoadScene("GameScene");
    }

    public void StartRegularGame()
    {
        PlayerPrefs.SetInt("gameMode", 2);
        SceneManager.LoadScene("GameScene");
    }

    public void StartHardGame()
    {
        PlayerPrefs.SetInt("gameMode", 3);
        SceneManager.LoadScene("GameScene");
    }
}
