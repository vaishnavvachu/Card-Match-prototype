using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    public Button[] levelButtons;  
    public LevelData[] allLevels;  

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i; 
            levelButtons[i].onClick.AddListener(() => OnLevelSelected(allLevels[levelIndex]));
            levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = allLevels[i].levelName;
        }
    }

    private void OnLevelSelected(LevelData levelData)
    {
        GameManager.Instance.SetLevel(levelData);
        UIManager.Instance.ShowInGameUI();
        gameObject.SetActive(false);
    }
}

