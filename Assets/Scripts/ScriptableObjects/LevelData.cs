using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "CardGame/Level")]
public class LevelData : ScriptableObject
{
    public string levelName;  
    public int rows;  
    public int cols;  
    public List<Sprite> cardSprites;  
    public Sprite backSprite;
}
