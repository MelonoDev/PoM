using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public GameObject SmallSkeleton;
	public GameObject BigSkeleton;
	public GameObject Warhound;


	public void SpawnEnemyForWave (int waveNumber, int spawnPointNumber){
		int e = 0;
		while (waveNumber > e) {
			if (e % 7 <= 2) {
				Invoke ("SpawnSmallSkeleton", e*2);
			}
			if (e % 3 == 2) {
				Invoke ("SpawnBigSkeleton", e*2);
			}
			if (e % 4 == 3) {
				Invoke ("SpawnWarhound", e*2);
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
}
