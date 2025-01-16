
using DG.Tweening;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    private bool isFlipped = false;
    private SpriteRenderer spriteRenderer;
    //public Sprite frontSprite; 
    //public Sprite backSprite;  

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ShowBack(); 
    }

    public void ResetFlip()
    {
        isFlipped = false;
        ShowBack();
        transform.rotation = Quaternion.identity; 
    }


    private void ShowBack()
    {
        //spriteRenderer.sprite = backSprite;
        transform.rotation = Quaternion.identity; 
    }

    private void ShowFront()
    {
        //spriteRenderer.sprite = frontSprite;
    }

    private void OnMouseDown()
    {
        if (!isFlipped)
        {
            isFlipped = true;
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
}


