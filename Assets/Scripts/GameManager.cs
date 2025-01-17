using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private  GameObject cardPrefab; 
    [SerializeField] private  Transform cardParent;
    [SerializeField] private  Vector2 gridSize;
    [SerializeField] private  GridLayoutGroup gridLayoutGroup;
    [SerializeField] private  List<Sprite> cardFrontSprites;
    [SerializeField] private LevelData currentLevelData;
    private int[] _cardIDs;
    private int _totalCards;
    private int _matchedCards;
    private int _currentCombo;
    private int _highestCombo;

   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
    }

    void Start()
    {
        if (currentLevelData != null)
        {
            GenerateGrid(currentLevelData.rows, currentLevelData.cols, currentLevelData.cardSprites, currentLevelData.backSprite);
        }
        
    }

    void GenerateGrid(int rows, int cols, List<Sprite> cardSprites, Sprite backSprite)
    {
        AdjustGridLayout(rows, cols);
        cardFrontSprites = cardSprites;
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
            Sprite cardSprite = cardFrontSprites[_cardIDs[i] % cardSprites.Count];
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
    public void ResetCombo()
    {
        _currentCombo = 0;
        UIManager.Instance.UpdateCombo(_currentCombo); 
    }

    public void IncreaseCombo()
    {
        _currentCombo++;
        if (_currentCombo > _highestCombo)
            _highestCombo = _currentCombo;

        UIManager.Instance.UpdateCombo(_currentCombo); 
    }

    public int GetHighestCombo()
    {
        return _highestCombo;
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
    public void SetLevel(LevelData levelData)
    {
        currentLevelData = levelData;
        GenerateGrid(levelData.rows, levelData.cols, levelData.cardSprites, levelData.backSprite);
    }
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            rows = (int)gridSize.x,
            cols = (int)gridSize.y,
            cardIDs = new List<int>(_cardIDs),
            matchedCards = new List<bool>(),
            turns = UIManager.Instance.Turns,
            matches = UIManager.Instance.Matches
        };

        foreach (Transform cardTransform in cardParent)
        {
            Card card = cardTransform.GetComponent<Card>();
            saveData.matchedCards.Add(card.isMatched);
        }

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved!");
    }
    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            Debug.LogError("No save data found!");
            return;
        }

        string json = PlayerPrefs.GetString("SaveData");
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        if (saveData.matchedCards.Count != _cardIDs.Length)
        {
            Debug.LogError("Saved matched card data doesn't match the current grid size.");
            return;
        }
        gridSize = new Vector2(saveData.rows, saveData.cols);
        _cardIDs = saveData.cardIDs.ToArray();

        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
        GenerateGrid(saveData.rows, saveData.cols,currentLevelData.cardSprites, currentLevelData.backSprite);

        for (int i = 0; i < cardParent.childCount; i++)
        {
            Card card = cardParent.GetChild(i).GetComponent<Card>();
        
            if (i < saveData.matchedCards.Count)
            {
                card.isMatched = saveData.matchedCards[i];

                if (card.isMatched)
                {
                    card.gameObject.SetActive(false);
                }
            }
        }

        UIManager.Instance.UpdateTurns(saveData.turns);
        UIManager.Instance.UpdateMatches(saveData.matches);

        Debug.Log("Game Loaded!");
    }
    
}
