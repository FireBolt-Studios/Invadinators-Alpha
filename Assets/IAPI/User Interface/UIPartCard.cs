using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPartCard : MonoBehaviour {

	public bool Animating;
	public float timer;
	public float speedTimer;
	public float speed;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Animating = true;
		}
		
		if (Animating)
		{
			if (speedTimer >= 0.5f)
			{
				speed -= 200;
				speedTimer = 0;
			}
			else
			{
				speedTimer += Time.deltaTime;
			}

			if (timer >= 2.5f)
			{
				Animating = false;
				timer = 0;
			}
			else
			{
				transform.Rotate(Vector3.up*speed*Time.deltaTime);
				timer += Time.deltaTime;
			}
		}
		else
		{
			transform.eulerAngles = new Vector3(0,0,0);
			timer = 0;
			speedTimer = 0;
			speed = 1100;
		}
	}

}
