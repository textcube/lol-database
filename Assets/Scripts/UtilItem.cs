using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilItem : MonoBehaviour {
	public int idx;
	UILabel mLabel, mDesc;
	UtilItemManager utilItemManager;
	UISprite mSprite;

	public void OnClick(){
		Dictionary<string,string> item;
		item = DataManager.Instance.items[idx];
		mLabel.text = item["name"] + " - " + item["gold"] + " Gold \n" + item["kname"];
		mDesc.text = item["kdesc"];
		utilItemManager.utilItem = this;
		mSprite.spriteName = item["id"];
	}
	
	void Start () {
		utilItemManager = GameObject.Find ("UtilItemManager").GetComponent<UtilItemManager>();
		mLabel = GameObject.Find("Item_Name_Label").GetComponent<UILabel>();
		mDesc = GameObject.Find("Item_Desc_Label").GetComponent<UILabel>();
		mSprite = GameObject.Find("Item_Portrait").GetComponent<UISprite>();
	}
	
	void Update () {
	
	}
}
