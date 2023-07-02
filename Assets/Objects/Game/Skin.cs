using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="Skin")]
    public class Skin : ScriptableObject
    {
        [SerializeField] Sprite ball, platform, brick, spikes;
    }
}