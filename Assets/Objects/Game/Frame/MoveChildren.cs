using System.Collections;
using UnityEngine;

namespace Game
{
    public class MoveChildren : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;

        private void FixedUpdate()
        {
            foreach (Transform child in transform)
            {
                child.transform.position += direction*Time.fixedDeltaTime;
            }
        }
    }
}