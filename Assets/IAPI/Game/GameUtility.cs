using UnityEngine;
using System.Collections;

namespace IAPI.Game {
	public class GameUtility {

		public static bool CenterParts (Transform Ship)
		{
			Vector3 center = new Vector3(0,0,0);
			float count = 0;

			Part[] Parts = Ship.GetComponentsInChildren<Part>();

			foreach (Part part in Parts)
			{
				center += part.transform.position;
				count++;
			}
			Vector3 theCenter = center / count;

			GameObject pivot = new GameObject("Pivot");
			pivot.transform.position = theCenter;
			foreach (Part part in Parts)
			{
				part.transform.parent = pivot.transform;
			}
			pivot.transform.parent = Ship;
			pivot.transform.localPosition = Vector3.zero;
			return true;
		}

	}
}
