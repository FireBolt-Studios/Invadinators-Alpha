using IAPI.Database;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructionManager : MonoBehaviour {

	public GameManager GManager;
	public MainDatabase mDB;

	public Image BaseColor;
	public Image DetailColor;

	public GameObject PartButton;
	public Transform PartDisplay;

	public Transform CurrentTile;

	public Slider[] ColorSliders;
	public Text[] ColorValues;

	public GameObject PartInfo;

	public void SetColor ()
	{
		BaseColor.color = new Color(ColorSliders[0].value/255,ColorSliders[1].value/255,ColorSliders[2].value/255,1);
		DetailColor.color = new Color(ColorSliders[3].value/255,ColorSliders[4].value/255,ColorSliders[5].value/255,1);
		ColorValues[0].text = ColorSliders[0].value.ToString();
		ColorValues[1].text = ColorSliders[1].value.ToString();
		ColorValues[2].text = ColorSliders[2].value.ToString();
		ColorValues[3].text = ColorSliders[3].value.ToString();
		ColorValues[4].text = ColorSliders[4].value.ToString();
		ColorValues[5].text = ColorSliders[5].value.ToString();
	}

	void DestroyPartButtons ()
	{
		UIPartButton[] partButtons = GameObject.FindObjectsOfType<UIPartButton>();
		for (int i = 0; i < partButtons.Length; i++)
		{
			Destroy(partButtons[i].gameObject);
		}
	}

	public void DisplayParts (PartType parts)
	{
		DestroyPartButtons();

		float xOffest = 0;
		float yOffset = 0;
		int count = 0;

		foreach (PartData partData in parts.Parts)
		{
			GameObject partButton = Instantiate(PartButton,transform.position,Quaternion.identity) as GameObject;
			partButton.GetComponent<RectTransform>().SetParent(PartDisplay);

			partButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(10+xOffest,-10-yOffset);

			partButton.GetComponent<UIPartButton>().partData = partData;
			partButton.GetComponent<UIPartButton>().CManager = this;
			partButton.GetComponent<UIPartButton>().SetProperties();

			if (count < 4)
			{
				xOffest += 70;
				count += 1;
			}
			else
			{
				xOffest = 0;
				yOffset += 70;
				count = 0;
			}
		}
	}

	public void SpawnPlacer (PartData partData)
	{
		Destroy(GameObject.FindObjectOfType<ConstructionPlacer>());

		GameObject newPlacer = new GameObject("Placer",typeof(SpriteRenderer),typeof(BoxCollider2D),typeof(ConstructionPlacer));
		GameObject newDetail = new GameObject("Detail",typeof(SpriteRenderer));
		newDetail.transform.parent = newPlacer.transform;
		newDetail.GetComponent<SpriteRenderer>().sortingOrder = 1;

		SpriteData spriteData = DataUtility.GetSpriteData(partData.Sprite,mDB);
		newPlacer.GetComponent<SpriteRenderer>().sprite = spriteData.Base;
		newDetail.GetComponent<SpriteRenderer>().sprite = spriteData.Detail;

		newPlacer.GetComponent<SpriteRenderer>().color = BaseColor.color;
		newDetail.GetComponent<SpriteRenderer>().color = DetailColor.color;

		newPlacer.GetComponent<ConstructionPlacer>().partData = partData;
		newPlacer.GetComponent<ConstructionPlacer>().CManager = this;
		newPlacer.GetComponent<ConstructionPlacer>().mDB = mDB;

		if (partData.Sprite.Contains("Small"))
		{
			newPlacer.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		}

		newPlacer.GetComponent<BoxCollider2D>().size = new Vector2(0.9f,0.9f);
	}

}
