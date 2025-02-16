using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _answerStatusImage;
    [SerializeField] private AnswerStatusData _statusData;

    private readonly Color _defaultColor = new Color(0, 0, 0, 0);

    private void OnValidate()
    {
        _descriptionText = GetComponentInChildren<TMP_Text>();
    }

    public void SetDescription(string description)
    {
        _descriptionText.text = description;
    }

    public void ShowResult(bool isCorrectAnswer)
    {
        _answerStatusImage.color = _statusData.GetColor(isCorrectAnswer);
        _answerStatusImage.sprite = _statusData.GetSprite(isCorrectAnswer);
    }

    public void ResetResultView()
    {
        _answerStatusImage.sprite = null;
        _answerStatusImage.color = _defaultColor;
    }
}