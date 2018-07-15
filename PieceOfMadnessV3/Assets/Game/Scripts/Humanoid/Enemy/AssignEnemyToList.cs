using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignEnemyToList : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EnemyListMaker.EnemiesList.Add (gameObject);	
	}

	//on death: 		EnemyListMaker.EnemiesList.Remove (gameObject);	


}
