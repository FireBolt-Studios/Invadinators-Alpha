using IAPI.Database;
using UnityEngine;
using System.Collections;

public class ConstructionPlacer : MonoBehaviour {

	public MainDatabase mDB;
	public PartData partData;
	public ConstructionManager CManager;

	public bool canPlace;

	void CheckCollisions ()
	{
		BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
		Collider2D[] overlap = Physics2D.OverlapAreaAll(col.bounds.min,col.bounds.max,Physics.DefaultRaycastLayers,0,5);

		if (overlap.Length > 1)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
			canPlace = false;
		}
		else
		{
			GetComponent<SpriteRenderer>().color = DataUtility.ConvertColorData(partData.Colors[0]);
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = DataUtility.ConvertColorData(partData.Colors[1]);
			canPlace = true;
		}
	}

	void OnMouseDown ()
	{
		PartType partType = DataUtility.GetPartType(partData.Type+"s",CManager.GManager.PManager.ActiveProfile);

		if (partData.Quantity > 0)
		{
			CManager.PlacePart(partData,transform);

			if (partData.Quantity > 1)
			{
				partData.Quantity -= 1;
			}
			else
			{
				int typeIndex = CManager.GManager.PManager.ActiveProfile.Cargo.IndexOf(partType);
				CManager.GManager.PManager.ActiveProfile.Cargo[typeIndex].Parts.Remove(partData);
				CManager.DisplayParts(partType);
				Destroy(gameObject);
			}
		}


		CManager.DisplayParts(partType);
	}

	void Update ()
	{
		if (CManager.CurrentTile != null)
		{
			CheckCollisions();

			if (partData.Sprite.Contains("Large"))
			{
				transform.position = CManager.CurrentTile.position-new Vector3(0.25f,0.25f,5);
			}
			else
			{ 
				transform.position = CManager.CurrentTile.position-new Vector3(0,0,5);
			}

			if (Input.GetMouseButtonDown(1))
			{
				Destroy(gameObject);
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				transform.Rotate(new Vector3(0,0,90));
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				if (transform.GetComponent<SpriteRenderer>().flipX)
				{
					transform.GetComponent<SpriteRenderer>().flipX = false;
					transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
				}
				else
				{
					transform.GetComponent<SpriteRenderer>().flipX = true;
					transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
				}
			}
		}
	}

}

