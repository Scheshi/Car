using Assets.Scripts.Configs;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Features.Garage
{
    public interface IUpgradableCar
    {
        float Speed { get; }
        void Restore();
        void Upgrade(UpgradableType type, float incrementPoint);
    }

    public interface IUpgradableCarHandler
    {
        IUpgradableCar UpgradeCar(IUpgradableCar car);
    }
}