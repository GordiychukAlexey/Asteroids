using Core.GameWorld.Entities.Projectile;
using Core.GameWorld.Entities.Projectile.Bullet;
using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip.Projectile.Bullet {
	public class BulletFactory : BaseProjectileFactory<BulletController, Bullet, BulletFactoryArgs>, IBulletFactory {
		private readonly BulletConfig config;
		private readonly IWorldBoundsProvider worldBoundsProvider;
		private readonly IInfinityWorld infinityWorld;

		public BulletFactory(
			IObjectPool<Bullet> viewPool,
			BulletConfig config,
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld) : base(viewPool){
			this.config = config;
			this.worldBoundsProvider = worldBoundsProvider;
			this.infinityWorld = infinityWorld;
		}

		protected override IProjectileController CreateController(Bullet viewInstance, IProjectileFactoryArgs args){
			BulletFactoryArgs argsCasted = (BulletFactoryArgs) args;
			var x = new BulletController(viewInstance, args.WorldSide, argsCasted.Owner, config, worldBoundsProvider, infinityWorld);
			x.AddSpeed(argsCasted.OwnerMovingSpeed);
			return x;
		}
	}
}