using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class RuneItemManager : MonoBehaviour {
	GameObject runeItemPrefab;
	GameObject grid;
	public RuneItem runeItem;

	IEnumerator DelaySetupUtilGrid(){
		yield return new WaitForSeconds(0f);
		SetupUtilGrid();
	}

	void Start () {
		runeItem = null;
		runeItemPrefab = Resources.Load("Prefabs/RuneItemPrefab", typeof(GameObject)) as GameObject;
		grid = FindGameObject("Grid");
		StartCoroutine( DelaySetupUtilGrid() );
	}
	
	void SetupUtilGrid(){
		int i = 0;
		foreach(Dictionary<string,string> item in DataManager.Instance.runes){
			GameObject instance = NGUITools.AddChild(grid, runeItemPrefab) as GameObject;
			UISprite sprite = instance.transform.FindChild("item").GetComponent<UISprite>();
			RuneItem rItem = instance.GetComponent<RuneItem>();
			sprite.spriteName = item["id"];
			instance.name = "Item_" + item["id"];
			rItem.idx = i;
			if (i==0) runeItem = rItem;
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
