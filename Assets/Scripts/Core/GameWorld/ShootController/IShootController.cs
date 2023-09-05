using System;
using Core.GameSycle;
using Core.GameWorld.Entities.Projectile;

namespace Core.GameWorld.ShootController {
	public interface IShootController : IUpdatable, IDisposable {
		public event EventHandler<IProjectileController[]> OnShoot;
		public bool IsSpawnActive{ get; set; }
	}
}