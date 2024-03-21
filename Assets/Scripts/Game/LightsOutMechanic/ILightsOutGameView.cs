using System;

/// <summary>
/// Game View
/// </summary>
public interface ILightsOutGameView
{
    /// <summary>
    /// Raised when player clicks on items, passing its index
    /// </summary>
    event Action<int> ItemClicked;

    /// <summary>
    /// Initialize view state
    /// </summary>
    /// <param name="rows">Required rows count</param>
    /// <param name="columns">Required coulumns count</param>
    void Initialize(int rows, int columns);

    /// <summary>
    /// Set item state by index
    /// </summary>
    /// <param name="index">Item index</param>
    /// <param name="turnedOn">Item state</param>
    void SetLightState(int index, bool turnedOn);
}
