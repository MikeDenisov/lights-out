public partial class GameManager
{
    private abstract class State
    {
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }

    /// <summary>
    /// Initialize scene and wait for player first turn
    /// </summary>
    private class IdleState: State
    {
        public override void OnEnter()
        {
            Instance.InitializeGame();
            Instance.Statistics.ResetStatistics();
            GameEvents.PlayerTurn.Subscribe(OnGameTurn);
        }

        public override void OnExit()
        {
            GameEvents.PlayerTurn.UnSubscribe(OnGameTurn);
        }

        private void OnGameTurn()
        {
            Instance.SetState(new InGameState());
        }
    }

    /// <summary>
    /// Track player moves and wait for vicroty event
    /// </summary>
    private class InGameState: State
    {
        public override void OnEnter()
        {
            Instance.Statistics.AddTurn();
            Instance.Statistics.StartGameTimer();

            GameEvents.PlayerTurn.Subscribe(OnGameTurn);
            GameEvents.Victory.Subscribe(OnVictory);
        }

        public override void OnExit()
        {
            Instance.Statistics.StopGameTimer();

            GameEvents.PlayerTurn.UnSubscribe(OnGameTurn);
            GameEvents.Victory.UnSubscribe(OnVictory);
        }

        private void OnGameTurn()
        {
            Instance.Statistics.AddTurn();
        }

        private void OnVictory()
        {
            Instance.SetState(new VictoryState());
        }
    }

    /// <summary>
    /// Show vicory popup and handle game restart
    /// </summary>
    private class VictoryState: State
    {
        public override void OnEnter()
        {
            PopupManager.Instance.ShowVictoryPopup();

            GameEvents.GameRestart.Subscribe(OnGameRestart);
        }

        public override void OnExit()
        {
            GameEvents.GameRestart.UnSubscribe(OnGameRestart);
        }

        private void OnGameRestart()
        {
            Instance.SetState(new IdleState());
        }
    }
}
