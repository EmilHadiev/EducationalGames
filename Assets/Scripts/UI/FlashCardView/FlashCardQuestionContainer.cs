using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ElementRotator))]
public class FlashCardQuestionContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _textDescription;
    [SerializeField] private TMP_Text _answerDescription;
    [SerializeField] private Image _statusImage;
    [SerializeField] private Button _flipButton;
    [SerializeField] private ElementRotator _rotator;
    [SerializeField] private FlashCardData[] _data;

    private bool _isOpen;
    private bool _isWork;

    private Sequence _showAnswer;
    private Sequence _showQuestion;

    private void OnValidate()
    {
        _rotator ??= GetComponent<ElementRotator>();
    }

    private void OnEnable()
    {
        _flipButton.onClick?.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _flipButton.onClick?.RemoveListener(OnClicked);
    }

    private void Start()
    {
        Show(_data[4]);

        _isWork = true;

        InitializeShowQuestionSequence();
        InitializeShowAnswerSequence();
    }

    private void InitializeShowAnswerSequence()
    {
        _showAnswer = DOTween.Sequence();
        _showAnswer.Pause();
        _showAnswer.Append(_answerDescription.DOFade(0, Constants.DefaultDuration));
        _showAnswer.Append(_textDescription.DOFade(1, Constants.DefaultDuration));
        _showAnswer.OnComplete(() => WorkToggle(true));
        _showAnswer.SetAutoKill(false);
    }

    private void InitializeShowQuestionSequence()
    {
        _showQuestion = DOTween.Sequence();
        _showQuestion.Pause();
        _showQuestion.Append(_textDescription.DOFade(0, Constants.DefaultDuration).OnComplete(() => _rotator.ResetRotate()));
        _showQuestion.Append(_answerDescription.DOFade(1, Constants.DefaultDuration));
        _showQuestion.OnComplete(() => WorkToggle(true));
        _showQuestion.SetAutoKill(false);
    }

    private void WorkToggle(bool isWork)
    {
        _isWork = isWork;
        _flipButton.interactable = isWork;
    }

    private void Show(FlashCardData flashCard)
    {
        _textDescription.text = flashCard.Description;
        _answerDescription.text = flashCard.AnswerDescription;
    }

    private void OnClicked()
    {
        if (_isOpen == false)
        {
            _rotator.Rotate();
            _showQuestion.Restart();
            _isOpen = true;
        }
        else
        {
            _rotator.ResetRotate();
            _showAnswer.Restart();
            _isOpen = false;
        }

        WorkToggle(false);
    }
}