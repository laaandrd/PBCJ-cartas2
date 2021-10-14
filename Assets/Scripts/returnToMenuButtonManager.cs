using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Botão para retornar ao menu principal
public class returnToMenuButtonManager : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
