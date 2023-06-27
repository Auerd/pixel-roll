using UnityEngine;

namespace Walls
{
	public class Brick : MonoBehaviour
	{
		public bool InWindow(GameWindow.Window display)
		{
			return display.DotInWindow(transform.position);
		}
	}
}