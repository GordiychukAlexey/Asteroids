using System;
using System.Linq;
using Core.GameWorld.Entities.Projectile;
using Core.Tools.InfinityWorld;
using UnityEngine;

namespace Core.GameWorld.ShootController {
	public class ShootController : IShootController {
		public event EventHandler<IProjectileController[]> OnShoot;

		public bool IsSpawnActive{ get; set; } = false;
		protected virtual bool IsReallySpawnActive => IsSpawnActive;

		private readonly IWorldObjectController owner;
		private readonly IProjectileFactory projectileFactory;
		private readonly Func<IProjectileFactoryArgs> getArgs;
		private readonly float shootDelay;
		private readonly bool isUseVirtualBullets;
		private readonly IInfinityWorld infinityWorld;

		private float nextShootTime = -1;

		public ShootController(IWorldObjectController owner,
							   IProjectileFactory projectileFactory,
							   Func<IProjectileFactoryArgs> getArgs,
							   float fireRate,
							   bool isUseVirtualBullets,
							   IInfinityWorld infinityWorld){
			this.owner = owner;
			this.projectileFactory = projectileFactory;
			this.getArgs = getArgs;
			shootDelay = 1.0f / fireRate;
			this.isUseVirtualBullets = isUseVirtualBullets;

			this.infinityWorld = infinityWorld;
		}

		public virtual void Update(float dt){
			if (IsReallySpawnActive){
				if (Time.time > nextShootTime){
					nextShootTime = Time.time + shootDelay;
					Shoot();
				}
			}
		}

		private void Shoot(){
			var args = getArgs();

			var tmpPosition = args.Position;

			IProjectileController[] projectiles =
				(isUseVirtualBullets
					? InfinityWorld.WorldSides
					: new[]{InfinityWorldSide.Center})
			   .Select(side => {
					var virtualPosition = infinityWorld.ToSidePosition(tmpPosition, side);

					args.Position = virtualPosition;
					args.WorldSide = side;

					return projectileFactory.Create(args);
				})
			   .ToArray();

			OnShoot?.Invoke(this, projectiles);
		}

		public virtual void Dispose(){ }
	}
}