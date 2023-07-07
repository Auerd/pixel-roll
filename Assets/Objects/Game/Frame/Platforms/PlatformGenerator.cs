using System.Collections.Generic;
using UnityEngine;

namespace Game.Frame.Platforms
{
	public sealed class PlatformGenerator : Generator
	{
		[SerializeField]
		private GameObject platform, ballInstance, brick;

		[SerializeField]
		[Range(1, 4)]
		private uint maxPlatformsOnLevel;

		[System.Serializable]
		private struct SpaceBetweenPlatforms
		{
            [Range(0, 4)]
            public int min;
            [Range(0f, 1f)]
            public float max;
        }
        [SerializeField] private SpaceBetweenPlatforms spaceBetweenPlatforms;

        private readonly List<GameObject> platforms = new();
		private float maxSpaceBetweenPlatforms, minSpaceBetweenPlatforms;
		private Vector2 platformCanvasSize, brickCanvasSize, ballCanvasSize;
		private float[,] occupedPos;

		private new void Awake()
		{
			base.Awake();
			occupedPos = new float[maxPlatformsOnLevel, 2];
			brickCanvasSize = GetCanvasSizeOf(brick.GetComponent<BoxCollider2D>());
			platformCanvasSize = GetCanvasSizeOf(platform.GetComponent<BoxCollider2D>());
			ballCanvasSize = GetCanvasSizeOf(ballInstance.GetComponent<CircleCollider2D>());
		}

		private new void Start()
		{
			base.Start();
			ballInstance.transform.parent = transform;
			CreateFirstPlatform();

            minSpaceBetweenPlatforms =
                ballCanvasSize.y * (1 + spaceBetweenPlatforms.min)
                + platformCanvasSize.y * 1.1f;

			maxSpaceBetweenPlatforms =
				canvasRect.height * (spaceBetweenPlatforms.max);

			if (maxSpaceBetweenPlatforms <  minSpaceBetweenPlatforms)
				maxSpaceBetweenPlatforms = minSpaceBetweenPlatforms;

        }

		private void Update()
		{
			CreateNewPlatform();
			RemoveOldPlatform();
		}

		private void CreateNewPlatform()
		{
			Vector2 lastPlatformPos = platforms[^1].transform.localPosition;
            Vector2 newPlatformPos = new()
            {
                y = lastPlatformPos.y - Random.Range(minSpaceBetweenPlatforms, maxSpaceBetweenPlatforms)
            };


			for (int i = 0; i < maxPlatformsOnLevel; i++)
			{
				newPlatformPos.x = RandomPlatformPointX;
				if (lastPlatformPos.y > 0) 
				{
					platforms.Add(CreatePlatform(newPlatformPos));
					occupedPos[i, 0] = newPlatformPos.x;
					occupedPos[i, 1] = newPlatformPos.x + platformCanvasSize.x;
                }	
			}
		}

		private void RemoveOldPlatform()
		{
			GameObject lastBrick = platforms[0];
			if (lastBrick.transform.localPosition.y > canvasRect.height)
			{
				mainPool.Return(lastBrick);
				platforms.RemoveAt(0);
			}
		}

		private void CreateFirstPlatform()
		{
			Vector2 firstPlatformPos = new
			(
				RandomPlatformPointX,
				(canvasRect.height - platformCanvasSize.y) / 2f
			);
			platforms.Add(CreatePlatform(firstPlatformPos));

			Vector2 ballPos = firstPlatformPos + new Vector2(platformCanvasSize.x / 2, platformCanvasSize.y);
			ballInstance.transform.localPosition = ballPos;
		}

		private GameObject CreatePlatform(Vector2 position) =>
			mainPool.Get(platform, transform, position);

		private float RandomPlatformPointX 
		{
			get => Random.Range
				(brickCanvasSize.x, canvasRect.width - platformCanvasSize.x - brickCanvasSize.x);
		}
	}
}