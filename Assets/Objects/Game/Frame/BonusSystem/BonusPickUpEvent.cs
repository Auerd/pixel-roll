using UnityEngine;

namespace Game.Frame.BonusSystem
{
    public class BonusPickUpEvent : ScriptableObject
    {
        public delegate void OnBonusPickUp(Bonus bonus);
        private event OnBonusPickUp @Event;

        public void Raise(Bonus bonus)=>
            Event.Invoke(bonus);

        public void Subscribe(OnBonusPickUp method)=>
            Event += method;
    }
}