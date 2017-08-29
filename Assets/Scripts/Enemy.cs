using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 150f;

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float firingRate = 5f;
	public float shotsPerSecond = 0.5f;

	void Update () {
		float probability = shotsPerSecond * Time.deltaTime;
			if (Random.value < probability) {
				Fire ();
			}
		}

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


	void Fire() {
		Vector3 startPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject laser = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
	}

}