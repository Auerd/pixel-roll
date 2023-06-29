using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Debug;

namespace Game.Walls
{
	public sealed class WallGenerator : Generator
	{
		[SerializeField] private GameObject brick;
		[SerializeField] private bool right;
        private readonly List<GameObject> wall = new();
        private Vector3 canvasBrickSize;

        private new void Awake()
        {
			base.Awake();
			CreateBrick(Vector2.zero);
        }

        private new void Start() 
		{
			base.Start();
            canvasBrickSize = wall[^1].GetComponent<BoxCollider2D>().size * wall[^1].transform.localScale;
            if (right)
                wall[^1].transform.localPosition -= new Vector3(canvasBrickSize.y, 0, 0); // Moves to left
        }

        private void Update() 
		{
			GameObject lastBrick = wall[0];
            Vector2 newBrickPosition = new
			(
				0, wall[^1].transform.localPosition.y - canvasBrickSize.x
			);

			if (right)
				newBrickPosition.x -= canvasBrickSize.y;

			if (IsDotInCanvas(-newBrickPosition))
				CreateBrick(newBrickPosition);
			if (0 < lastBrick.transform.localPosition.y-canvasBrickSize.x)
			{
                RemoveBrick(lastBrick);
                wall.RemoveAt(0);
			}
		}

		private void CreateBrick(in Vector2 newBrickPosition)=>
			wall.Add(mainPool.Get(brick, transform, newBrickPosition, Quaternion.Euler(0, 0, -90)));
		
		private void RemoveBrick(GameObject brick)=>
			mainPool.Return(brick);
	}
}