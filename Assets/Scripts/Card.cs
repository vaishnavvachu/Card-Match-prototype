
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public int cardID; 
    public bool isMatched = false;
    public Image cardImage;
    public Sprite frontSprite; 
    public Sprite backSprite;
    
    private bool _isFlipped = false;
    
    public void SetCardFrontSprite(Sprite sprite)
    {
        frontSprite = sprite;
        cardImage.sprite = sprite;
    }

    public void SetCardBackSprite(Sprite sprite)
    {
        backSprite = sprite;
        //spriteRenderer.sprite = sprite;
    }
    void Start()
    {
        ShowBack(); 
    }

    public void ResetFlip()
    {
        _isFlipped = false;
        ShowBack();
        transform.rotation = Quaternion.identity; 
    }


    private void ShowBack()
    {
        cardImage.sprite = backSprite;
        transform.rotation = Quaternion.identity; 
    }

    private void ShowFront()
    {
        cardImage.sprite = frontSprite;
    }

    public void OnMouseDown()
    {
        if (!_isFlipped)
        {
            _isFlipped = true;
            FlipCard();
            CardController.Instance.SelectCard(GetComponent<Card>());
        }
    }

    private void FlipCard()
    {
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            ShowFront();
            transform.DORotate(new Vector3(0, 0, 0), 0.25f);
        });
    }

   public void ShowFrontInitially()
    {
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            ShowFront();
            transform.DORotate(new Vector3(0, 0, 0), 0.25f);
        });
      Invoke("ResetFlip", 1.5f);
    }
    
}



