using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class LobbyManager : MonoBehaviour {
	GameObject championItemPrefab;
	GameObject grid;
	public ChampionItem championItem;

	MovieTexture movieTexture;
	AudioClip movieAudioClip;
	AudioSource movieAudioSource;

	GameObject mMovie;

	IEnumerator DelaySetupChampionGrid(){
		yield return new WaitForSeconds(0f);
		SetupChampionGrid();
	}

	void Start () {
		mMovie = GameObject.Find("Champion_Movie");
		championItem = null;
		championItemPrefab = Resources.Load("Prefabs/ChampionItemPrefab", typeof(GameObject)) as GameObject;
		grid = FindGameObject("Grid");
		StartCoroutine( DelaySetupChampionGrid() );
	}
	
	IEnumerator DelayPlayMovie(){
		yield return new WaitForSeconds(1f);
		championItem.OnClick();
	}

	void SetupChampionGrid(){
		int i = 0;
		foreach(Dictionary<string,string> item in DataManager.Instance.champions){
			GameObject instance = NGUITools.AddChild(grid, championItemPrefab) as GameObject;
			UISprite sprite = instance.transform.FindChild("champ").GetComponent<UISprite>();
			ChampionItem cItem = instance.GetComponent<ChampionItem>();
			sprite.spriteName = item["id"];
			instance.name = "Item_" + item["id"];
			cItem.idx = i;
			if (i==0) championItem = cItem;
			i ++;
		}
		grid.GetComponent<UIGrid>().Reposition();
		//StartCoroutine(DelayPlayMovie());
	}

	public void StopMovie(){
		movieTexture.Stop();
		movieAudioSource.Stop();
	}

	IEnumerator Download( WWW www ) {
		yield return www;
		while (!www.isDone)
		{
		}
	}

	IEnumerator PlayMovieWWW(string url) {
		WWW www = new WWW(url);
		while(!www.movie.isReadyToPlay)
			yield return www;
		try {          
			if (www.error != null)
				Debug.Log("WWW Error - " + www.error);
			else {
				movieTexture = www.movie;
				Debug.Log("Movie is playing!");
				movieAudioClip = movieTexture.audioClip;
				movieAudioSource.clip = movieAudioClip;
				mMovie.renderer.material.mainTexture = movieTexture;
				movieTexture.Play();
				movieAudioSource.Play();
			}
		}
		catch(Exception ex){
			Debug.Log("EarthRotator.DownloadFile(): Exception - " + ex.Message);
			Exception innerEx = ex.InnerException;
			while (innerEx != null) {
				Debug.Log("EarthRotator.DownloadFile(): Inner Exception - " + innerEx.Message);
				innerEx = innerEx.InnerException;
			}
	    }
	}

	public void PlayMovie(int idx){
		if (movieTexture) {
			StopMovie();
			Destroy( movieAudioSource );
		}
		movieAudioSource = gameObject.AddComponent<AudioSource>();
		string movieTexturePath = "ogv/" + string.Format("{0:D4}", int.Parse( DataManager.Instance.champions[ championItem.idx ]["id"]) ) + "_0" + (idx+2);
		string url = "http://yourdomain.com" + movieTexturePath + ".ogv";
		StartCoroutine( PlayMovieWWW(url) );
	}

	GameObject FindGameObject(string name) {
		GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
		foreach (GameObject pObject in pAllObjects) {
			if (pObject.name.Equals(name)) return pObject;
		}
		return null;
	}
	
	void OnClickQ(){
		PlayMovie(0);
	}

	void OnClickW(){
		PlayMovie(1);
	}

	void OnClickE(){
		PlayMovie(2);
	}

	void OnClickR(){
		PlayMovie(3);
	}
	
	void GoHome(){
		Application.OpenURL("http://hompy.info/671");
	}

	void Update () {
	
	}
}
