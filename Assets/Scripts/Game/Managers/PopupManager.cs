using UnityEngine;

public class PopupManager : MonoSingleton<PopupManager>
{
    [SerializeField] VictoryPopup _victoryPopup;

    public void ShowVictoryPopup()
    {
        _victoryPopup.Show();
    }
}
