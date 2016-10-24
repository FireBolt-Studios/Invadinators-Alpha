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
		Collider2D[] overlap = Physics2D.OverlapAreaAll(col.bounds.min,col.bounds.max,Physics.DefaultRaycastLayers,0,10);

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

		if (partData.Quantity > 1)
		{
			TransformData tData = new TransformData();

			tData.PositionX = transform.position.x;
			tData.PositionY = transform.position.y;
			tData.PositionZ = transform.position.z;

			tData.RotationX = transform.eulerAngles.x;
			tData.RotationY = transform.eulerAngles.y;
			tData.RotationZ = transform.eulerAngles.z;

			partData.Transform = tData;

			GameObject newPart = DataUtility.MakePart(partData,mDB);
			newPart.transform.localScale = transform.localScale;
			partData.Quantity -= 1;
			CManager.DisplayParts(partType);
		}
		else
		{
			DataUtility.MakePart(partData,mDB);
			int typeIndex = CManager.GManager.PManager.ActiveProfile.Cargo.IndexOf(partType);
			CManager.GManager.PManager.ActiveProfile.Cargo[typeIndex].Parts.Remove(partData);
			CManager.DisplayParts(DataUtility.GetPartType(partData.Type+"s",CManager.GManager.PManager.ActiveProfile));
			Destroy(gameObject);
		}
	}

	void Update ()
	{
		if (CManager.CurrentTile != null)
		{
			CheckCollisions();

			if (partData.Sprite.Contains("Large"))
			{
				transform.position = CManager.CurrentTile.position-new Vector3(0.25f,0.25f,10);
			}
			else
			{ 
				transform.position = CManager.CurrentTile.position-new Vector3(0,0,10);
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

