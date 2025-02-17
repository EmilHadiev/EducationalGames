using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelSelectorData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, TextArea(3, 5)] public string Description { get; private set; }
    [field: SerializeField] public LevelSelectorType LevelType { get; private set; }
}