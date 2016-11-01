using UnityEngine;
using System.Collections;

public class PartDisplay : MonoBehaviour {

	public Part part;
	public SpriteRenderer SR;

	public float percent;

	void Awake ()
	{
		SR = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		if (SR != null)
		{
			float cDura = part.PartData.CurrentDurability;
			float mDura = part.PartData.MaxDurability;
			percent = (cDura/mDura);
			SR.color = Color.Lerp(Color.red,Color.green,percent);
		}
	}
}
