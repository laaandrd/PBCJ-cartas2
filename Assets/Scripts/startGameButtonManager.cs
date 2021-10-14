using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGameButtonManager : MonoBehaviour
{
    public void StarSelectGameModeScene()
    {
        SceneManager.LoadScene("SelectGameModeScene");
    }
}
