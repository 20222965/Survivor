using UnityEngine;

public class PassiveStatus
{
    public int Level { get; set; }
    public PassiveData Data { get; set; }

    public PassiveStatus(PassiveData data)
    {
        Level = 1;
        Data = data;
    }

    public void LevelUp()
    {
        if (Level < Data.MaxLevel)
        {
            ++Level;
        }
    }

    public float GetValue()
    {
        return Data.Values[Mathf.Min(Level, Data.Values.Length) - 1];
    }
}
