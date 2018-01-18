using System.Collections.Generic;

[System.Serializable]
public class LevelStats
{
    public bool hasAllCrystals = false;
    public bool hasAllFruits = false;
    public bool levelPassed = false;
   
    public List<int> collectedFruits = new List<int>();
}
