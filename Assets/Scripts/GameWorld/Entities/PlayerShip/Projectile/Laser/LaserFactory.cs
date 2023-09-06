using Core.GameWorld.Entities.Projectile;
using Core.GameWorld.Entities.Projectile.Laser;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip.Projectile.Laser {
	public class LaserFactory : BaseProjectileFactory<LaserController, Laser, LaserFactoryArgs>, ILaserFactory {
		private readonly LaserConfig config;

		public LaserFactory(IObjectPool<Laser> viewPool, LaserConfig config) : base(viewPool){
			this.config = config;
		}

		public IProjectileController Create(ILaserFactoryArgs args){
			var controller = base.Create((LaserFactoryArgs) args);

			return controller;
		}

		protected override IProjectileController CreateController(Laser viewInstance, IProjectileFactoryArgs args){
			LaserFactoryArgs argsCasted = (LaserFactoryArgs) args;
			return new LaserController(viewInstance, args.WorldSide, config, argsCasted.Owner);
		}
	}
}