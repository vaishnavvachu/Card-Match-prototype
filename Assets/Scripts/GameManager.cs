using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject cardPrefab; 
    public Transform cardParent; 
    public Vector2 gridSize; 
    public float spacing = 10f;
    public List<Sprite> allSprites;
    public Sprite backSprite;

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
        
        if (allSprites.Count < pairsRequired)
        {
            Debug.LogError("Not enough sprites.");
            return;
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int index = row * cols + col;
                Vector3 position = new Vector3(col * spacing, -row * spacing, 0);

                GameObject card = Instantiate(cardPrefab, position, Quaternion.identity, cardParent);
                Card cardComp = card.GetComponent<Card>();

                cardComp.cardID = _cardIDs[index];
                Sprite cardSprite = allSprites[_cardIDs[index]];
                cardComp.SetCardFrontSprite(cardSprite);
                cardComp.SetCardBackSprite(backSprite);
                cardComp.ShowFrontInitially();
            }
        }
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
