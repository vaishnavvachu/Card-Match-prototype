
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    private bool isFlipped = false;
    

    private void OnMouseDown()
    {
        if (!isFlipped)
        {
            isFlipped = true;
            CardController.Instance.SelectCard(GetComponent<Card>());
        }
    }

    public void ResetFlip()
    {
        isFlipped = false;
    }
}

