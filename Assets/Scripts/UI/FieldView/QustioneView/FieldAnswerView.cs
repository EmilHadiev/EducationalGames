using TMPro;
using UnityEngine;

public class FieldAnswerView : AnswerView
{
    [SerializeField] private TMP_Text _answerDescription;

    protected override void PrintData() => _answerDescription.text = Data.Answer;
}