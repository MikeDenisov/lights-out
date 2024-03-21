using System;
using System.Collections.Generic;

public class LightsOutGameModel: ILightsOutGameModel
{
    private GameBoard _board;

    public int Columns { get; private set; }
    public int Rows { get; private set; }

    public int Length => Columns * Rows;
    public bool IsComplete => _board.IsComplete;

    public IEnumerable<(int, bool)> AllItems
    {
        get
        {
            for (int i = 0; i < Length; i++)
            {
                yield return (i, _board.GetValueAt(i));
            }
        }
    }

    public void SetBoardSize(int width, int height)
    {
        Columns = width;
        Rows = height;
        _board = new GameBoard(height, width);
    }

    public int[] ToggleItem(int index)
    {
        return _board.ToggleItem(index);
    }


    public bool GetItemState(int index)
    {
        return _board.GetValueAt(index);
    }


    /// <summary>
    /// Encapculates game state representation and its modification logic.
    /// This board implementation has limit of 64 items and O(1) win condition check.
    /// </summary>
    private class GameBoard
    {
        private static readonly (int, int)[] _lookupDirections = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

        private long _boardState;

        private readonly int _rows;
        private readonly int _columns;

        // If all bits are 0's - game is finished
        public bool IsComplete => _boardState == 0;

        public GameBoard(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;

            if (rows * columns > 64)
            {
                throw new ArgumentOutOfRangeException("Size of the board shouldn't exceed 64 items");
            }
        }

        public bool GetValueAt(int index)
        {
            // If bit at the index == 1 - item state is on
            return (_boardState & 1L << index) > 0;
        }

        public int[] ToggleItem(int index)
        {
            var (mask, toggledIndexes) = GetToggleMask(index);

            // Toggle affected items bits represented by bitmask using XOR
            _boardState ^= mask;

            return toggledIndexes;
        }

        /// <summary>
        /// Calculte a bitmask for items that are affected by toogle action
        /// </summary>
        /// <param name="index">Toggle item index</param>
        /// <returns>Bitmask + affected items indexes collection</returns>
        private (long, int[]) GetToggleMask(int index)
        {
            // collection to store affected indexes
            var affectedIndexes = new List<int>(5) { index };

            // initial mask starts with toggles item
            var mask = 1L << index;

            // toggled item 2D position in grid
            var (vIndex, hIndex) = IndexToPosition(index);

            // check negbour items in grid
            foreach (var (deltaV, deltaH) in _lookupDirections)
            {
                // vertical index
                var vI = vIndex + deltaV;
                // horizontal index
                var hI = hIndex + deltaH;

                // if item position out of the grid - skip it
                if (vI < 0 || vI >= _rows || hI < 0 || hI >= _columns)
                    continue;

                // index of the affected neighbour item
                var ind = PositionToIndex(vI, hI);

                // add affected item index to the bitmask
                mask |= 1L << ind;

                // store affected item index
                affectedIndexes.Add(ind);
            }

            return (mask, affectedIndexes.ToArray());
        }

        /// <summary>
        /// Convert sequential item index to a 2D grid position
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private (int, int) IndexToPosition(int index)
        {
            var verticalIndex = index / _columns;
            var horizontalIndex = index - verticalIndex * _columns;
            return (verticalIndex, horizontalIndex);
        }

        /// <summary>
        /// Converts 2D item position in grid to sequential index
        /// </summary>
        /// <param name="verticalIndex"></param>
        /// <param name="horizontalIndex"></param>
        /// <returns></returns>
        private int PositionToIndex(int verticalIndex, int horizontalIndex)
        {
            return (verticalIndex * _columns) + horizontalIndex;
        }
    }
}