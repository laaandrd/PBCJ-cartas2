using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Card : MonoBehaviour
{
    public Deck deck;     //indica de qual baralho a crata faz parte
    public Sprite sprite1, sprite2; //sprites da frente e veros da carta
    
    public int value;       //indica um valor para carta caso seja necessário para jogabilidade
    public string ident;    //identificador da carta
    public string suit;     //naipe ou grupo do qual a carta faz parte
    
    public bool isHidden;   //booleano que indica se a carta está escondida ou revelada

    /*esse método está associado ao collider do GameObject das cartas e é responsável
     * por selecioná-los para o gameManager*/
    public void OnMouseDown()
    {
        GameObject.Find("gameManager").GetComponent<gameManager>().SelectCard(gameObject);
    }

    public void ShowCard()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
        isHidden = false;
    }
    public void HideCard()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
        isHidden = true;
    }

    /*esse método é responsável por montar a string associada ao GameObject da carta;
     *é utilizado para carregar a sprite da face revelada dessa carta*/
    public override string ToString()
    {
        return ident + "_" + suit;
    }

}
