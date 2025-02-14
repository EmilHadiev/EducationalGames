using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _fieldNumberText;

    private FieldData _fieldData;

    public void Initialize(string fieldNumber, FieldData fieldData)
    {
        _fieldNumberText.text = fieldNumber;
        _fieldData = fieldData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_fieldData.Description);
    }
}
