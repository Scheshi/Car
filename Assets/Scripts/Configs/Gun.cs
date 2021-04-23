using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/Ability/Gun")]
    public class Gun : Ability
    {
        public GameObject Ammo;
        public Vector2 Force;
        
        public override void Apply(Vector2 position)
        {
            var ammo = Instantiate(Ammo, position, Quaternion.identity);
            if (ammo.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(Force);
            }
        }
    }
}