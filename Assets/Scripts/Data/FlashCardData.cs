using UnityEngine;

[CreateAssetMenu(fileName = "FlashCardData", menuName = "Level/FlashCardData")]
public class FlashCardData : FieldData
{
    [field: SerializeField] public string AnswerDescription { get; private set; }
}