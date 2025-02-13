using UnityEngine;

public class FlashCard : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();
}