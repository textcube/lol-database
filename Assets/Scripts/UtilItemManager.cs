using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class UtilItemManager : MonoBehaviour {
	GameObject utilItemPrefab;
	GameObject grid;
	public UtilItem utilItem;

	IEnumerator DelaySetupUtilGrid(){
		yield return new WaitForSeconds(0f);
		SetupUtilGrid();
	}

	void Start () {
		utilItem = null;
		utilItemPrefab = Resources.Load("Prefabs/UtilItemPrefab", typeof(GameObject)) as GameObject;
		grid = FindGameObject("Grid");
		StartCoroutine( DelaySetupUtilGrid() );
	}
	
	void SetupUtilGrid(){
		int i = 0;
		foreach(Dictionary<string,string> item in DataManager.Instance.items){
			GameObject instance = NGUITools.AddChild(grid, utilItemPrefab) as GameObject;
			UISprite sprite = instance.transform.FindChild("item").GetComponent<UISprite>();
			UtilItem uItem = instance.GetComponent<UtilItem>();
			sprite.spriteName = item["id"];
			instance.name = "Item_" + item["id"];
			uItem.idx = i;
			if (i==0) utilItem = uItem;
			i ++;
		}
		grid.GetComponent<UIGrid>().Reposition();
	}

	GameObject FindGameObject(string name) {
		GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
		foreach (GameObject pObject in pAllObjects) {
			if (pObject.name.Equals(name)) return pObject;
		}
		return null;
	}
	
	void GoHome(){
		Application.OpenURL("http://hompy.info/671");
	}

	void Update () {
	
	}
}
