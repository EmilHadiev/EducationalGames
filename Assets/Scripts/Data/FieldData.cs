using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FieldData", menuName = "Level/FieldData")]
public class FieldData : ScriptableObject
{
    [field: SerializeField, TextArea(3, 5)] public string Description { get; private set; }
    [SerializeField] private AnswerOption[] _answerOptions;

    public IReadOnlyCollection<AnswerOption> Answers => _answerOptions;

    private void OnValidate()
    {
        int correctAnswers = 0;

        foreach (var answer in _answerOptions)
        {
            if (answer.IsCorrect)
                correctAnswers += 1;
        }

        if (correctAnswers > Constants.MaxFieldCorrectAnswers)
            throw new ArgumentOutOfRangeException(nameof(correctAnswers));
    }
}