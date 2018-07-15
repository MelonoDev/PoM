using System.Collections;
using UnityEngine;

public class Enemy : Humanoid{

	void Start (){
		gameObject.tag = "Enemy";
		gameObject.layer = LayerMask.NameToLayer("EnemyLayer");
	}

	void Update (){
		Test ();
		Invulnerable ();
	}

	protected override void Test ()
	{
		print (Health.ToString () + " EnemyHealth");
	}
		
	protected override void Move ()
	{
		throw new System.NotImplementedException ();
	}

	protected override void KnockBack (string attackType)
	{
		base.KnockBack (attackType);
		print (attackType + "Knockback received");
	}
}