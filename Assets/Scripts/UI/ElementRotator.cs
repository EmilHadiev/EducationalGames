using DG.Tweening;
using UnityEngine;

public class ElementRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationPosition = new Vector3(0, 180, 0);

    private readonly Vector3 _defaultRotation = new Vector3(0, 0, 0);

    private Tween _rotate;
    private Tween _reset;

    private void Start()
    {
        InitializeRotator();
        InitializeReseter();
    }

    private void InitializeReseter()
    {
        _reset = transform.DORotate(_defaultRotation, 0).SetEase(Ease.Linear).SetAutoKill(false);
        _reset.Pause();
    }

    private void InitializeRotator()
    {
        _rotate = transform.DORotate(_rotationPosition, Constants.DefaultDuration).SetEase(Ease.Linear).SetAutoKill(false);
        _rotate.Pause();
    }

    public void Rotate() => _rotate.Restart();

    public void ResetRotate() => _reset.Restart();
}
