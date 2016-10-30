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

	public InputField nameField;

	public Slider[] ColorSliders;
	public Text[] ColorValues;

	public GameObject PartInfo;

	public Transform Ship;

	public void SaveShip ()
	{
		if (nameField.text == "")
		{
			print("NO NAME INPUT");
			return;
		}

		Part[] parts = Ship.GetComponentsInChildren<Part>();
		ShipData newShip = new ShipData();
		newShip.Name = nameField.text;

		foreach (Part part in parts)
		{
			print("SAVED PART: "+part.PartData.Name);
			newShip.Parts.Add(part.PartData);
		}

		GManager.PManager.ActiveProfile.Ships.Add(newShip);
		DataUtility.Save(Application.persistentDataPath+"/Profiles/Profile.pro",GManager.PManager.ActiveProfile);
	}

	public void LoadShip ()
	{
		ShipData shipData = DataUtility.GetShip(nameField.text,GManager.PManager.ActiveProfile);
		foreach (PartData partData in shipData.Parts)
		{
			GameObject part = DataUtility.SpawnPart(partData,mDB,Ship);
			part.GetComponent<Part>().Ship = Ship.GetComponent<Ship>();
		}
		Ship.GetComponent<Ship>().ShipData = shipData;
		Ship.GetComponent<Ship>().Initialize();
	}

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
		
	public void PlacePart (PartData partData,Transform placer)
	{
		// Create Gameobjects
		GameObject newPart = new GameObject(partData.Name,typeof(SpriteRenderer),typeof(BoxCollider2D),typeof(Part));
		GameObject newDetail = new GameObject("Detail",typeof(SpriteRenderer));

		// Setup Transforms
		newDetail.transform.parent = newPart.transform;
		newPart.transform.position = placer.position;
		newPart.transform.eulerAngles = placer.eulerAngles;
		newPart.transform.localScale = placer.localScale;
		newPart.transform.parent = Ship;

		//Setup Sprite Renderers
		SpriteRenderer Base = newPart.GetComponent<SpriteRenderer>();
		SpriteRenderer Detail = newDetail.GetComponent<SpriteRenderer>();
		Base.sprite = placer.GetComponent<SpriteRenderer>().sprite;
		Base.color = placer.GetComponent<SpriteRenderer>().color;
		Base.flipX = placer.GetComponent<SpriteRenderer>().flipX;
		Detail.sprite = placer.GetChild(0).GetComponent<SpriteRenderer>().sprite;
		Detail.color = placer.GetChild(0).GetComponent<SpriteRenderer>().color;
		Detail.flipX = placer.GetChild(0).GetComponent<SpriteRenderer>().flipX;
		Detail.sortingOrder = 1;

		//Setup Part Data
		PartData copiedData = DataUtility.DeepCopy<PartData>(partData);
		copiedData.Transform = DataUtility.ConvertTransform(newPart.transform.localPosition,newPart.transform.localEulerAngles);
		copiedData.Colors[0] = DataUtility.ConvertColor(Base.color);
		copiedData.Colors[1] = DataUtility.ConvertColor(Detail.color);
		copiedData.FlipX = Base.flipX;
		newPart.GetComponent<Part>().PartData = copiedData;
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
