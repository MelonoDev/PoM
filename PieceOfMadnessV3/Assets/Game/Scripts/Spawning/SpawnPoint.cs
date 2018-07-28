using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public GameObject SmallSkeleton;
	public GameObject BigSkeleton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnEnemyForWave (int waveNumber, int spawnPointNumber){
		int e = 0;
		while (waveNumber * 2 > e) {
			Invoke ("SpawnSmallSkeleton", 1f);
			e++;
		}
	}

	void SpawnSmallSkeleton(){
		Instantiate (SmallSkeleton, gameObject.transform.position, gameObject.transform.rotation);
	}
}
