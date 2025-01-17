using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    [SerializeField] private  GameObject levelSelectionMenu;
    [SerializeField] private  GameObject inGameUI;
    [SerializeField] private  TextMeshProUGUI turnsText; 
    [SerializeField] private  TextMeshProUGUI matchesText; 
    [SerializeField] private  GameObject gameOverCanvas;
    [SerializeField] private  TextMeshProUGUI gameOverTurnsText; 
    [SerializeField] private  TextMeshProUGUI gameOverMatchesText; 
    [SerializeField] private  TextMeshProUGUI comboText;
    [SerializeField] private  TextMeshProUGUI highestComboText; 
    public int Turns { get; private set; }
    public int Matches { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ShowLevelSelectionMenu();
        ResetUI();
    }

    public void ShowLevelSelectionMenu()
    {
        levelSelectionMenu.SetActive(true);
        inGameUI.SetActive(false);
        gameOverCanvas.SetActive(false);
    }
    public void ShowInGameUI()
    {
        levelSelectionMenu.SetActive(false);
        inGameUI.SetActive(true);
        gameOverCanvas.SetActive(false);
    }
    public void IncrementTurns()
    {
        Turns++;
        UpdateTurnsUI();
    }

    public void IncrementMatches()
    {
        Matches++;
        UpdateMatchesUI();
    }

    private void UpdateTurnsUI()
    {
        if (turnsText != null)
            turnsText.text = $"Turns: {Turns}";
    }

    private void UpdateMatchesUI()
    {
        if (matchesText != null)
            matchesText.text = $"Matches: {Matches}";
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
                gameOverTurnsText.text = $"Turns: {Turns}";

            if (gameOverMatchesText != null)
                gameOverMatchesText.text = $"Matches: {Matches}";
            
            if(highestComboText != null)
                highestComboText.text = $"High Combo: {GameManager.Instance.GetHighestCombo().ToString()}";
        }
    }

    public void HideGameOver()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }
    public void UpdateTurns(int turns)
    {
        Turns = turns;
        turnsText.text = "Turns: " + Turns;
    }

    public void UpdateMatches(int matches)
    {
        Matches = matches;
        matchesText.text = "Matches: " + Matches;
    }
    
    public void UpdateCombo(int combo)
    {
        if (comboText != null)
        {
            comboText.text = combo > 0 ? $"Combo: {combo}" : ""; // Show combo only if > 0
        }
    }
    
    public void OnSaveButtonPressed()
    {
        GameManager.Instance.SaveGame();
    }

    public void OnLoadButtonPressed()
    {
        GameManager.Instance.LoadGame();
    }
    public void ResetUI()
    {
        Turns = 0;
        Matches = 0;
        UpdateTurnsUI();
        UpdateMatchesUI();
        HideGameOver();
    }
    
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}