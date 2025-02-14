using TMPro;
using UnityEngine;

public class QuestionView : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;

    public void SetDescription(string description) => _descriptionText.text = description;
}
