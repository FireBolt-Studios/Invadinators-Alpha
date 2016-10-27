using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPartCard : MonoBehaviour {

	public PartData partData;
	public ProfileManager PManager;
	public MainDatabase mDB;

	public bool Animating;
	public float timer;
	public float speedTimer;
	public float glowTimer;
	public float speed;

	public Text NameText;
	public Transform cockpit;
	public Transform reactor;
	public Transform shield;
	public Transform weapon;
	public Transform thruster;
	public Transform armor;
	public Transform icon;
	public RectTransform glow;
	public Image flash;
	public Image okGlow;

	public void AddToCargo ()
	{
		PartType pType = IAPI.Database.DataUtility.GetPartType(partData.Type+"s",PManager.ActiveProfile);

		if (pType.Parts.Contains(partData))
		{
			pType.Parts[pType.Parts.IndexOf(partData)].Quantity += 1;
		}
		else
		{
			pType.Parts.Add(partData); 
		}

		//Destroy(gameObject);
	}

	void Awake ()
	{
		partData = IAPI.Database.GenerateUtility.GeneratePart("Armors",4,4,1,mDB);

		NameText.text = partData.Name;
		NameText.color = mDB.rarities[partData.Rarity].color;
		glow.GetComponent<Image>().color = mDB.rarities[partData.Rarity].color;
		flash.color = mDB.rarities[partData.Rarity].color;
		okGlow.color = mDB.rarities[partData.Rarity].color;

		SpriteData sData = IAPI.Database.DataUtility.GetSpriteData(partData.Sprite,mDB);
		icon.GetChild(0).GetComponent<Image>().sprite = sData.Base;
		icon.GetChild(1).GetComponent<Image>().sprite = sData.Detail;

		if (partData.Type == "Cockpit")
		{
			cockpit.gameObject.SetActive(true);
			cockpit.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			cockpit.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			cockpit.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
		}
		if (partData.Type == "Reactor")
		{
			reactor.gameObject.SetActive(true);
			reactor.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			reactor.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			reactor.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			reactor.GetChild(3).GetComponent<Text>().text = "CAPACITY: "+partData.MaxCapacity.ToString();
			reactor.GetChild(4).GetComponent<Text>().text = "RECHARGE: "+partData.RechargeRate.ToString();
		}
		if (partData.Type == "Shield")
		{
			shield.gameObject.SetActive(true);
			shield.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			shield.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			shield.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			shield.GetChild(3).GetComponent<Text>().text = "CAPACITY: "+partData.MaxCapacity.ToString();
			shield.GetChild(4).GetComponent<Text>().text = "RECHARGE: "+partData.RechargeRate.ToString();
			shield.GetChild(5).GetComponent<Text>().text = "DRAIN: "+partData.Drain.ToString();
		}
		if (partData.Type == "Weapon")
		{
			weapon.gameObject.SetActive(true);
			weapon.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			weapon.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			weapon.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			weapon.GetChild(3).GetComponent<Text>().text = "DAMAGE: "+partData.Damage.ToString();
			weapon.GetChild(4).GetComponent<Text>().text = "FIRE-RATE: "+partData.FireRate.ToString();
			weapon.GetChild(4).GetComponent<Text>().text = "DRAIN: "+partData.Drain.ToString();
		}
		if (partData.Type == "Thruster")
		{
			thruster.gameObject.SetActive(true);
			thruster.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			thruster.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			thruster.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
			thruster.GetChild(3).GetComponent<Text>().text = "THRUST: "+partData.Thrust.ToString();
			thruster.GetChild(4).GetComponent<Text>().text = "TORQUE: "+partData.Torque.ToString();
		}
		if (partData.Type == "Armor")
		{
			armor.gameObject.SetActive(true);
			armor.GetChild(0).GetComponent<Text>().text = "WORTH: "+partData.Worth.ToString();
			armor.GetChild(1).GetComponent<Text>().text = "SIZE: "+partData.size;
			armor.GetChild(2).GetComponent<Text>().text = "DURABILITY: "+partData.MaxDurability.ToString();
		}
		Animating = true;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Animating = true;
		}

		if (glowTimer >= 0.3f)
		{
			glow.sizeDelta = new Vector2(160,130);
			glowTimer = 0;
		}
		else if (glowTimer >= 0.2f && glowTimer < 0.3f)
		{
			glow.sizeDelta = new Vector2(170,140);
			glowTimer += Time.deltaTime;
		}
		else if (glowTimer >= 0.1f && glowTimer < 0.2f)
		{
			glow.sizeDelta = new Vector2(160,130);
			glowTimer += Time.deltaTime;
		}
		else
		{
			glow.sizeDelta = new Vector2(150,120);
			glowTimer += Time.deltaTime;
		}

		if (Animating)
		{
			if (speedTimer >= 0.5f)
			{
				speed -= 200;
				speedTimer = 0;
			}
			else
			{
				speedTimer += Time.deltaTime;
			}

			if (timer >= 2.5f)
			{
				Animating = false;
				timer = 0;
			}
			else
			{
				flash.color = new Color(flash.color.r,flash.color.g,flash.color.b,1-(timer/2.5f));
				transform.GetChild(0).Rotate(Vector3.up*speed*Time.deltaTime);
				timer += Time.deltaTime;
			}
		}
		else
		{
			transform.GetChild(0).eulerAngles = new Vector3(0,0,0);
			timer = 0;
			speedTimer = 0;
			speed = 1000;
		}
	}

}
