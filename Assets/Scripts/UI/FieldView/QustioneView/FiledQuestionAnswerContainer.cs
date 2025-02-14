using UnityEngine;

public class FiledQuestionAnswerContainer : MonoBehaviour
{
    [SerializeField] private QuestionView _questionView;
    [SerializeField] private AnswerView[] _answerViews;

    [SerializeField] private FieldViewContainer _container;

    private void OnEnable() => _container.Selected += OnSelected;

    private void OnDisable() => _container.Selected -= OnSelected;

    private void OnSelected(FieldData data)
    {
        _questionView.SetDescription(data.Description);

        int index = 0;
        foreach (var answer in data.Answers)
        {
            _answerViews[index].SetData(answer);
            index++;
        }
    }
}