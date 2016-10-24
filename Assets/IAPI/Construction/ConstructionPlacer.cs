using UnityEngine;
using System.Collections;

public class ConstructionPlacer : MonoBehaviour {

	public MainDatabase mDB;
	public PartData partData;
	public ConstructionManager CManager;

	void OnMouseDown ()
	{

	}

	void MovePlacer ()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,-10));
		transform.position = new Vector3((int)pos.x,(int)pos.y,(int)pos.z);
	}

	void Update ()
	{
		MovePlacer();
	}

}

