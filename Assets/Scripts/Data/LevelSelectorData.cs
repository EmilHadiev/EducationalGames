using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "level/LevelData")]
public class LevelSelectorData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField, TextArea(2, 2)] public string Description { get; private set; }
}