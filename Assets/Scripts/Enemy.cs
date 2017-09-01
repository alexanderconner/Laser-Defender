using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 150f;

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float firingRate = 5f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;

	public AudioClip shot;
	public AudioClip hit;
	public AudioClip death;

	private ScoreKeeper scoreKeeper;


	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

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
			AudioSource.PlayClipAtPoint (hit, transform.position);
			if (health <= 0) {
				AudioSource.PlayClipAtPoint (death, transform.position);
				Destroy (gameObject); 
				scoreKeeper.Score (scoreValue);
			}
		} 
		else {
			Debug.Log ("Player collided with Enemy");
				AudioSource.PlayClipAtPoint (hit, transform.position);
		}
	}


	void Fire() {
		Vector3 startPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject laser = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (shot, transform.position);
	}

}