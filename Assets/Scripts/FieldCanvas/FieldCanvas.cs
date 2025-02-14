using UnityEngine;

public class FieldCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();
}