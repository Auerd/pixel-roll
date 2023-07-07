using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameEvent")]
    public class Event : ScriptableObject
    {
        public delegate void OnEvent();
        private event OnEvent CurrEvent;
        public void Raise()=>
            CurrEvent.Invoke();

        public void Subscribe(OnEvent method)=>
            CurrEvent += method;
    }
}