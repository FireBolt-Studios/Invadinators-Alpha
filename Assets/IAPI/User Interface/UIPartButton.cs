using IAPI.Database;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIPartButton : MonoBehaviour {

	public ConstructionManager CManager;
	public PartData partData;
	public Text Quantity;

	public void SetProperties ()
	{
		SpriteData spriteData = DataUtility.GetSpriteData(partData.Sprite,CManager.mDB);
		transform.GetChild(0).GetComponent<Image>().sprite = spriteData.Base;
		transform.GetChild(1).GetComponent<Image>().sprite = spriteData.Detail;
		Quantity.text = partData.Quantity.ToString();

		if (partData.Sprite.Contains("Small"))
		{
			transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(20,20);
			transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(20,20);
		}
	}

	public void OnClick ()
	{
		partData.Colors[0] = DataUtility.ConvertColor(CManager.BaseColor.color);
		partData.Colors[1] = DataUtility.ConvertColor(CManager.DetailColor.color);
		CManager.SpawnPlacer(partData);
	}

	public void OnExit ()
	{
		CManager.PartInfo.SetActive(false);
	}

	public void OnOver ()
	{
		CManager.PartInfo.SetActive(true);
		CManager.PartInfo.transform.position = Input.mousePosition;
		CManager.PartInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = partData.Name;
		CManager.PartInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = CManager.mDB.rarities[partData.Rarity].color;
		if (partData.Name.Contains("Cockpit"))
		{

		}
		else if (partData.Name.Contains("Reactor"))
		{

		}
		else if (partData.Name.Contains("Shield"))
		{

		}
		else if (partData.Name.Contains("Thruster"))
		{

		}
		else if (partData.Name.Contains("Weapon"))
		{

		}
		else
		{

		}
	}

}
