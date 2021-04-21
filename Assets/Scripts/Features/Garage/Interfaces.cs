namespace Assets.Scripts.Features.Garage
{
    public interface IUpgradableCar
    {
        float Speed { get; }
        void Restore();
    }

    public interface IUpgradableCarHandler
    {
        IUpgradableCar UpgradeCar(IUpgradableCar car);
    }
}