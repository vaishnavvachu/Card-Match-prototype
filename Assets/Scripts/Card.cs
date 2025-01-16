
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    public int cardID; 
    public bool isMatched = false;
    
    public SpriteRenderer frontSpriteRenderer;

    public void SetCardSprite(Sprite sprite)
    {
        frontSpriteRenderer.sprite = sprite;
    }
}



