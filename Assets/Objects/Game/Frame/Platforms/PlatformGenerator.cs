using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace Game.Frame.Platforms
{
	public sealed class PlatformGenerator : Generator
	{
		[SerializeField]
		private GameObject platform, spikes, ball, brick;
		[SerializeField]
		[Range(0, 4)]
		private int min;
		[SerializeField]
		[Range (-4, 1)]
		private int max;

		private readonly List<GameObject> platforms = new();
		private float maxHeightBetweenPlatforms, minHeightBetweenPlatforms;
		private Vector2 platformCanvasSize, brickCanvasSize, ballCanvasSize;

		private new void Awake()
		{
			base.Awake();
			brickCanvasSize = GetCanvasSizeOf(brick.GetComponent<BoxCollider2D>());
			platformCanvasSize = GetCanvasSizeOf(platform.GetComponent<BoxCollider2D>());
			ballCanvasSize = GetCanvasSizeOf(ball.GetComponent<CircleCollider2D>());
		}

		private new void Start()
		{
			base.Start();
			CreateFirstPlatform();

            minHeightBetweenPlatforms =
                ballCanvasSize.y * (1 + min)
                + platformCanvasSize.y * 1.1f;

            maxHeightBetweenPlatforms =
                canvasRect.height
                + ballCanvasSize.y * (max - 1);
        }

		private void Update()
		{
			CreateNewPlatform();
			RemoveOldPlatform();
		}

		private void CreateNewPlatform()
		{
			Vector2 lastPlatformPos = platforms[^1].transform.localPosition;
			Vector2 newPlatformPos = new
			(
				RandomPlatformPointX,
				lastPlatformPos.y - Random.Range(minHeightBetweenPlatforms, maxHeightBetweenPlatforms)
			);

            if (lastPlatformPos.y > 0)
				platforms.Add(CreatePlatform(newPlatformPos));
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
			Instantiate(ball, transform).transform.localPosition = ballPos;
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