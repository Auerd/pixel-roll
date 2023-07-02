using UnityEngine;

namespace Game.Frame
{
    public class MoveChildren : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 speed;

        private void Update()
        {
            speed = GameManager.Speed;
        }

        private void FixedUpdate()
        {
            foreach (Transform child in transform)
            {
                child.transform.position += speed*Time.fixedDeltaTime;
            }
        }
    }
}