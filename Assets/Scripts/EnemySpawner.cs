using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height =5f;

	public float speed =5f;
	public float padding = 1f;

	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xmin;
	private float xmax;

	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xmin = leftBoundary.x + padding;
		xmax = rightBoundary.x - padding;

		SpawnEnemies ();
		}
		
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		if (leftEdgeOfFormation < xmin) {
			movingRight = true;
		}
		else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}
	
		if (AllMembersDead ()) {
			SpawnUntilFormationFull ();
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {
				return false;
			} 
		}
		return true;
	}

	Transform NextFreePostition() {
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			} 
		}
		return null;
	}

	void SpawnEnemies()
	{
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFormationFull()
	{
		Transform freePosition = NextFreePostition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePostition()){
		Invoke ("SpawnUntilFormationFull", spawnDelay);
		}
	}
}