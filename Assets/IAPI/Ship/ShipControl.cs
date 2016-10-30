using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {

	public Ship ship;

	public Transform reticle;

	void Awake ()
	{
		reticle = GameObject.Find("Reticle").transform;
	}

	void Aim ()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;
		reticle.position = Camera.main.ScreenToWorldPoint(mousePos);

		Transform Pivot = transform.GetChild(0);

		Vector3 dir = reticle.position - Pivot.transform.position;
		var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion finalRot = Quaternion.AngleAxis(rot-90, Vector3.forward);
		Pivot.rotation = Quaternion.Lerp(Pivot.rotation,finalRot,ship.Torque*Time.deltaTime);

		float angle = Pivot.transform.localEulerAngles.z;
		angle = (angle > 180) ? angle - 360 : angle;

		if (angle < -90)
		{

			print("Clamping Right");
			Vector3 euler = Pivot.transform.localEulerAngles;
			Pivot.transform.localEulerAngles = new Vector3(euler.x,euler.y,-90);
		}

		if (angle > 90)
		{

			print("Clamping Left");
			Vector3 euler = Pivot.transform.localEulerAngles;
			Pivot.transform.localEulerAngles = new Vector3(euler.x,euler.y,90);
		}
	}

	void FixedUpdate ()
	{
		if (ship.Active)
		{
			Aim();
			
			transform.Translate(new Vector3(1,0,0)*Input.GetAxis("Horizontal")*ship.Thrust*Time.deltaTime);
			transform.Translate(new Vector3(0,1,0)*Input.GetAxis("Vertical")*ship.Thrust*Time.deltaTime);
		}
	}

}
