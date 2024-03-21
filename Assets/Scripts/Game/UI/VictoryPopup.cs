using UnityEngine;
using UnityEngine.UI;

public class VictoryPopup : MonoBehaviour
{
    [SerializeField] Button _replayButton;

    void OnEnable()
    {
        _replayButton.onClick.AddListener(OnReplayButtonClick);
    }

    void OnDisable()
    {
        _replayButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnReplayButtonClick()
    {
        GameEvents.GameRestart.Raise();
        Hide();
    }
}
