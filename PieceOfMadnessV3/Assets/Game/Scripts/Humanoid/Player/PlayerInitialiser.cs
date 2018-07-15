using System.Collections;
using UnityEngine;

public class PlayerInitialiser : MonoBehaviour {

	void Awake (){
		Humanoid<Player> playerCharacter = new Humanoid<Player> ("PlayerCharacter");
	}
}
 