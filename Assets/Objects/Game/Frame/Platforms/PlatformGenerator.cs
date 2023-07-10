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

		[System.Serializable] private struct SpaceBetweenPlatforms
		{
			[Range(0, 4)]
			public int min;
			[Range(0f, 1f)]
			public float max;
		}
		[SerializeField] private SpaceBetweenPlatforms spaceBetweenPlatforms;

		private readonly List<GameObject> platforms = new();
		private float maxY, minY, minX, maxX;
		private Vector2 platformCanvasSize, brickCanvasSize, ballCanvasSize;

		private new void Awake()
		{
			base.Awake();
			brickCanvasSize = GetCanvasSizeOf(brick.GetComponent<BoxCollider2D>());
			platformCanvasSize = GetCanvasSizeOf(platform.GetComponent<BoxCollider2D>());
			ballCanvasSize = GetCanvasSizeOf(ballInstance.GetComponent<CircleCollider2D>());
		}

		private new void Start()
		{
			base.Start();
			DefineRanges();
			CreateFirstPlatform();
        }

		private void Update()
		{
			CreateNewPlatforms();
			RemoveOldPlatforms();
		}

		private void CreateNewPlatforms()
		{
			Vector2 lastPlatformPos = platforms[^1].transform.localPosition;

            if (lastPlatformPos.y > 0)
            {
				Vector2 newPlatformPos = new()
				{
					y = lastPlatformPos.y - Random.Range(minY, maxY)
				};

                for (int i = 0; i < maxPlatformsOnLevel; i++)
				{
					newPlatformPos.x = Random.Range(minX, maxX);
					GameObject newPlatform = CreatePlatform(newPlatformPos);
                    platforms.Add(newPlatform);
				}	
			}
		}

		private void RemoveOldPlatforms()
		{
			GameObject lastBrick = platforms[0];
			if (lastBrick.transform.localPosition.y > canvasRect.height)
			{
				mainPool.Return(lastBrick);
				platforms.RemoveAt(0);
			}
		}

		private void DefineRanges()
		{
            minY =
                ballCanvasSize.y * (1 + spaceBetweenPlatforms.min)
                + platformCanvasSize.y * 1.1f;
            maxY = canvasRect.height * spaceBetweenPlatforms.max;
            if (maxY < minY)
                maxY = minY;

            minX = brickCanvasSize.x;
            maxX = canvasRect.width - platformCanvasSize.x - brickCanvasSize.x;
        }

		private void CreateFirstPlatform()
		{
			Vector2 firstPlatformPos = new
			(
				Random.Range(minX, maxX),
				(canvasRect.height - platformCanvasSize.y) / 2f
			);
			platforms.Add(CreatePlatform(firstPlatformPos));

			Vector2 ballPos = firstPlatformPos + new Vector2(platformCanvasSize.x / 2, platformCanvasSize.y);
            ballInstance.transform.parent = transform;
            ballInstance.transform.localPosition = ballPos;
		}

		private GameObject CreatePlatform(Vector2 position) =>
			mainPool.Get(platform, transform, position);
	}
}