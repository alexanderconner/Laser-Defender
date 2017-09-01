using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float speed = 15.0f;
	public float padding = 1f;
	public float firingRate = 0.2f;
	public float health = 250f;
	float xmin;
	float xmax;

	public AudioClip shot;
	public AudioClip hit;
	public AudioClip death;

	private Vector3 pos;
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));;
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
		{	
			InvokeRepeating ("Fire", 0.00001f, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space))
			{
			CancelInvoke ("Fire");

			}
		if (Input.GetKey (KeyCode.UpArrow))
		{	
			//transform.position += new Vector3 (0, +speed* Time.deltaTime, 0);
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{	
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{	
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{	
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		//restrict player Touch gamespace
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("Player Hit by Missile");
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();

		if (missile) {
			health -= missile.getDamage ();
			missile.Hit();
			AudioSource.PlayClipAtPoint (hit, transform.position);
			if (health <= 0) {
				AudioSource.PlayClipAtPoint (death, transform.position);
				LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
				man.LoadLevel ("Win");
				Destroy (gameObject);

			}
		} else { Debug.Log ("Player collision"); 
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3 (0, 1, 0);
		GameObject laser = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		//laser.transform.parent = child;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (shot, transform.position);
	}


}
