using System;
using UnityEngine;

namespace Core.GameWorld.Entities.EnemyShip {
	public interface IEnemyShipController : IWorldObjectController {
		void SetTarget(IWorldObjectController target);
//		void MoveForward(float value);
//		void RotateTowards(Vector2 direction);
	}
}