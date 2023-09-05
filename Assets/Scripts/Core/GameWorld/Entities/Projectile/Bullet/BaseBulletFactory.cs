using Core.GameWorld.Entities.Bullet.PlayerBullet;
using Core.GameWorld.Entities.Projectile.Bullet;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.GameWorld.Entities.Bullet {
//	public abstract class BaseBulletFactory<TBulletController,TView, TArgs> :
//		BaseProjectileFactory<IBulletController, TView, TArgs>, IBulletFactory<IBulletController,TArgs>
//		where TBulletController : IBulletController
//		where TView : Object, IBulletView
//		where TArgs : IBulletFactoryArgs {
//		public BaseBulletFactory(IObjectPool<TView> viewPool) : base(viewPool){ }
//
////		public IBulletController Create(IBulletFactoryArgs args){
////			var controller = base.Create((TArgs) args);
////			controller.AddSpeed(args.OwnerMovingSpeed);
//////			controller.SetAngularSpeed(args.OwnerAngularSpeed);
////
////			return controller;
////		}
//
//		public TBulletController Create(IBulletFactoryArgs args){
//			var controller = base.Create((TArgs) args);
//			controller.AddSpeed(args.OwnerMovingSpeed);
////			controller.SetAngularSpeed(args.OwnerAngularSpeed);
//
//			return (TBulletController)controller;
//		}
//	}


//	public abstract class BaseBulletFactory<TView, TArgs> :
//		BaseProjectileFactory<IBulletController, TView, TArgs>,
//		IBulletFactory<IBulletController, TArgs>
//		where TView : Object, IBulletView
//		where TArgs : IBulletFactoryArgs {
//		public BaseBulletFactory(IObjectPool<TView> viewPool) : base(viewPool){ }
//
//		public IBulletController Create(IBulletFactoryArgs args){
//			var controller = base.Create((TArgs) args);
//			controller.AddSpeed(args.OwnerMovingSpeed);
////			controller.SetAngularSpeed(args.OwnerAngularSpeed);
//
//			return controller;
//		}
//	}
}