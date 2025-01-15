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
        }
        else
        {
            secondCard = card;
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;

            Debug.Log("OK+++++++++++++Matched");
            // Handle match (e.g., scoring)
        }
        else
        {
            Debug.Log("X----------------MisMatched");
            Invoke(nameof(ResetCards), 1f);
        }
    }

    private void ResetCards()
    {
        firstCard = null;
        secondCard = null;
    }
}
