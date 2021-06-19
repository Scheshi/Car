using UnityEngine;

namespace Assets.Scripts.Configs
{
    public abstract class Item : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite Sprite;
    }
}