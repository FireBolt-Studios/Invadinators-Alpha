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
		for (int i = 0; i < CManager.PartInfo.transform.GetChild(1).childCount; i++)
		{
			CManager.PartInfo.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
		}
	}

	public void OnOver ()
	{
		CManager.PartInfo.SetActive(true);
		CManager.PartInfo.transform.position = Input.mousePosition;
		CManager.PartInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = partData.Name;
		CManager.PartInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().color = CManager.mDB.rarities[partData.Rarity].color;
		if (partData.Name.Contains("Cockpit"))
		{
			Transform tData = CManager.PartInfo.transform.GetChild(1).GetChild(1);
			tData.gameObject.SetActive(true);
			tData.transform.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			tData.transform.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			tData.transform.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
		}
		else if (partData.Name.Contains("Reactor"))
		{
			Transform tData = CManager.PartInfo.transform.GetChild(1).GetChild(2);
			tData.gameObject.SetActive(true);
			tData.transform.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			tData.transform.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			tData.transform.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			tData.transform.GetChild(3).GetComponent<Text>().text = "CAPACITY: "+partData.MaxCapacity.ToString();
			tData.transform.GetChild(4).GetComponent<Text>().text = "RECHARGE: "+partData.RechargeRate.ToString();
		}
		else if (partData.Name.Contains("Shield"))
		{
			Transform tData = CManager.PartInfo.transform.GetChild(1).GetChild(3);
			tData.gameObject.SetActive(true);
			tData.transform.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			tData.transform.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			tData.transform.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			tData.transform.GetChild(3).GetComponent<Text>().text = "CAPACITY: "+partData.MaxCapacity.ToString();
			tData.transform.GetChild(4).GetComponent<Text>().text = "RECHARGE: "+partData.RechargeRate.ToString();
			tData.transform.GetChild(5).GetComponent<Text>().text = "DRAIN: "+partData.Drain.ToString();
		}
		else if (partData.Name.Contains("Thruster"))
		{
			Transform tData = CManager.PartInfo.transform.GetChild(1).GetChild(4);
			tData.gameObject.SetActive(true);
			tData.transform.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			tData.transform.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			tData.transform.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			tData.transform.GetChild(3).GetComponent<Text>().text = "THRUST: "+partData.Thrust.ToString();
			tData.transform.GetChild(4).GetComponent<Text>().text = "TORQUE: "+partData.Torque.ToString();
		}
		else if (partData.Name.Contains("Weapon"))
		{
			Transform tData = CManager.PartInfo.transform.GetChild(1).GetChild(5);
			tData.gameObject.SetActive(true);
			tData.transform.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			tData.transform.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			tData.transform.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();	
			tData.transform.GetChild(3).GetComponent<Text>().text = "DAMAGE: "+partData.Damage.ToString();
			tData.transform.GetChild(4).GetComponent<Text>().text = "FIRERATE: "+partData.FireRate.ToString();
			tData.transform.GetChild(5).GetComponent<Text>().text = "DRAIN: "+partData.Drain.ToString();
		}
		else
		{
			Transform tData = CManager.PartInfo.transform.GetChild(1).GetChild(0);
			tData.gameObject.SetActive(true);
			tData.transform.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			tData.transform.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			tData.transform.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();		
		}
	}

}
