using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuneItem : MonoBehaviour {
	public int idx;
	UILabel mLabel, mDesc;
	RuneItemManager runeItemManager;
	UISprite mSprite;

	public void OnClick(){
		Dictionary<string,string> item;
		item = DataManager.Instance.runes[idx];
		mLabel.text = item["name"];
		mDesc.text = item["desc"];
		runeItemManager.runeItem = this;
		mSprite.spriteName = item["id"];
	}
	
	void Start () {
		runeItemManager = GameObject.Find ("RuneItemManager").GetComponent<RuneItemManager>();
		mLabel = GameObject.Find("Item_Name_Label").GetComponent<UILabel>();
		mDesc = GameObject.Find("Item_Desc_Label").GetComponent<UILabel>();
		mSprite = GameObject.Find("Item_Portrait").GetComponent<UISprite>();
	}
	
	void Update () {
	
	}
}
