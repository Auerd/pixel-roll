using UnityEngine;
using Walls;

namespace Game
{
    public class FrameGenerator : MonoBehaviour
    {
        [SerializeField]
        private WallsGenerator wallsGenerator;
        void Start()
        {

        }

        void Update()
        {
            WallsGenerator.Instance().UpdateWalls();
        }
    }
}