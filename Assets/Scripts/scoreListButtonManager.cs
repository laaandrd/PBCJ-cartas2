using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Botão para abrir a lista de pontuações
public class scoreListButtonManager : MonoBehaviour
{
    public void OpenRecordList()
    {
        SceneManager.LoadScene("ScoreScene");
    }
}
