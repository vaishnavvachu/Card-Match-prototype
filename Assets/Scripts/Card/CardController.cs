using DG.Tweening;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public static CardController Instance;
    private Card _firstCard, _secondCard;
    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(Card card)
    {
        if (card.isMatched || card == _firstCard || _secondCard != null) return;

        AudioManager.Instance.PlayCardClickSound();
        
        if (_firstCard == null)
        {
            _firstCard = card;
            ScaleCard(card.transform, true);
        }
        else
        {
            _secondCard = card;
            ScaleCard(card.transform, true);
            CheckMatch();
        }
    }


    private void CheckMatch()
    {
     
        UIManager.Instance.IncrementTurns();
        if (_firstCard.cardID == _secondCard.cardID)
        {
            // Cards match
            _firstCard.isMatched = true;
            _secondCard.isMatched = true;
            UIManager.Instance.IncrementMatches(); 
            AudioManager.Instance.PlayCardMatchSound();
            DestroyMatchedCards();
            GameManager.Instance.CardMatched();
            GameManager.Instance.IncreaseCombo();
        }
        else
        {
            AudioManager.Instance.PlayCardMismatchSound();
            GameManager.Instance.ResetCombo();
            Invoke(nameof(ResetSelection), 0.5f); 
        }
    }
    
    private void DestroyMatchedCards()
    {
        if (_firstCard != null)
        {
            _firstCard.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                if (_firstCard != null) 
                {
                    Destroy(_firstCard.gameObject);
                }
            });
        }
        if (_secondCard != null)
        {
            _secondCard.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
            {
                if (_secondCard != null) 
                {
                    Destroy(_secondCard.gameObject);
                }
            });
        }
        _firstCard = null;
        _secondCard = null;
    }


    private void ResetSelection()
    {
        if (_firstCard != null)
        {
            ResetCardState(_firstCard);
            _firstCard = null;
        }
        if (_secondCard != null)
        {
            ResetCardState(_secondCard);
            _secondCard = null;
        }
    }

    private void ResetCardState(Card card)
    {
        card.ResetFlip(); 
        card.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack); 
    }



    private void ScaleCard(Transform cardTransform, bool scaleUp)
    {
        float targetScale = scaleUp ? 1.2f : 1f;
        cardTransform.DOScale(targetScale, 0.2f).SetEase(Ease.OutBack);
    }
}