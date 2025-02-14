using Zenject;

public class ScoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindScore();
    }

    private void BindScore()
    {
        Container.BindInterfacesTo<Score>().AsSingle();
    }
}