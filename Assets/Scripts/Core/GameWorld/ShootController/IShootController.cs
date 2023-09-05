using System;
using Core.GameWorld.Entities.Projectile;

namespace Core.GameWorld.ShootController {
	public interface IShootController : IDisposable {
		public event EventHandler<IProjectileController[]> OnShoot;
		public bool IsSpawnActive{ get; set; }
		public void Update(float dt);
	}
}