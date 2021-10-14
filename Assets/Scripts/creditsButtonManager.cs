using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Botão para mostrar os creditos
public class creditsButtonManager : MonoBehaviour
{
    public void StartCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
