using TMPro;
using UnityEngine;

public class TurnCounterIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    void Start()
    {
        UpdateCounter(GameManager.Instance.Statistics.TurnsCount.Value);

        GameManager.Instance.Statistics.TurnsCount.Subscribe(UpdateCounter);
    }

    void OnDestroy()
    {
        GameManager.Instance.Statistics.TurnsCount.Unsubscribe(UpdateCounter);
    }

    private void UpdateCounter(int value)
    {
        _text.text = value.ToString();
    }
}
