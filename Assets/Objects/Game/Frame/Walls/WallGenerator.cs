using UnityEngine;
using System.Collections.Generic;

namespace Walls
{
	[System.Serializable]
	public sealed class WallGenerator : MonoBehaviour
	{
		[SerializeField] private Brick brick;
		[SerializeField] private Canvas canvas;
		[SerializeField] private bool right;
		private Rect canvasRectTransform;
		private	readonly List<Brick> wall = new();
		private Vector2 canvasBrickSize;

        private void Awake()
        {
			wall.Add(CreateBrick(Vector2.zero));
        }

        private void Start () 
		{
            canvasBrickSize = wall[^1].Collider.size * wall[^1].transform.localScale;
            canvasRectTransform = canvas.GetComponent<RectTransform>().rect;
            if (right)
                wall[^1].transform.localPosition -= new Vector3(canvasBrickSize.y, 0, 0);
        }

        private void Update() 
		{
            Vector2 newBrickPosition = new
			(
				0, wall[^1].transform.localPosition.y - canvasBrickSize.x
			);

			if (right)
				newBrickPosition.x -= canvasBrickSize.y;

			if (IsDotInCanvas(-newBrickPosition))
				wall.Add(CreateBrick(newBrickPosition));
		}

		private Brick CreateBrick(Vector2 pos)
		{
			Brick newBrick = Instantiate(brick, transform);
			newBrick.transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 0, -90));
			return newBrick;
		}

		private void RemoveBrick(Brick brick)
		{
			Destroy(brick);
		}

		private bool RectInCanvas(Rect rect)
		{
			Vector2 rectPos = new(rect.x, rect.y);
			Vector2 rectWidth = new(rect.width, 0);
			Vector2 rectHeight = new(0, rect.height);
			return IsLeastOneDotInCanvas(
				rectPos, 
				rectPos+rectWidth, 
				rectPos+rectHeight,
				rectPos+rectWidth+rectHeight
			);
		}

		private bool IsLeastOneDotInCanvas(params Vector2[] dots)
		{
			foreach (var dot in dots) 
				if (IsDotInCanvas(dot))
					return true;
			return false;
		}

		private bool IsDotInCanvas(Vector2 dot)
		{
			return dot.x >= 0
				&& dot.y >= 0
				&& dot.x <= canvasRectTransform.width
				&& dot.y <= canvasRectTransform.height;
		}
	}
}