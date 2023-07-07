using UnityEngine;

namespace Game.Frame.Spikes 
{
	public sealed class SpikeGenerator : Generator
	{
		[SerializeField] GameObject spikes;
		[SerializeField] bool top;

		private new void Awake()
		{
			base.Awake();
		}
		private new void Start()
		{
			base.Start();
			Vector2 spikesCanvasSize = GetCanvasSizeOf(spikes.GetComponent<BoxCollider2D>());
			Vector2 newSpikesPosition;
			Quaternion newSpikesRotation = Quaternion.Euler(0, 0, top ? 180 : 0);

			for (float x = canvasRect.width / 2;
				 x < canvasRect.width;
				 x += spikesCanvasSize.x)
			{
				newSpikesPosition = new(top ? x + spikesCanvasSize.x : x, 0);
                Instantiate(spikes, transform).transform
					.SetLocalPositionAndRotation(newSpikesPosition, newSpikesRotation);
            }

			for (float x = canvasRect.width / 2;
				 x > -spikesCanvasSize.x;
				 x -= spikesCanvasSize.x)
			{
				newSpikesPosition = new(top ? x + spikesCanvasSize.x : x, 0);
				Instantiate(spikes, transform).transform
					.SetLocalPositionAndRotation(newSpikesPosition, newSpikesRotation);
			}
		}
	}
}