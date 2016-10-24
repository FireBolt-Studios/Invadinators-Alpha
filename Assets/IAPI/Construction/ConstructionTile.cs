using UnityEngine;
using System.Collections;

public class ConstructionTile : MonoBehaviour {

	public ConstructionManager CManager;

	void OnMouseOver ()
	{
		CManager.CurrentTile = transform;
	}

}
