using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameViewContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _topicText;
    [SerializeField] private TMP_Text _descriptionText;

    public void SetData(LevelSelectorData data)
    {
        _nameText.text = data.Name;
        _topicText.text = Constants.TopicSubject;
        _descriptionText.text = data.Description;
    }
}