using UnityEngine;

public partial class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] GameSettings _settings;
    [SerializeField] LightsOutSystem _gameSystem;

    private State _currentState;

    public GameStatistics Statistics { get; } = new GameStatistics();

    void Start()
    {
        SetState(new IdleState());
    }

    private void SetState(State state)
    {
        if (_currentState != null)
            _currentState.OnExit();

        _currentState = state;
        _currentState.OnEnter();
    }

    private void InitializeGame()
    {
        _gameSystem.Setup(_settings);
    }
}
