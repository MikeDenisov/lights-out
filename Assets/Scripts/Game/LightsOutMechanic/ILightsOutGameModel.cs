using System.Collections.Generic;
/// <summary>
/// Game logic model
/// </summary>
public interface ILightsOutGameModel
{
    /// <summary>
    /// Rows count
    /// </summary>
    int Rows { get; }

    /// <summary>
    /// Columns count
    /// </summary>
    int Columns { get; }

    /// <summary>
    /// Total items count
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Enumerate all existing indexes and their states
    /// </summary>
    IEnumerable<(int, bool)> AllItems { get; }

    /// <summary>
    /// True if all items are off
    /// </summary>
    bool IsComplete { get; }

    /// <summary>
    /// Toggle item at index
    /// </summary>
    /// <param name="index">item index</param>
    /// <returns>Affected items indexes</returns>
    int[] ToggleItem(int index);

    /// <summary>
    /// Get state of item at index
    /// </summary>
    /// <param name="index">Item index</param>
    /// <returns>Item state</returns>
    bool GetItemState(int index);
}
