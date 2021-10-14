using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject basicCard;    //asset do modelo básico de uma carta
    public int id;                  //identificador do baralho
    public string [] backTypes;     //tipos/cores da parte de trás do baralho

    public List<GameObject> cards;  //lista dos GameObjects referentes às cartas do baralho

    //cria uma nova carta com a cor principal do baralho
    public GameObject NewCard(int value, string ident, string suit)
    {
        GameObject gameCard = (GameObject) Instantiate(basicCard, new Vector3(0, 0, 0), Quaternion.identity);
        gameCard.GetComponent<Card>().deck = this;
        gameCard.GetComponent<Card>().value = value;
        gameCard.GetComponent<Card>().ident = ident;
        gameCard.GetComponent<Card>().suit = suit;
        gameCard.GetComponent<Card>().sprite1 = (Sprite) Resources.Load<Sprite>(gameCard.GetComponent<Card>().ToString());
        gameCard.GetComponent<Card>().sprite2 = (Sprite)Resources.Load<Sprite>(this.backTypes[0]);
        gameCard.GetComponent<Card>().isHidden = true;
        gameCard.GetComponent<SpriteRenderer>().sprite = gameCard.GetComponent<Card>().sprite2;
        gameCard.name = (gameCard.GetComponent<Card>().ToString() + " : " + backTypes[0]);
        gameCard.transform.SetParent(GameObject.Find("gameDeck_"+this.id).transform);
        gameCard.transform.position = new Vector3(gameCard.transform.position.x, gameCard.transform.position.y, 0.01f*cards.Count);
        return gameCard;
    }

    //cria uma nova carta com uma cor que pode ser diferente da cor principal do baralho
    public GameObject NewCard(int value, string ident, string suit, string backType)
    {
        GameObject gameCard = (GameObject)Instantiate(basicCard, new Vector3(0, 0, 0), Quaternion.identity);
        gameCard.GetComponent<Card>().value = value;
        gameCard.GetComponent<Card>().ident = ident;
        gameCard.GetComponent<Card>().suit = suit;
        gameCard.GetComponent<Card>().sprite1 = (Sprite)Resources.Load<Sprite>(gameCard.GetComponent<Card>().ToString());
        gameCard.GetComponent<Card>().sprite2 = (Sprite)Resources.Load<Sprite>(backType);
        gameCard.GetComponent<SpriteRenderer>().sprite = gameCard.GetComponent<Card>().sprite2;
        gameCard.GetComponent<Card>().isHidden = true;
        gameCard.name = (gameCard.GetComponent<Card>().ToString() + " : " + backType);
        gameCard.transform.SetParent(GameObject.Find("gameDeck_" + this.id).transform);
        gameCard.transform.position = new Vector3(gameCard.transform.position.x, gameCard.transform.position.y, 0.1f * cards.Count);
        return gameCard;
    }

    //As cartas são adicionadas uma a uma no baralho, seguindo as especificações passadas
    public void SetDefaultDeck()
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
        this.cards = new List<GameObject>();
        cards.Add(NewCard(1, "card1", "random"));
        cards.Add(NewCard(2, "card2", "random"));
        cards.Add(NewCard(3, "card3", "random"));
        cards.Add(NewCard(4, "card4", "random"));
        cards.Add(NewCard(5, "card5", "random"));
        cards.Add(NewCard(6, "roxo", "solid"));
        cards.Add(NewCard(7, "marrom", "solid"));
        cards.Add(NewCard(8, "amarelo", "solid"));
        cards.Add(NewCard(9, "anil", "solid"));
    }

    public void SetPairDeck1()
    {
        this.cards = new List<GameObject>();
        cards.Add(NewCard(1, "alien", "pares1"));
        cards.Add(NewCard(2, "arroz", "pares1"));
        cards.Add(NewCard(3, "beija-flor", "pares1"));
        cards.Add(NewCard(4, "navegador", "pares1"));
    }

    public void SetPairDeck2()
    {
        this.cards = new List<GameObject>();
        cards.Add(NewCard(1, "espaco", "pares2"));
        cards.Add(NewCard(2, "feijao", "pares2"));
        cards.Add(NewCard(3, "flor", "pares2"));
        cards.Add(NewCard(4, "oceano", "pares2"));
    }

    //métodos responsável por embaralhar as cartas do baralho
    public void ShuffleDeck()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            int index = Random.Range(0, cards.Count);

            //como as sprites são renderizadas com base na distância que essas estão da câmera, é importante variar o valor
            //da posição "Z" de cada uma, para o caso de um tipo de jogo em que as cartas serão sobrepostas
            Vector3 pos1, pos2;
            pos1 = new Vector3(cards[i].transform.position.x, cards[i].transform.position.y, cards[index].transform.position.z);
            pos2 = new Vector3(cards[index].transform.position.x, cards[index].transform.position.y, cards[i].transform.position.z);
            
            GameObject aux = cards[i];
            cards[i] = cards[index];
            cards[index] = aux;

            cards[i].transform.position = pos2;
            cards[index].transform.position = pos1;

        }
    }
}
