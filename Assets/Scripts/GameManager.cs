using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab; 
    public Transform cardParent; 
    public Vector2 gridSize; 
    public float spacing = 10f;
    private int[] cardIDs;

    void Start()
    {
        GenerateGrid((int)gridSize.x, (int)gridSize.y);
    }

    void GenerateGrid(int rows, int cols)
    {
        
        int totalCards = rows * cols;
        cardIDs = new int[totalCards];
        for (int i = 0; i < totalCards / 2; i++)
        {
            cardIDs[i * 2] = i;
            cardIDs[i * 2 + 1] = i;
        }

        ShuffleArray(cardIDs);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int index = row * cols + col;
                Vector3 position = new Vector3(col * spacing, -row * spacing, 0);
                GameObject card = Instantiate(cardPrefab, position, Quaternion.identity, cardParent);
                card.GetComponent<Card>().cardID = cardIDs[index];
            }
        }
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

