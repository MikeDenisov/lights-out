using System;
using System.Threading;
using System.Threading.Tasks;

public class GameStatistics
{
    public ReactiveProperty<TimeSpan> RoundTime = new ReactiveProperty<TimeSpan>(TimeSpan.Zero);
    public ReactiveProperty<int> TurnsCount = new ReactiveProperty<int>(0);

    private CancellationTokenSource _timerCTS;

    public void StartGameTimer()
    {
        _timerCTS?.Cancel();
        _timerCTS = new CancellationTokenSource();

        TimerUpdateTask(_timerCTS.Token).ConfigureAwait(false);
    }

    public void StopGameTimer()
    {
        _timerCTS?.Cancel();
    }

    public void AddTurn()
    {
        TurnsCount.Value++;
    }

    public void ResetStatistics()
    {
        TurnsCount.Value = 0;
        RoundTime.Value = TimeSpan.Zero;
    }

    private async Task TimerUpdateTask(CancellationToken token)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), token);
            RoundTime.Value += TimeSpan.FromSeconds(1);
        }
    }
}
