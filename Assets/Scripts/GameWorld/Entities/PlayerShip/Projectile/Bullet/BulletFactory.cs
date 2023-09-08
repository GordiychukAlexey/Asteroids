using Core.GameWorld.Entities.Projectile;
using Core.GameWorld.Entities.Projectile.Bullet;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip.Projectile.Bullet {
	public class BulletFactory : BaseProjectileFactory<BulletController, Bullet, BulletFactoryArgs>, IBulletFactory {
		private readonly BulletConfig config;

		public BulletFactory(IObjectPool<Bullet> viewPool, BulletConfig config) : base(viewPool){
			this.config = config;
		}

		protected override IProjectileController CreateController(Bullet viewInstance, IProjectileFactoryArgs args){
			BulletFactoryArgs argsCasted = (BulletFactoryArgs) args;
			var x = new BulletController(viewInstance, args.WorldSide, argsCasted.Owner, config);
			x.AddSpeed(argsCasted.OwnerMovingSpeed);
			return x;
		}
	}
}