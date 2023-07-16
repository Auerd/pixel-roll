using UnityEngine;

namespace Game.Frame
{
    public class MoveChildren : MonoBehaviour
    {
        private void Update()
        {
        }

        private void FixedUpdate()
        {
            foreach (Transform child in transform)
            {
                child.transform.position += GameManagement.GameManager.Speed*Time.fixedDeltaTime;
            }
        }
    }
}