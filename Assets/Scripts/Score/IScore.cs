using System;

public interface IScore
{
    public int CurrentPoints { get; }

    event Action<int> Changed;

    void Add(int point);
    void Reset();
}
