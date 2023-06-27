using UnityEngine;
using GameWindow;
using System.Collections.Generic;

namespace Walls
{
	[System.Serializable]
	public sealed class WallsGenerator
	{
		[SerializeField] private BoxCollider2D brick;
		[SerializeField] private Window gameWindow;
		private List<BoxCollider2D> wall;

        #region Singleton
        private static WallsGenerator instance;

        private WallsGenerator() =>
			wall = new List<BoxCollider2D> { brick };

		public static WallsGenerator Instance () 
		{
			instance ??= new WallsGenerator();
			return instance;
		}
        #endregion

        public void UpdateWalls() 
		{
			Vector2 newBrickPos = new(0, wall[^1].transform.position.y + brick.size.x);
			if (gameWindow.DotInWindow(newBrickPos))
				wall.Add(CreateBrick(newBrickPos));
		}

		private BoxCollider2D CreateBrick(Vector2 pos)
		{
			return Object.Instantiate
			(
				brick,
				pos,
				Quaternion.Euler(0, 0, 90)
			);
		}
	}
}