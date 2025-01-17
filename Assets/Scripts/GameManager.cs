using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private  GameObject cardPrefab; 
    [SerializeField] private  Transform cardParent;
    [SerializeField] private  Vector2 gridSize;
    [SerializeField] private  GridLayoutGroup gridLayoutGroup;
    [SerializeField] private  List<Sprite> cardFrontSprites;
    [SerializeField] private  Sprite backSprite;

    private int[] _cardIDs;
    private int _totalCards;
    private int _matchedCards;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GenerateGrid((int)gridSize.x, (int)gridSize.y);
    }

    void GenerateGrid(int rows, int cols)
    {
        AdjustGridLayout(rows, cols);
        
        _totalCards = rows * cols;

        if (_totalCards % 2 != 0)
        {
            Debug.LogError("Grid must have an even number of cards for proper matching.");
            return;
        }

        _matchedCards = 0; 
        _cardIDs = new int[_totalCards];
        for (int i = 0; i < _totalCards / 2; i++)
        {
            _cardIDs[i * 2] = i;
            _cardIDs[i * 2 + 1] = i;
        }

        ShuffleArray(_cardIDs);

        int pairsRequired = rows>cols ? cols : rows;

        if (cardFrontSprites.Count < pairsRequired)
        {
            Debug.LogError("Not enough sprites.");
            return;
        }

        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _totalCards; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardParent);
            Card cardComp = card.GetComponent<Card>();

            cardComp.cardID = _cardIDs[i];
            Sprite cardSprite = cardFrontSprites[_cardIDs[i]];
            cardComp.SetCardFrontSprite(cardSprite);
            cardComp.SetCardBackSprite(backSprite);
            cardComp.ShowFrontInitially();
        }
    }
    void AdjustGridLayout(int rows, int cols)
    {
        RectTransform parentRect = cardParent.GetComponent<RectTransform>();

        if (gridLayoutGroup == null || parentRect == null)
        {
            Debug.LogError("CardParent must have a GridLayoutGroup and RectTransform.");
            return;
        }
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = cols;

        float parentWidth = parentRect.rect.width;
        float parentHeight = parentRect.rect.height;

        float cellWidth = (parentWidth - gridLayoutGroup.spacing.x * (cols - 1)) / cols;
        float cellHeight = (parentHeight - gridLayoutGroup.spacing.y * (rows - 1)) / rows;

        float cardSize = Mathf.Min(cellWidth, cellHeight);
        gridLayoutGroup.cellSize = new Vector2(cardSize, cardSize);
    }



    public void CardMatched()
    {
        _matchedCards += 2; 
        if (_matchedCards >= _totalCards)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! All cards matched.");
        AudioManager.Instance.PlayGameOverSound();
        UIManager.Instance.ShowGameOver();
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }
}
