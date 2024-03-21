using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings")]
public class GameSettings : ScriptableObject
{
    public int Rows;
    public int Columns;
    
    [Range(3, 20)]
    public int Difficulty;

    private void OnValidate()
    {
        if (Rows * Columns > 64)
        {
            Debug.LogError("Board size over 64 items not supported");
        }
    }
}
