using UnityEngine;

[CreateAssetMenu(menuName = "AnswerStatus", fileName ="AnswerStatus")]
public class AnswerStatusData : ScriptableObject
{
    [field: SerializeField] public Sprite CorrectAnswerSprite { get; private set; }
    [field: SerializeField] public Sprite WrongAnswerSprite { get; private set; }

    [field: SerializeField] public Color CorrectAnswerColor { get; private set; }
    [field: SerializeField] public Color WrongAnswerColor { get; private set; }

    public Sprite GetSprite(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
            return CorrectAnswerSprite;

        return WrongAnswerSprite;
    }

    public Color GetColor(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
            return CorrectAnswerColor;

        return WrongAnswerColor;
    }
}