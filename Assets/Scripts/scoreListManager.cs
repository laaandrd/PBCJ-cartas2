using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreListManager : MonoBehaviour
{
    // Start is called before the first frame update

    int[] scores; // Lista de pontuações
    public Text Score; // Prefab para cada pontuação
    int lines = 5; // Quantidade de linhas para apresentar as pontuações

    void Start()
    {
        scores = FindObjectOfType<scoreList>().ReadList(); // Carrega as Pontuações
        int i = 0;

        // Apresenta as pontuações, posicionando da com menos tentativas a com mais 
        foreach(int score in scores)
        {
            GameObject centroDaTela = GameObject.Find("centroDaTela");
            Vector3 pos = new Vector3(
                centroDaTela.transform.position.x - 250 + (i / lines * 160), 
                centroDaTela.transform.position.y + 80 - ((i % lines) * 30), 
                centroDaTela.transform.position.z
            );

            Text gameRecord = (Text)Instantiate(Score, pos, transform.rotation);
            gameRecord.name = (i+ 1) + "º score";
            gameRecord.transform.SetParent(GameObject.Find("Canvas").transform);
            gameRecord.text = (i + 1) + "º " + score + " tentativas";
            print(score + "\n");
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
