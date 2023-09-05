//using System;
//using Core.GameWorld.Entities.Projectile;
//using Core.GameWorld.Entities.Projectile.Laser;
//using Core.Tools.InfinityWorld;
//using Core.Tools.ServiceLocator;
//using UnityEngine;
//
//namespace Core.GameWorld.ShootController {
//	public class LaserShootController : IShootController {
//		public event EventHandler<IProjectileController[]> OnShoot;
//
//		public bool IsSpawnActive{ get; set; } = false;
//		protected virtual bool IsReallySpawnActive => IsSpawnActive; //todo
//
//		private readonly IWorldObjectController owner;
//		private readonly ILaserFactory projectileFactory;
//		private readonly Func<ILaserFactoryArgs> getArgs;
//		private readonly float fireRate;
//		private readonly bool isUseVirtualBullets;
//		private readonly float maxCharge;
//		private readonly float chargeTime;
//		private readonly InfinityWorld infinityWorld;
//
//		private float currentCharge;
//		private float currentChargeTime = 0.0f;
//		private float nextShootTime = -1;
//
//		public LaserShootController(IWorldObjectController owner,
//									ILaserFactory projectileFactory,
//									Func<ILaserFactoryArgs> getArgs,
//									float fireRate,
//									bool isUseVirtualBullets,
//									float maxCharge,
//									float chargeTime){
//			this.owner = owner;
//			this.projectileFactory = projectileFactory;
//			this.getArgs = getArgs;
//			this.fireRate = 1.0f / fireRate;
//			this.isUseVirtualBullets = isUseVirtualBullets;
//			this.maxCharge = maxCharge;
//			currentCharge = maxCharge;
//			this.chargeTime = chargeTime;
//
//			infinityWorld = ServiceLocator.Resolve<InfinityWorld>();
//
//			OnShoot += ShootHandler;
//		}
//
//		public virtual void Update(float dt){
//			if (currentCharge < maxCharge){
//				currentChargeTime += dt;
//				if (currentChargeTime >= chargeTime){
//					currentChargeTime = 0.0f;
//					currentCharge++;
//				}
//			}
//
//			if (IsReallySpawnActive){
//				if (Time.time > nextShootTime){
//					nextShootTime = Time.time + fireRate;
//					Shoot();
//				}
//			}
//		}
//
//		private void Shoot(){
//			var args = getArgs();
//
//			IProjectileController[] controllers = isUseVirtualBullets ? new IProjectileController[5] : new IProjectileController[1];
//			controllers[0] = projectileFactory.Create(args);
//
//			if (isUseVirtualBullets){
//				var virtualPositions = infinityWorld.ToVirtualPositions(args.Position);
//				for (var i = 0; i < virtualPositions.Length; i++){
//					Vector2 virtualPosition = virtualPositions[i];
//					args.Position = virtualPosition;
//					controllers[i + 1] = projectileFactory.Create(args);
//				}
//			}
//
//			OnShoot?.Invoke(this, controllers);
//		}
//		
//		private void ShootHandler(object sender, IProjectileController[] projectileController){
//			currentCharge--;
//		}
//
//		public virtual void Dispose(){
//			OnShoot -= ShootHandler;
//		}
//	}
//}

