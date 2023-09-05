namespace Core.GameWorld.Entities.Projectile.Bullet {
	public interface ILaserFactory //<TBulletController, TBulletFactoryArgs>
		: IProjectileFactory       //<TBulletController,TBulletFactoryArgs>
//	where TBulletController: IBulletController
//	where TBulletFactoryArgs: IBulletFactoryArgs
	{
//		public TBulletController Create(TBulletFactoryArgs args);
	}
}