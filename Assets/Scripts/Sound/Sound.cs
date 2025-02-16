using System;
using UnityEngine;

[Serializable]
public struct Sound
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField] public AnswerStatus AnswerStatus { get; private set; }
}
