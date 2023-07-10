using System.Collections.Generic;
using UnityEngine;

namespace Game.Frame.Platforms
{
	public sealed class PlatformsGenerator : Generator
	{
        [SerializeField]
		private GameObject platform, ballInstance, brick;

		[System.Serializable] private struct LevelSettings
		{
			[Min(1)]
			public uint maxPlatformsOnLevel;

			[Tooltip("If adaptive is turned on, MaxPlatformsOnLevel value will be calculated automaticly")]
			public bool adaptive;

			[Range(0f, 1.5f)]
			[Tooltip("Maximum Y offset from the main platform")]
			public float maxRange;
			[Range(0f, 1.5f)]
			public float minSpaceBetweenPlatforms;
		}
		[SerializeField] private LevelSettings levelSettings;

        [System.Serializable] private struct HeightBetweenPlatforms
		{
			[Range(1, 4)]
			public int min;
			[Range(0f, 1f)]
			public float max;
		}
		[SerializeField] private HeightBetweenPlatforms heightBetweenPlatforms;

		private readonly List<GameObject> platforms = new();
		private float maxY, minY, minX, maxX;
		private uint maxPlatformsOnLevel;
		private float maxOffsetRange, minSpaceBetweenPlatforms;
		private Vector2 platformCanvasSize, brickCanvasSize, ballCanvasSize;


		private new void Awake()
		{
			base.Awake();
			brickCanvasSize = GetCanvasSizeOf(brick.GetComponent<BoxCollider2D>());
			platformCanvasSize = GetCanvasSizeOf(platform.GetComponent<BoxCollider2D>());
			ballCanvasSize = GetCanvasSizeOf(ballInstance.GetComponent<CircleCollider2D>());
		}

		private void DefineAllVariables()
		{
            minY =
                ballCanvasSize.y * heightBetweenPlatforms.min
                + platformCanvasSize.y * 1.1f;
            maxY = canvasRect.height * heightBetweenPlatforms.max;
            if (maxY < minY)
                maxY = minY;

            minX = brickCanvasSize.x;
            maxX = canvasRect.width - platformCanvasSize.x - brickCanvasSize.x;

			maxOffsetRange = platformCanvasSize.y * levelSettings.maxRange;
			if (levelSettings.adaptive)
				maxPlatformsOnLevel = (uint)Mathf.Floor(canvasRect.width / 2 / platformCanvasSize.x);
			else
				maxPlatformsOnLevel = levelSettings.maxPlatformsOnLevel;
			minSpaceBetweenPlatforms = levelSettings.minSpaceBetweenPlatforms * ballCanvasSize.x;
        }

		private new void Start()
		{
			base.Start();
			DefineAllVariables();
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

				var platformsOnLevel = new float[maxPlatformsOnLevel];

				for (int i = 0; i < maxPlatformsOnLevel; i++)
				{
					newPlatformPos.x = Random.Range(minX, maxX);

					foreach (var platformPosX in platformsOnLevel)
					{
						float leftCornerNewPlat = newPlatformPos.x;
						float rightCornerNewPlat = newPlatformPos.x + platformCanvasSize.x;
						float leftCornerCurrPlat = platformPosX;
						float rightCornerCurrPlat = platformPosX + platformCanvasSize.x;

                        bool doPlatformsIntersect =
                               leftCornerCurrPlat >= leftCornerNewPlat - minSpaceBetweenPlatforms
							&& leftCornerCurrPlat <= rightCornerNewPlat + minSpaceBetweenPlatforms

							|| rightCornerCurrPlat >= leftCornerNewPlat - minSpaceBetweenPlatforms
                            && rightCornerCurrPlat <= rightCornerNewPlat + minSpaceBetweenPlatforms;

						if (doPlatformsIntersect)
							return;
					}

					if(i != 0)
						newPlatformPos.y += Random.Range(-maxOffsetRange, maxOffsetRange);

					platforms.Add(CreatePlatform(newPlatformPos));
					platformsOnLevel[i] = newPlatformPos.x;
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