public interface IStateSwitcher
{
    void Switch<T>() where T : ILevelSelectorState;
}