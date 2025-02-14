using System;

public class Score : IScore
{
    public int CurrentPoints { get; private set; }

    public event Action<int> Changed;

    public void Add(int point)
    {
        IsValid(point);
        CurrentPoints += point;
        Changed?.Invoke(CurrentPoints);
    }

    public void Reset()
    {
        CurrentPoints = 0;
        Changed?.Invoke(CurrentPoints);
    }

    private bool IsValid(int point)
    {
        if (point < 0)
            throw new ArgumentOutOfRangeException(nameof(point));

        return true;
    }
}