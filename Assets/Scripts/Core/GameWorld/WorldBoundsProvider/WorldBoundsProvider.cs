using Core.Tools.Extensions;
using UnityEngine;

namespace Core.GameWorld.WorldBoundsProvider {
	public class WorldBoundsProvider: IWorldBoundsProvider{
		public Bounds Bounds{ get; }

		public WorldBoundsProvider(Camera mainCamera){
			var x = mainCamera.OrthographicBounds();
			Bounds = new Bounds((Vector2) x.center, (Vector2) x.size);
		}
	}
}