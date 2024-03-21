using System;

public static class GameEvents
{
    public static IGameEvent PlayerTurn { get; } = new SimpleEvent();
    public static IGameEvent Victory { get; } = new SimpleEvent();
    public static IGameEvent GameRestart { get; } = new SimpleEvent();


    public interface IGameEvent
    {
        void Subscribe(Action handler);
        void UnSubscribe(Action handler);
        void Raise();
    }

    private class SimpleEvent: IGameEvent
    {
        private event Action _event;
        public void Raise()
        {
            _event?.Invoke();
        }

        public void Subscribe(Action handler)
        {
            _event += handler;
        }

        public void UnSubscribe(Action handler)
        {
            _event -= handler;
        }
    }
}
