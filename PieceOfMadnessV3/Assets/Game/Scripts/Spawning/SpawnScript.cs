using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

	public bool NewWave = true;
	static public int WaveNumber = 0;
	static public float WaveTimer = 0;
	static public float WaveDuration;
	private AudioSource NextWaveAudio;

	// Use this for initialization
	void Awake () {
		NextWaveAudio = gameObject.GetComponent<AudioSource> ();
		WaveNumber = 0;
		WaveTimer = 0;

	}
	
	void Update () {
		CheckForNewWave (); 
	}

	void CheckForNewWave() {
		if (NewWave) {
			NewWave = false;
			NextWaveAudio.Play ();
			WaveNumber++;
			WaveDuration = WaveNumber * 4 + 15f;

			int i = 0;
			foreach (Transform spawnPoint in transform) {
				spawnPoint.gameObject.GetComponent<SpawnPoint> ().SpawnEnemyForWave (WaveNumber, i);
				i++;
			}
		} else {
			WaveTimer += Time.deltaTime;
		}

		if (WaveDuration <= WaveTimer) {
			NewWave = true;
			WaveTimer = 0;
		}
	}
}
