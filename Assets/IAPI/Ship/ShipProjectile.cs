using UnityEngine;
using System.Collections;

public class ShipProjectile : MonoBehaviour {

	public float speed;

	void Start ()
	{
		Destroy(gameObject,2);
	}

	void Update ()
	{
		transform.Translate(-Vector2.up*speed*Time.deltaTime);
	}

}
