using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Black : MonoBehaviour {

	public float health = 150f;

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.getDamage ();
			missile.Hit();
			if (health <= 0) {
				Destroy (gameObject);
			}
		} else { Debug.Log ("Player collision"); 
		}
	}
}