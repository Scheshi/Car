using System;
using UnityEngine;

namespace Assets.Scripts.Features.GenerateLevel
{
    public interface IGenerateLevelController
    {
        void Init();
    }

    public interface IGenerateLevelView
    {
        event Action OnComplete;
        
        int Height { get; }

        int StartPositionHeight { get; }
        int Width { get; }
        GameObject GroundPrefab { get; }

        int PercentFill { get; }

        GameObject DirtPrefab { get; }

        GameObject TrianglePrefab { get; }
    }
}