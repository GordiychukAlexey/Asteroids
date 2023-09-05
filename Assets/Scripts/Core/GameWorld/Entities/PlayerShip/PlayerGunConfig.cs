using System;
using Core.GameWorld.Entities.Projectile;

namespace Core.GameWorld.Entities.PlayerShip {
	public class PlayerGunConfig {
		public IProjectileFactory projectileFactory{ get; }
		public Func<IWorldObjectController, IProjectileFactoryArgs> projectileFactoryArgs{ get; }

		public PlayerGunConfig(IProjectileFactory projectileFactory, Func<IWorldObjectController, IProjectileFactoryArgs> projectileFactoryArgs){
			this.projectileFactory = projectileFactory;
			this.projectileFactoryArgs = projectileFactoryArgs;
		}
	}
}