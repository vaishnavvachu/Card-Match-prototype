using DG.Tweening;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public static CardController Instance;
    private Card firstCard, secondCard;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(Card card)
    {
        if (card.isMatched || card == firstCard || secondCard != null) return;

        if (firstCard == null)
        {
            firstCard = card;
            ScaleCard(card.transform, true);
        }
        else
        {
            secondCard = card;
            ScaleCard(card.transform, true);
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        if (firstCard.cardID == secondCard.cardID)
        {
            // Cards match
            firstCard.isMatched = true;
            secondCard.isMatched = true;
            DestroyMatchedCards();
        }
        else
        {
            
            Invoke(nameof(ResetSelection), 0.5f); 
        }
    }


    private void DestroyMatchedCards()
    {
        firstCard.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(firstCard.gameObject));
        secondCard.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(secondCard.gameObject));

        firstCard = null;
        secondCard = null;
    }

    private void ResetSelection()
    {
        if (firstCard != null)
        {
            firstCard.GetComponent<CardFlip>().ResetFlip(); 
            firstCard = null;
        }
        if (secondCard != null)
        {
            secondCard.GetComponent<CardFlip>().ResetFlip(); 
            secondCard = null;
        }
    }


    private void ScaleCard(Transform cardTransform, bool scaleUp)
    {
        float targetScale = scaleUp ? 1.2f : 1f;
        cardTransform.DOScale(targetScale, 0.2f).SetEase(Ease.OutBack);
    }
}