using UnityEngine;
using Zenject;

public class GamePanelInstaller : MonoInstaller
{
    [SerializeField] private GamePanelMediator _mediator;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<GamePanelMediator>().FromInstance(_mediator);
    }
}