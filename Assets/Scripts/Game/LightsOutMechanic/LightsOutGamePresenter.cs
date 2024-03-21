using System;

using UnityEngine.Assertions;

/// <summary>
/// Joins together game view and model, and triggers game events
/// </summary>
public class LightsOutGamePresenter: IDisposable
{
    private readonly ILightsOutGameView _view;
    private readonly ILightsOutGameModel _model;

    LightsOutGamePresenter(ILightsOutGameView view, ILightsOutGameModel model)
    {
        _view = view;
        _model = model;

        ConnectView();
    }

    /// <summary>
    /// Shuffle game state
    /// </summary>
    /// <param name="stepsCount">Minimal number of actions required to solve the result puzzle</param>
    public void Shuffle(int stepsCount)
    {
        while (stepsCount-- > 0 || _model.IsComplete)
        {
            _model.ToggleItem(UnityEngine.Random.Range(0, _model.Length));
        }

        RefreshView();
    }

    private void ConnectView()
    {
        _view.Initialize(_model.Rows, _model.Columns);
        _view.ItemClicked += View_ItemClicked;

        RefreshView();
    }

    private void View_ItemClicked(int index)
    {
        MakeTurn(index);
    }

    private void MakeTurn(int index)
    {
        var affectedIndexes = _model.ToggleItem(index);
        RefreshView(affectedIndexes);

        GameEvents.PlayerTurn.Raise();

        if (_model.IsComplete)
        {
            GameEvents.Victory.Raise();
        }
    }

    private void RefreshView(int[] affectedItems = null)
    {
        // Update only affected items if known
        if (affectedItems != null)
        {
            foreach(var index in affectedItems)
            {
                _view.SetLightState(index, _model.GetItemState(index));
            }
        }
        // Full view refresh
        else
        {
            foreach (var (index, state) in _model.AllItems)
            {
                _view.SetLightState(index, state);
            }
        }
    }

    public void Dispose()
    {
        _view.ItemClicked -= View_ItemClicked;
    }

    public class Builder
    {
        readonly LightsOutGameModel model = new LightsOutGameModel();

        public Builder WithSettings(GameSettings settings)
        {
            model.SetBoardSize(settings.Columns, settings.Rows);

            return this;
        }

        public LightsOutGamePresenter Build(ILightsOutGameView view)
        {
            Assert.IsNotNull(view);

            return new LightsOutGamePresenter(view, model);
        }
    }
}
