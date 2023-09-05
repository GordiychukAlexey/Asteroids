namespace Core.GameWorld.Entities.Projectile.Bullet {
	public interface IBulletFactory //<TBulletController, TBulletFactoryArgs>
		: IProjectileFactory        //<TBulletController,TBulletFactoryArgs>
//	where TBulletController: IBulletController
//	where TBulletFactoryArgs: IBulletFactoryArgs
	{
//		public TBulletController Create(TBulletFactoryArgs args);
	}

	//	public interface IBulletFactory:IProjectileFactory{
//		public TBulletController Create(TBulletFactoryArgs args);
//	}
}