using UnityEngine;

namespace Assets.Scripts.Configs
{
    public abstract class Ability : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite Sprite;

        public abstract void Apply(Vector2 carTransform);
    }
}