using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public GameObject SmallSkeleton;
	public GameObject BigSkeleton;
	public GameObject Warhound;
	public GameObject Bowman;


	public void SpawnEnemyForWave (int waveNumber, int spawnPointNumber){
		int e = 0;
		while (waveNumber > e) {
			if (e % 6 <= 1) {
				Invoke ("SpawnSmallSkeleton", e*2);
			}
			if (e % 6 == 1) {
				Invoke ("SpawnBigSkeleton", e*2);
			}
			if (e % 6 == 1) {
				Invoke ("SpawnWarhound", e*2);
			}
			if (e % 6 == 1) {
				Invoke ("SpawnBowman", e*2);
			}
			e++;
		}
	}

	void SpawnSmallSkeleton(){
		Instantiate (SmallSkeleton, gameObject.transform.position, gameObject.transform.rotation);
	}

	void SpawnBigSkeleton (){
		Instantiate (BigSkeleton, gameObject.transform.position, gameObject.transform.rotation);
	}

	void SpawnWarhound (){
		Instantiate (Warhound, gameObject.transform.position, gameObject.transform.rotation);
	}
	void SpawnBowman (){
		Instantiate (Bowman, gameObject.transform.position, gameObject.transform.rotation);
	}
}
