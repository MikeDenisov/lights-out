using UnityEngine;

/// <summary>
/// Entry point for the game system
/// </summary>
public class LightsOutSystem : MonoBehaviour
{
    [SerializeField] LightsOutGameView _view;

    private LightsOutGamePresenter _presenter;

    public void Setup(GameSettings settings)
    {
        if (_presenter != null)
            _presenter.Dispose();

        _presenter = new LightsOutGamePresenter.Builder()
            .WithSettings(settings)
            .Build(_view);

        _presenter.Shuffle(settings.Difficulty);
    }
}
