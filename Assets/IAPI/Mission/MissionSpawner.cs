using UnityEngine;
using System.Collections;

public class MissionSpawner : MonoBehaviour {

	public bool playerDirection;
	public GameManager GManager;

	public Transform player;

	void Start ()
	{
		GManager = GameObject.FindObjectOfType<GameManager>();
		player = GameObject.FindObjectOfType<Ship>().transform;
	}

	void Update ()
	{
//		if (player != null)
//		{
//			Vector3 dir = player.position - transform.position;
//			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
//			transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
//		}
	}

	public void Spawn ()
	{
		int randomAsteroid = Random.Range(0,GManager.mDB.Asteroids.Length);
		GameObject newAsteroid = Instantiate(GManager.mDB.Asteroids[randomAsteroid],transform.position,transform.rotation) as GameObject;
		newAsteroid.GetComponent<ShipProjectile>().speed = Random.Range(5,25);
	}
}
