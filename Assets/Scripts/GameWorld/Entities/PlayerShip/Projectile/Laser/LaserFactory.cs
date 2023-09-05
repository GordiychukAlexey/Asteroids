using Core.GameWorld.Entities.Bullet;
using Core.GameWorld.Entities.Bullet.PlayerBullet;
using Core.GameWorld.Entities.Laser;
using Core.GameWorld.Entities.Projectile;
using Core.GameWorld.Entities.Projectile.Bullet;
using GameWorld.Entities.PlayerShip.Projectile.Laser;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip {
//	public class LaserProjectileFactory   : BaseBulletFactory<SimpleBulletController, PlayerBullet, PlayerBulletFactoryArgs> {
//		private readonly PlayerBulletConfig config;
//
//		public SimpleBulletFactory(IObjectPool<PlayerBullet> viewPool, PlayerBulletConfig config) : base(viewPool){
//			this.config = config;
//		}
//
//		protected override IBulletController CreateController(PlayerBullet viewInstance, PlayerBulletFactoryArgs args) =>
//			new SimpleBulletController(viewInstance, config);
//	}


	public class LaserFactory : BaseProjectileFactory<LaserController, Laser, LaserFactoryArgs>,ILaserFactory {
		private readonly LaserConfig config;

		public LaserFactory(IObjectPool<Laser> viewPool, LaserConfig config) : base(viewPool){
			this.config = config;
		}
		
//		public LaserController Create(ILaserFactoryArgs args){
		public IProjectileController Create(ILaserFactoryArgs args){
			var controller = base.Create((LaserFactoryArgs) args);
//			controller.AddSpeed(args.OwnerMovingSpeed);
//			controller.SetAngularSpeed(args.OwnerAngularSpeed);

			return controller;
		}
		
//		protected override LaserController CreateController(Laser viewInstance, LaserFactoryArgs args) =>
		protected override IProjectileController CreateController(Laser viewInstance, IProjectileFactoryArgs args){
			LaserFactoryArgs argsCasted = (LaserFactoryArgs) args;
			return new LaserController(viewInstance,args.WorldSide, config, argsCasted.Owner);
		}
	}
}