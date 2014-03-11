using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChampionItem : MonoBehaviour {
	public int idx;
	UILabel mLabel;
	UITexture mTexture;
	LobbyManager lobbyManager;

	public void OnClick(){
		Dictionary<string,string> champion;
		champion = DataManager.Instance.champions[idx];
		mLabel.text = champion["name"];
		mTexture.material.SetTexture("_MainTex", Resources.Load("portraits/"+champion["id"]) as Texture);
		lobbyManager.championItem = this;
		lobbyManager.PlayMovie(3);
	}
	
	void Start () {
		lobbyManager = GameObject.Find ("LobbyManager").GetComponent<LobbyManager>();
		mLabel = GameObject.Find("Champion_Name_Label").GetComponent<UILabel>();
		mTexture = GameObject.Find("Champion_Portrait").GetComponent<UITexture>();
	}
	
	void Update () {
	
	}
}
