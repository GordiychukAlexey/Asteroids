using System;
using System.Collections.Generic;
using Core.GameWorld;

namespace Core {
	public class Scorer :IDisposable{
		public int CurrentScore{ get; private set; }

		private readonly List<IDestroyableWorldObjectController> destroyables = new List<IDestroyableWorldObjectController>();

		public void Register(IDestroyableWorldObjectController destroyableWorldObjectController){
			destroyables.Add(destroyableWorldObjectController);

			destroyableWorldObjectController.OnDestroy += DestroyHandler;
			destroyableWorldObjectController.OnDispose += DisposeHandler;
		}

		private void DestroyHandler(IDestroyableWorldObjectController destroyableWorldObjectController){
			CurrentScore++;

			Unregister(destroyableWorldObjectController);
		}

		private void DisposeHandler(IWorldObjectController destroyable) =>
			Unregister((IDestroyableWorldObjectController) destroyable);

		private void Unregister(IDestroyableWorldObjectController destroyableWorldObjectController){
			destroyableWorldObjectController.OnDestroy -= DestroyHandler;
			destroyableWorldObjectController.OnDispose -= DisposeHandler;

			destroyables.Remove(destroyableWorldObjectController);
		}

		public void Dispose(){
			for (var index = 0; index < destroyables.Count; index++){
				IDestroyableWorldObjectController destroyable = destroyables[index];
				Unregister(destroyable);
			}
		}
	}
}