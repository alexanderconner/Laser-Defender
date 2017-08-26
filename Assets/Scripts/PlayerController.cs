﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float speed = 15.0f;
	public float padding = 1f;
	public float firingRate = 0.2f;
	float xmin;
	float xmax;

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

	void Fire() {
		GameObject laser = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		//laser.transform.parent = child;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
	}


}
