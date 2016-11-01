using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int Durability;
	public GameObject ExplosionEffect;

	void Update ()
	{
		CheckCollisions();
		if (Durability <= 0)
		{
			Destroy(gameObject);
			Instantiate(ExplosionEffect,transform.position,Quaternion.identity);
		}
	}

	void CheckCollisions ()
	{
		CircleCollider2D col = gameObject.GetComponent<CircleCollider2D>();
		Collider2D[] overlap = Physics2D.OverlapAreaAll(col.bounds.min,col.bounds.max,Physics.DefaultRaycastLayers,0,5);

		if (overlap.Length > 1)
		{
			foreach (Collider2D c2D in overlap)
			{
				if (c2D.tag == "Projectile")
				{
					Durability -= c2D.GetComponent<ShipProjectile>().damage;
				}
			}
		}
		else
		{

		}
	}
}
