using System;
using System.Collections;
using Core.Tools.Pool;
using GameWorld.Entities.EnemyShip;
using UnityEngine;

public class Timer :MonoBehaviour {
	private EnemyShip pooledItem;
	private void Awake(){
		pooledItem = GetComponent<EnemyShip>();
	}

	private void OnEnable(){
		StartTimer();
	}

	public void StartTimer(){
		StartCoroutine(TimerCoroutine());
	}
		
	private  IEnumerator TimerCoroutine(){
		yield return new WaitForSeconds(3);
		pooledItem.Dispose();
	}
}