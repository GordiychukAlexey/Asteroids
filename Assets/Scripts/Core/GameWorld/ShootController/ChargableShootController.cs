using System;
using Core.GameWorld.Entities.Projectile;
using UnityEngine;

namespace Core.GameWorld.ShootController {
	public class ChargableShootController : ShootController {
		protected override bool IsReallySpawnActive => base.IsReallySpawnActive && CurrentCharges > 0;

		private readonly int maxCharges;

		private float currentChargeTime = 0.0f;
		private readonly float chargeTime;

		public int CurrentCharges{ get; private set; }

		public float ChargeTimeLeft => Mathf.Clamp(chargeTime-currentChargeTime, 0.0f, chargeTime);

		public ChargableShootController(
			IWorldObjectController owner,
			IProjectileFactory projectileFactory,
			Func<IProjectileFactoryArgs> getArgs,
			float fireRate,
			bool isUseVirtualBullets,
			int maxCharges,
			float chargeTime) : base(owner, projectileFactory, getArgs, fireRate, isUseVirtualBullets){
			this.maxCharges = maxCharges;
			CurrentCharges = maxCharges;

			this.chargeTime = chargeTime;

			OnShoot += ShootHandler;
		}

		private void ShootHandler(object sender, IProjectileController[] projectileController){
			CurrentCharges--;
		}

		public override void Update(float dt){
			if (CurrentCharges < maxCharges){
				currentChargeTime += dt;
				if (currentChargeTime >= chargeTime){
					currentChargeTime = 0.0f;
					CurrentCharges++;
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