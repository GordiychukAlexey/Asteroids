using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Core.Tools.Pool {
	public abstract class WorldObjectsPool<TComponent> : IObjectPool<TComponent>
		where TComponent : Component, IWorldObjectPoolable<TComponent> {
		public event Action<TComponent> OnCreatePooledItem;
		public event Action<TComponent> OnTakeFromPool;
		public event Action<TComponent> OnReturnedToPool;
		public event Action<TComponent> OnDestroyPoolItem;

		public enum PoolType {
			ObjectPool,
			LinkedPool
		}

		private readonly TComponent prefab;
		private readonly Transform parentTransform;
		private readonly PoolType poolType;
		private readonly int maxPoolSize;
		private readonly bool collectionChecks;

		public WorldObjectsPool(
			TComponent prefab,
			Transform parentTransform,
			PoolType poolType,
			int maxPoolSize,
			bool collectionChecks){
			this.prefab = prefab;
			this.parentTransform = parentTransform;
			this.poolType = poolType;
			this.maxPoolSize = maxPoolSize;
			this.collectionChecks = collectionChecks;
		}

		private IObjectPool<TComponent> pool;

		public IObjectPool<TComponent> Pool{
			get{
				if (pool == null){
					switch (poolType){
						case PoolType.ObjectPool:
							pool = new ObjectPool<TComponent>(CreatePooledItemHandler, TakeFromPoolHandler, ReturnedToPoolHandler,
															  DestroyPoolItemHandler,
															  collectionChecks, 10, maxPoolSize);
							break;
						case PoolType.LinkedPool:
							pool = new LinkedPool<TComponent>(CreatePooledItemHandler, TakeFromPoolHandler, ReturnedToPoolHandler,
															  DestroyPoolItemHandler,
															  collectionChecks, maxPoolSize);
							break;
						default:
							throw new Exception("Unknown pool type");
					}
				}

				return pool;
			}
		}

		public int CountInactive => Pool.CountInactive;

		public TComponent Get() => Pool.Get();

		public PooledObject<TComponent> Get(out TComponent v) => Pool.Get(out v);

		public void Release(TComponent element) => Pool.Release(element);

		public void Clear() => Pool.Clear();

		protected virtual TComponent CreatePooledItemHandler(){
			TComponent instance = Object.Instantiate(prefab);

			if (parentTransform != null){
				instance.SetParent(parentTransform);
			}

			OnCreatePooledItem?.Invoke(instance);

			return instance;
		}

		protected virtual void TakeFromPoolHandler(TComponent item){
			item.OnSpawned(Pool);
			item.gameObject.SetActive(true);

			OnTakeFromPool?.Invoke(item);
		}

		protected virtual void ReturnedToPoolHandler(TComponent item){
			item.OnDespawned();
			item.gameObject.SetActive(false);

			OnReturnedToPool?.Invoke(item);
		}

		protected virtual void DestroyPoolItemHandler(TComponent item){
			OnDestroyPoolItem?.Invoke(item);

			Object.Destroy(item.gameObject);
		}
	}
}