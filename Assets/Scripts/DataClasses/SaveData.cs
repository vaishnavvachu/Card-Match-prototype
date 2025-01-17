using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int rows;
    public int cols;
    public List<int> cardIDs;       
    public List<bool> matchedCards;
    public int turns;
    public int matches;
}