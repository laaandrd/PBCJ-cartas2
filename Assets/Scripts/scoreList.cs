using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class scoreList : MonoBehaviour
{
    private List<int> scores; // lista de pontuações
    public static scoreList instance; // Instancia da lista de pontuações

    void Awake()
    {
        /*Instancia o Gerenciador de pontuações e o destroi caso j� exista*/
        if (instance == null) instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }

        /*Evita que o Gerenciador de pontuações seja destruido ao abrir outra cena*/
        DontDestroyOnLoad(gameObject);

        // Lê a lista de pontuações salva na memória
        string recordList = PlayerPrefs.GetString("RecordList");
        scores = new List<int>(Array.ConvertAll(recordList.Split(' '), int.Parse));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pega a melhor pontuação, ou seja, o record    
    public int GetRecord()
    {
        return scores != null ? scores.First() : 0;
    }

    // Método para adicionar uma nova pontuação a lista
    public void AddScore(int record) 
    {
        if (scores == null) scores = new List<int>();
        
        // Adiciona a pontuação e ordena as pontuações
        scores.Add(record);
        scores.Sort();

        // Remove o 21ª pontuação, uma vez que apenas 20 pontuações são mostradas na tela
        if (scores.Count > 20) scores.RemoveAt(scores.Count - 1);

        // Salva a lista de pontuações
        string recordList = string.Join(" ", scores);
        PlayerPrefs.SetString("RecordList", recordList);
    }

    // Retorna um array com a lista de pontuações
    public int[] ReadList() 
    {
        return scores.ToArray();
    }
}
