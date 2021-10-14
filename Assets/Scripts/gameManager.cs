using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject centroDaTela;     //refer�ncia ao centro da tela
    public GameObject deck;             //prefab do GameObject deck

    public List<GameObject> decks;                  //lista de decks (caso mais de um seja usado) presentes no jogo
    public GameObject card1 = null, card2 = null;   //referencias �s cartas selecionadas no jogo

    public int gameMode;        //PlayerPref utilizado na sele��o do modo de jogo
    public int easyGameRecord;      //PlayerPref utilizado para marcar o record de cada modo de jogo
    public int regularGameRecord;
    public int hardGameRecord;
    public int score;           //vari�vel utilizada para marcar o score de uma partida
    public int numAttempts = 0; // variavel que armazena a quantidade de tentativas 

    float timeHolder = 0.0f;
    bool isTimerOn;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("numAttempts").GetComponent<Text>().text = "Tentativas: " + numAttempts;
        PlayerPrefs.SetInt("numAttempts", numAttempts);

        int record = FindObjectOfType<scoreList>().GetRecord();
        GameObject.Find("record").GetComponent<Text>().text = "Record: " + record;

        gameMode = PlayerPrefs.GetInt("gameMode");
        
        //modo de jogo EASY: dois baralhos de diferentes cores, encontrar um par de cartas iguais
        if(gameMode == 1)
        {
            easyGameRecord = PlayerPrefs.GetInt("easyGameRecord");
            OpenDefaultDeck("preto");
            OpenDefaultDeck("branco");
        }
    
        //modo de jogo REGULAR: ??? (sugest�o: dois baralhos diferentes, definir um par de cartas relacionadas, n�o iguais)
        if(gameMode == 2)
        {
            regularGameRecord = PlayerPrefs.GetInt("regularGameRecord");
            OpenPairDeck("preto", 1);
            OpenPairDeck("branco", 2);
        }

        //modo de jogo HARD: ??? (sugest�o: um misto dos dois jogos acima)
        if(gameMode == 3)
        {
            hardGameRecord = PlayerPrefs.GetInt("hardGameRecord");
            OpenDefaultDeck("preto");
            OpenDefaultDeck("branco");
            OpenPairDeck("preto", 1);
            OpenPairDeck("branco", 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            timeHolder += Time.deltaTime;
            if (timeHolder > 1.3f)
            {
                UpdateAttempts(); // Atualiza a quantidade de tentativas
                VerifyCards();
            }
        }
        
    }

    public void OpenDefaultDeck(string backType)
    {
        this.centroDaTela = GameObject.Find("centroDaTela");
        Vector3 pos = new Vector3(centroDaTela.transform.position.x, centroDaTela.transform.position.y, centroDaTela.transform.position.z);
        GameObject gameDeck = (GameObject)Instantiate(deck, pos, Quaternion.identity);
        gameDeck.GetComponent<Deck>().backTypes[0] = backType;
        gameDeck.GetComponent<Deck>().id = decks.Count;
        gameDeck.name = "gameDeck_" + gameDeck.GetComponent<Deck>().id;
        gameDeck.GetComponent<Deck>().SetDefaultDeck();
        gameDeck.GetComponent<Deck>().ShuffleDeck();
        decks.Add(gameDeck);
        OrganizeCards();
    }

    public void OpenPairDeck(string backType, int pairDeck)
    {
        this.centroDaTela = GameObject.Find("centroDaTela");
        Vector3 pos = new Vector3(centroDaTela.transform.position.x, centroDaTela.transform.position.y, centroDaTela.transform.position.z);
        GameObject gameDeck = (GameObject)Instantiate(deck, pos, Quaternion.identity);
        gameDeck.GetComponent<Deck>().backTypes[0] = backType;
        gameDeck.GetComponent<Deck>().id = decks.Count;
        gameDeck.name = "gameDeck_" + gameDeck.GetComponent<Deck>().id;
        if(pairDeck == 1)
        {
            gameDeck.GetComponent<Deck>().SetPairDeck1();
        }
        else if(pairDeck == 2)
        {
            gameDeck.GetComponent<Deck>().SetPairDeck2();
        }
        gameDeck.GetComponent<Deck>().ShuffleDeck();
        decks.Add(gameDeck);
        OrganizeCards();
    }

    public void OrganizeCards()
    {
        int decksCount = 1;
        foreach(GameObject gameDeck in decks)
        {
            float yPosition = -2.8f * ((float)decksCount - ((float)decks.Count +1.0f)/2.0f);    //posiciona cada baralho em uma linha
            //print("yPos = " + yPosition);
            int cardsCount = 1;
            foreach(GameObject gameCard in gameDeck.GetComponent<Deck>().cards)
            {
                float xPosition = 2.5f * ((float)cardsCount - ((float)gameDeck.GetComponent<Deck>().cards.Count +1.0f)/2.0f);   //posiciona cada carta em uma coluna
                //print("xPos = " + xPosition);
                Vector3 newPosition = new Vector3(xPosition, yPosition, gameCard.transform.position.z);
                gameCard.transform.position = newPosition;
                cardsCount++;
            }
            decksCount++;
        }
    }

    public void SelectCard(GameObject card)
    {
        if(card1 == null)
        {
            card1 = card;
            card.GetComponent<Card>().ShowCard();
        }
        else if(card2 == null && (card.GetComponent<Card>().deck.GetComponent<Deck>().id % 2 ) != card1.GetComponent<Card>().deck.GetComponent<Deck>().id % 2)
        {
            card2 = card;
            card.GetComponent<Card>().ShowCard();
            isTimerOn = true;
        }
    }

    public void VerifyCards()
    {
        // Essa verifica��o � para o modo f�cil do jogo
        if (card1 != null && card2 != null)
        {
            if((card1.GetComponent<Card>().suit == "pares1" || card1.GetComponent<Card>().suit == "pares2") && (card2.GetComponent<Card>().suit == "pares1" || card2.GetComponent<Card>().suit == "pares2"))
            {
                if (card1.GetComponent<Card>().value == card2.GetComponent<Card>().value)
                {
                    card1.GetComponent<Card>().deck.GetComponent<Deck>().cards.Remove(card1);   //remove a carta1 do seu deck
                    card2.GetComponent<Card>().deck.GetComponent<Deck>().cards.Remove(card2);   //remove a carta2 do seu deck
                    Destroy(card1);     //destr�i o GameObject referente � carta1
                    Destroy(card2);     //destr�i o GameObject referente � carta2
                }
                else
                {
                    card1.GetComponent<Card>().HideCard();
                    card2.GetComponent<Card>().HideCard();

                    card1 = null;
                    card2 = null;
                }
            }
            else if ((card1.GetComponent<Card>().suit == "random" || card1.GetComponent<Card>().suit == "solid") && (card2.GetComponent<Card>().suit == "random" || card2.GetComponent<Card>().suit == "solid"))
            {
                if (card1.GetComponent<Card>().ToString().Equals(card2.GetComponent<Card>().ToString()))
                {
                    card1.GetComponent<Card>().deck.GetComponent<Deck>().cards.Remove(card1);   //remove a carta1 do seu deck
                    card2.GetComponent<Card>().deck.GetComponent<Deck>().cards.Remove(card2);   //remove a carta2 do seu deck
                    Destroy(card1);     //destr�i o GameObject referente � carta1
                    Destroy(card2);     //destr�i o GameObject referente � carta2
                }
                else
                {
                    card1.GetComponent<Card>().HideCard();
                    card2.GetComponent<Card>().HideCard();

                    card1 = null;
                    card2 = null;
                }
            }
            else
            {
                card1.GetComponent<Card>().HideCard();
                card2.GetComponent<Card>().HideCard();

                card1 = null;
                card2 = null;
            }
            
            isTimerOn = false;
            timeHolder = 0.0f;
            
            int cardsLeft = 0;
            foreach(GameObject deck in decks)
            {
                foreach(GameObject card in deck.GetComponent<Deck>().cards)
                {
                    if(card != null)
                    {
                        cardsLeft++; //faz a contagem do n�mero de cartas restante em todos os baralhos
                    }
                }
            }

            print("Cards left: " + cardsLeft);

            if(cardsLeft == 0)
            {
                print("VITORIA, FIM DE JOGO"); //a vit�ria acontece quando n�o h� mais cartas
                FindObjectOfType<scoreList>().AddScore(numAttempts);
                SceneManager.LoadScene("EndScene"); // chama a tela de finalização
            }

        }
    }

    // Função que incrementa em um a quantidade de tentativas e a salva
    public void UpdateAttempts() 
    {
        numAttempts++;
        GameObject.Find("numAttempts").GetComponent<Text>().text = "Tentativas: " + numAttempts;
        PlayerPrefs.SetInt("numAttempts", numAttempts);
    }
}
