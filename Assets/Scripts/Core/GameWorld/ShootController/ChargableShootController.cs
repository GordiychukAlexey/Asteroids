using System;
using Core.GameWorld.Entities.Projectile;

namespace Core.GameWorld.ShootController {
	public class ChargableShootController : ShootController {
		protected override bool IsReallySpawnActive => base.IsReallySpawnActive && currentCharges > 0;

		private readonly int maxCharges;

		private float currentChargeTime = 0.0f;
		private readonly float chargeTime;

		private int currentCharges;

		public ChargableShootController(
			IWorldObjectController owner,
			IProjectileFactory projectileFactory,
			Func<IProjectileFactoryArgs> getArgs,
			float fireRate,
			bool isUseVirtualBullets,
			int maxCharges,
			float chargeTime) : base(owner, projectileFactory, getArgs, fireRate, isUseVirtualBullets){
			this.maxCharges = maxCharges;
			currentCharges = maxCharges;

			this.chargeTime = chargeTime;

			OnShoot += ShootHandler;
		}

		private void ShootHandler(object sender, IProjectileController[] projectileController){
			currentCharges--;
		}

		public override void Update(float dt){
			if (currentCharges < maxCharges){
				currentChargeTime += dt;
				if (currentChargeTime >= chargeTime){
					currentChargeTime = 0.0f;
					currentCharges++;
				}
			}

			base.Update(dt);
		}

		public override void Dispose(){
			OnShoot -= ShootHandler;

			base.Dispose();
		}
	}
}