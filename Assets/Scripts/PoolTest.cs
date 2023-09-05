//using System;
//using GameWorld.Entities.EnemyShip;
//using UnityEngine;
//using Random = UnityEngine.Random;
//
//public class PoolTest : MonoBehaviour {
//	[SerializeField]private EnemyShip prefab;
//	private EnemyShipPool objectPool;
//
//	private void Awake(){
//		objectPool = new EnemyShipPool(
//			prefab,
//			EnemyShipPool.PoolType.ObjectPool,
//			10,
//			true);
//	}
//		
//	private void OnGUI(){
//		if (GUILayout.Button("Pool spawn")){
//			var amount = Random.Range(1, 3);
//			for (int i = 0; i < amount; ++i){
//				var ps = objectPool.Get();
//				ps.transform.position =transform.position+ (Vector3)Random.insideUnitCircle * 10;
//			}
//		}
//	}
//}