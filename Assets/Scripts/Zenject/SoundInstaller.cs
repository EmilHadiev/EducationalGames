using UnityEngine;
using Zenject;

public class SoundInstaller : MonoInstaller
{
    [SerializeField] private SoundContainer _soundContainer;

    public override void InstallBindings()
    {
        BindSoundContainer();
    }

    private void BindSoundContainer()
    {
        Container.Bind<SoundContainer>().FromComponentInNewPrefab(_soundContainer).AsSingle();
    }
}