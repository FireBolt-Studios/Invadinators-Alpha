using UnityEngine;
using System.Collections;

public class ShipProjectile : MonoBehaviour {

	public float speed;
	public int damage;
	public bool rotate;
	public float rotateSpeed;

	void Start ()
	{
		Destroy(gameObject,4);
	}

	void Update ()
	{
		transform.Translate(-Vector2.up*speed*Time.deltaTime);
		if (rotate)
		{
			transform.Rotate(Vector3.forward*rotateSpeed*Time.deltaTime);
		}
	}

}
