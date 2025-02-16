using UnityEngine;
using System;

[CreateAssetMenu(menuName = "AnswerStatus", fileName ="AnswerStatus")]
public class AnswerStatusData : ScriptableObject
{
    [field: SerializeField] public Sprite CorrectAnswerSprite { get; private set; }
    [field: SerializeField] public Sprite WrongAnswerSprite { get; private set; }

    [field: SerializeField] public Color CorrectAnswerColor { get; private set; }
    [field: SerializeField] public Color WrongAnswerColor { get; private set; }

    public Sprite GetSprite(AnswerStatus answerStatus)
    {
        switch (answerStatus)
        {
            case AnswerStatus.Correct:
                return CorrectAnswerSprite;
            case AnswerStatus.Wrong:
                return WrongAnswerSprite;
            default:
                throw new ArgumentOutOfRangeException(nameof(answerStatus));
        };
    }

    public Color GetColor(AnswerStatus answerStatus)
    {
        switch (answerStatus)
        {
            case AnswerStatus.Correct:
                return CorrectAnswerColor;
            case AnswerStatus.Wrong:
                return WrongAnswerColor;
            default:
                throw new ArgumentOutOfRangeException(nameof(answerStatus));
        };
    }
}