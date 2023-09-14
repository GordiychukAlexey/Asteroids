using Core.GameWorld.Entities.Projectile;
using Core.GameWorld.Entities.Projectile.Laser;
using Core.GameWorld.WorldBoundsProvider;
using Core.Tools.InfinityWorld;
using UnityEngine.Pool;

namespace GameWorld.Entities.PlayerShip.Projectile.Laser {
	public class LaserFactory : BaseProjectileFactory<LaserController, Laser, LaserFactoryArgs>, ILaserFactory {
		private readonly LaserConfig config;
		private readonly IWorldBoundsProvider worldBoundsProvider;
		private readonly IInfinityWorld infinityWorld;

		public LaserFactory(
			IObjectPool<Laser> viewPool, 
			LaserConfig config, 
			IWorldBoundsProvider worldBoundsProvider,
			IInfinityWorld infinityWorld)
			: base(viewPool){
			this.config = config;
			this.worldBoundsProvider = worldBoundsProvider;
			this.infinityWorld = infinityWorld;
		}

		public IProjectileController Create(ILaserFactoryArgs args){
			var controller = base.Create((LaserFactoryArgs) args);

			return controller;
		}

		protected override IProjectileController CreateController(Laser viewInstance, IProjectileFactoryArgs args){
			LaserFactoryArgs argsCasted = (LaserFactoryArgs) args;
			return new LaserController(viewInstance, args.WorldSide, config, argsCasted.Owner, worldBoundsProvider, infinityWorld);
		}
	}
}