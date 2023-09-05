using System;
using System.Linq;
using Core.GameWorld.Entities.Bullet;
using Core.GameWorld.Entities.Projectile;
using Core.Tools;
using Core.Tools.InfinityWorld;
using Core.Tools.ServiceLocator;
using UnityEngine;

namespace Core.GameWorld.ShootController {
//	public class PeriodicalTimer {
//		
//		public bool IsPause{ get; set; } = false;
//		
//		private readonly float fireRate;
//		private float nextShootTime = -1;
//		
//		public bool Check(){
//				if (Time.time > nextShootTime){
//					nextShootTime = Time.time + fireRate;
//					Shoot();
//				}
//		}
//		
//		public virtual void Update(float dt){
//			if (!IsPause){
//				if (Time.time > nextShootTime){
//					nextShootTime = Time.time + fireRate;
//					Shoot();
//				}
//			}
//		}
//	}

	public class ShootController
//		<TProjectileController, TArgs> 
		: IShootController
//		where TProjectileController : class, IProjectileController
//		where TArgs : IProjectileFactoryArgs
	{
//		public virtual event EventHandler<TBulletController> OnShoot;
		public event EventHandler<IProjectileController[]> OnShoot;

		public bool IsSpawnActive{ get; set; } = false;
		protected virtual bool IsReallySpawnActive => IsSpawnActive; //todo

		private readonly IWorldObjectController owner;

		private readonly IProjectileFactory
			//<TProjectileController, TArgs> 
			projectileFactory;

		private readonly Func<IProjectileFactoryArgs> getArgs;
		private readonly float shootDelay;

		private readonly bool isUseVirtualBullets;

//		private readonly PositionToVirtualPositionsAdapter positionToVirtualPositionsAdapter;
		private readonly InfinityWorld infinityWorld;
		private float nextShootTime = -1;

		public ShootController(IWorldObjectController owner,
							   IProjectileFactory
								   //<TProjectileController, TArgs> 
								   projectileFactory,
							   Func<IProjectileFactoryArgs> getArgs,
							   float fireRate,
							   bool isUseVirtualBullets){
			this.owner = owner;
			this.projectileFactory = projectileFactory;
			this.getArgs = getArgs;

			this.shootDelay = 1.0f / fireRate;
			this.isUseVirtualBullets = isUseVirtualBullets;

//			positionToVirtualPositionsAdapter = ServiceLocator.Resolve<PositionToVirtualPositionsAdapter>();
			infinityWorld = ServiceLocator.Resolve<InfinityWorld>();
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

////			IProjectileController[] controllers = isUseVirtualBullets ? new IProjectileController[5] : new IProjectileController[1];
////			controllers[0] = //(TProjectileController)
////				projectileFactory.Create(args);
//
////			OnShoot?.Invoke(this, controller);
////			OnShoot?.Invoke(this, new []{controller});
//
//			if (isUseVirtualBullets){
//				var virtualPositions = infinityWorld.ToVirtualPositions(args.Position);
//				for (var i = 0; i < virtualPositions.Length; i++){
//					Vector2 virtualPosition = virtualPositions[i];
//					args.Position = virtualPosition;
//					controllers[i + 1] = //(TProjectileController)
//						projectileFactory.Create(args);
//
////					OnShoot?.Invoke(this, controller2);
//				}
//			}

			OnShoot?.Invoke(this, projectiles);
		}

		public virtual void Dispose(){ }
	}
}