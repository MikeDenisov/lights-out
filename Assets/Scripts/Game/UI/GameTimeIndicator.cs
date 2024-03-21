using System;
using TMPro;
using UnityEngine;

public class GameTimeIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    void Start()
    {
        UpdateTimer(GameManager.Instance.Statistics.RoundTime.Value);

        GameManager.Instance.Statistics.RoundTime.Subscribe(UpdateTimer);
    }

    void OnDestroy()
    {
        GameManager.Instance.Statistics.RoundTime.Unsubscribe(UpdateTimer);
    }

    private void UpdateTimer(TimeSpan value)
    {
        _text.text = value.ToString(@"mm\:ss");
    }
}
