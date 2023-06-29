using System.Collections.Generic;
using UnityEngine;

namespace Game.Platforms
{
    public sealed class PlatformGenerator : Generator
    {
        [SerializeField] private GameObject platform;

        private new void Start()
        {
            base.Start();
        }
    }
}