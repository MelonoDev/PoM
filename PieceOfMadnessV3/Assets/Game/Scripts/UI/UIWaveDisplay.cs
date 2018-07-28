using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveDisplay : MonoBehaviour {

	public Text WaveText;

	// Use this for initialization
	void Awake () {
		WaveText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		WaveText.text = "WAVE " + SpawnScript.WaveNumber.ToString () + "\n" + "next wave in " + (Mathf.Round ((SpawnScript.WaveDuration - SpawnScript.WaveTimer))).ToString () + " sec";
	}
}
