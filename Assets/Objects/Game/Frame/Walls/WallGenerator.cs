using UnityEngine;
using System.Collections.Generic;

namespace Game.Frame.Walls
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
            wall.Add(CreateBrick(Vector2.zero));
        }

        private new void Start() 
		{
            base.Start();
            canvasBrickSize = GetCanvasSizeOf(wall[^1].GetComponent<BoxCollider2D>());
            if (right)
                wall[^1].transform.localPosition -= new Vector3(canvasBrickSize.x, 0); // Moves to left
        }

        private void Update() 
		{
            CreateNewBrick();
            RemoveOldBrick();
        }

		private void CreateNewBrick()
		{
            Vector2 newBrickPosition = new
            (
                0, wall[^1].transform.localPosition.y - canvasBrickSize.y
            );

            if (right)
                newBrickPosition.x -= canvasBrickSize.x;

            if (-newBrickPosition.y<canvasRect.height)
                wall.Add(CreateBrick(newBrickPosition));
        }

        private void RemoveOldBrick()
        {
            GameObject lastBrick = wall[0];
            if (lastBrick.transform.localPosition.y > canvasBrickSize.y)
            {
                RemoveBrick(lastBrick);
                wall.RemoveAt(0);
            }
        }

        private GameObject CreateBrick(in Vector2 newBrickPosition)=>
			mainPool.Get(brick, transform, newBrickPosition, Quaternion.identity);

        private void RemoveBrick(GameObject brick) =>
            mainPool.Return(brick);
        
	}
}