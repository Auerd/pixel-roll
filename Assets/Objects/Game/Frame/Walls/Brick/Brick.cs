using UnityEngine;

namespace Walls
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Brick : MonoBehaviour
	{
		public BoxCollider2D Collider { get; private set; }
		private void Awake()
		{
			Collider = GetComponent<BoxCollider2D>();
		}
	}
}