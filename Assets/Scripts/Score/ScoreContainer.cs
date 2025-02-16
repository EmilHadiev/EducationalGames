using System;

public class ScoreContainer : IScore
{
    public int CurrentPoints { get; private set; }

    public event Action<int> Changed;

    public void Add()
    {
        CurrentPoints += Constants.Point;
        Changed?.Invoke(CurrentPoints);
    }

    public void Reset()
    {
        CurrentPoints = 0;
        Changed?.Invoke(CurrentPoints);
    }
}