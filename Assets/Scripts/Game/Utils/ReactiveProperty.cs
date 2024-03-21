using System;

public class ReactiveProperty<T>
{
    public event Action<T> ValueChanged;

    private T _value;

    public T Value {
        get => _value;
        set
        {
            _value = value;
            ValueChanged?.Invoke(_value);
        }
    }

    public ReactiveProperty() : this(default(T))
    {
    }

    public ReactiveProperty(T initialValue)
    {
        Value = initialValue;
    }

    public void Subscribe(Action<T> action)
    {
        ValueChanged += action;
    }

    public void Unsubscribe(Action<T> action)
    {
        ValueChanged -= action;
    }
}
