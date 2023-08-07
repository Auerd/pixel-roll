using Game.Ball;
using Game.GameManagement;
using System.Collections.Generic;
using UnityEngine;
using static Game.GameManagement.GameManager;

namespace Game.Frame.Platforms
{
	public sealed class PlatformsGenerator : Generator
	{
		#region Input

		[SerializeField]
		private GameObject platform, ballInstance, brick = null;

        [System.Serializable] private struct HeightBetweenPlatforms
		{
			[Range(1, 4)]
			public int min;
			[Range(0f, 1f)]
			public float max;
		}
		[SerializeField] private HeightBetweenPlatforms heightBetweenPlatforms;

        #endregion
        #region Private variables

        private readonly List<GameObject> platforms = new();
		private float maxY, minY, minX, maxX;
		private Vector2 platformCanvasSize, brickCanvasSize, ballCanvasSize;
		private GameObject reservedPlatform;

        #endregion

		public static bool NeedToCreateBall { private get; set; }

		public void RespawnBall()
		{
			NeedToCreateBall = true;
		}

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
			CulcVariables();
			CreateFirstPlatform();
        }

		private void Update()
		{
			CreateNewPlatforms();
			RemoveOldPlatforms();
		}

		private void CulcVariables()
		{
            minY =
                ballCanvasSize.y * heightBetweenPlatforms.min
                + platformCanvasSize.y * 1.1f;
            maxY = canvasRect.height * heightBetweenPlatforms.max;
            if (maxY < minY)
                maxY = minY;

            minX = brickCanvasSize.x;
            maxX = canvasRect.width - platformCanvasSize.x - brickCanvasSize.x;
        }

		private void CreateNewPlatforms()
		{
			Vector2 lastPlatformPos = platforms[^1].transform.localPosition;

			if (lastPlatformPos.y > 0)
			{
				Vector2 newPlatformPos = new
				(
					Random.Range(brickCanvasSize.x, canvasRect.width - brickCanvasSize.x - platformCanvasSize.x),
					lastPlatformPos.y - Random.Range(minY, maxY)
				);

				platforms.Add(CreatePlatform(newPlatformPos));


				if (NeedToCreateBall)
				{
					SetBallOnPlatformWithPos(lastPlatformPos);
					ballInstance.SetActive(true);
					NeedToCreateBall = false;
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

            ballInstance.transform.parent = transform;
			SetBallOnPlatformWithPos(firstPlatformPos);
		}

		private GameObject CreatePlatform(Vector2 position) =>
			mainPool.Get(platform, transform, position);

		private void SetBallOnPlatformWithPos(Vector2 pos) =>
            ballInstance.transform.localPosition = 
			pos + new Vector2(platformCanvasSize.x / 2, platformCanvasSize.y);
    }
}