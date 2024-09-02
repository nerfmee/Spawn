namespace Game.Scripts.SpawnBetterPerformance.Scripts.Services
{
    public interface IScope <T>
    {
        TP Register<TP>(TP newService) where TP : T;
        void Unregister<TP>(TP service) where TP : T;
        TP Get<TP>() where TP : T;
    }
}