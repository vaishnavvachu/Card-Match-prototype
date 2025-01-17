using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI turnsText; 
    public TextMeshProUGUI matchesText; 
    public GameObject gameOverCanvas;
    public TextMeshProUGUI gameOverTurnsText; 
    public TextMeshProUGUI gameOverMatchesText; 

    private int _turns = 0;
    private int _matches = 0; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ResetUI();
    }

    public void IncrementTurns()
    {
        _turns++;
        UpdateTurnsUI();
    }

    public void IncrementMatches()
    {
        _matches++;
        UpdateMatchesUI();
    }

    private void UpdateTurnsUI()
    {
        if (turnsText != null)
            turnsText.text = $"Turns: {_turns}";
    }

    private void UpdateMatchesUI()
    {
        if (matchesText != null)
            matchesText.text = $"Matches: {_matches}";
    }

    public void ShowGameOver()
    {
       Invoke("ShowResultAfterDelay",0.5f);
    }

    void ShowResultAfterDelay()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);

            if (gameOverTurnsText != null)
                gameOverTurnsText.text = $"Turns: {_turns}";

            if (gameOverMatchesText != null)
                gameOverMatchesText.text = $"Matches: {_matches}";
        }
    }

    public void HideGameOver()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void ResetUI()
    {
        _turns = 0;
        _matches = 0;
        UpdateTurnsUI();
        UpdateMatchesUI();
        HideGameOver();
    }
}