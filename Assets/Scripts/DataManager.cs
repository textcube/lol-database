using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class DataManager : MonoBehaviour {
	private SQLiteDB db = null;
	bool dbOk = false;
	public bool isLobbyStartSecondTime = false;
    private static DataManager s_instance = null;
	
	public List<Dictionary<string, string>> champions;
	public List<Dictionary<string, string>> items;
	public List<Dictionary<string, string>> abilities;
	public List<Dictionary<string, string>> runes;
	public List<Dictionary<string, string>> stats;
	
	public static DataManager Instance {
        get {
            if (null == s_instance) {
                s_instance = FindObjectOfType(typeof(DataManager)) as DataManager;
                if (null == s_instance) {
                    Debug.Log("Fail to get Manager Instance");
                }
            }
            return s_instance;
        }
    }
	
    void OnApplicationQuit() {
        s_instance = null;
    }
 
	void Awake () {
		DontDestroyOnLoad(this);
		InitDatabase();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Download( WWW www ) {
		yield return www;
		while (!www.isDone)
		{
			
		}
	}
	
	void ReadDatabase(){
		ReadChampions();
		//ReadAbilities();
		ReadItems();
		ReadRunes();
		//ReadStats();
	}

	void InitDatabase(){
		byte[] bytes = null;
		db = new SQLiteDB();
		string dbfilename = "LOLDatabase.sqlite";
		string filename = Application.persistentDataPath + "/" + dbfilename;
		/*
		if (File.Exists(filename)) {
			db.Open(filename);
			dbOk = true;
			ReadDatabase();
			return;
		}
		 */
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
			string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename;
			WWW www = new WWW(dbpath);
			Download(www);
			bytes = www.bytes;
		#elif UNITY_WEBPLAYER
			string dbpath = "StreamingAssets/" + dbfilename;
			Debug.Log("dbpath:" + dbpath);
			WWW www = new WWW(dbpath);
			Download(www);
			bytes = www.bytes;
		#elif UNITY_IPHONE
			string dbpath = Application.dataPath + "/Raw/" + dbfilename;
			try{	
				using ( FileStream fs = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
					bytes = new byte[fs.Length];
					fs.Read(bytes,0,(int)fs.Length);
				}			
			} catch (Exception e){
			}
		#elif UNITY_ANDROID
			string dbpath = Application.streamingAssetsPath + "/" + dbfilename;
			WWW www = new WWW(dbpath);
			Download(www);
			bytes = www.bytes;
		#endif
		if ( bytes != null ) {
			try {	
				using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) ) {
					fs.Write(bytes,0,bytes.Length);
				}
				db.Open(filename);
				dbOk = true;;
				ReadDatabase();
				
			} catch (Exception e){
			}
		}
	}
	
	void ReadChampions(){
		champions = new List<Dictionary<string, string>> ();
		string querySelect = "select * from Champions order by id;";
		if(!dbOk) return;
		SQLiteQuery qr;
		qr = new SQLiteQuery(db, querySelect);
		while(qr.Step()){
			Dictionary<string, string> item = new Dictionary<string, string>();
			int id = qr.GetInteger("id");
			string kname = qr.GetString ("kname");
			string name = qr.GetString ("name");
			string title = qr.GetString ("title");
			string lore = qr.GetString ("lore");
			int attack = qr.GetInteger("attack");
			int health = qr.GetInteger("health");
			int difficulty = qr.GetInteger("difficulty");
			int spells = qr.GetInteger("spells");
			string icon = id + "";
			item.Add("id", id.ToString());
			item.Add("kname", kname);
			item.Add ("name", name);
			item.Add ("title", title);
			item.Add ("lore", lore);
			item.Add ("attack", attack.ToString());
			item.Add ("health", health.ToString());
			item.Add ("difficulty", difficulty.ToString());
			item.Add ("spells", spells.ToString());
			item.Add ("icon", icon);
			champions.Add (item);
		}
		qr.Release();
	}

	void ReadAbilities(){
		abilities = new List<Dictionary<string, string>> ();
		string querySelect = "select * from Abilities order by champion;";
		if(!dbOk) return;
		SQLiteQuery qr;
		qr = new SQLiteQuery(db, querySelect);
		while(qr.Step()){
			Dictionary<string, string> item = new Dictionary<string, string>();
			int champion = qr.GetInteger("champion");
			int skill = qr.GetInteger("skill");
			//string kname = qr.GetString ("kname");
			string name = qr.GetString ("name");
			string desc = qr.GetString ("desc");
			string effect = qr.GetString ("effect");
			string icon = champion + "-" + skill;
			item.Add("champion", champion.ToString());
			item.Add("skill", skill.ToString());
			//item.Add("kname", kname);
			item.Add ("name", name);
			item.Add ("desc", desc);
			item.Add ("effect", effect);
			item.Add ("icon", icon);
			abilities.Add (item);
		}
		qr.Release();
	}

	void ReadItems(){
		items = new List<Dictionary<string, string>> ();
		string querySelect = "select * from Items order by id;";
		if(!dbOk) return;
		SQLiteQuery qr;
		qr = new SQLiteQuery(db, querySelect);
		while(qr.Step()){
			Dictionary<string, string> item = new Dictionary<string, string>();
			int id = qr.GetInteger("id");
			string kname = qr.GetString ("kname");
			string name = qr.GetString ("name");
			string kdesc = qr.GetString ("kdesc");
			string icon = id + "";
			int gold = qr.GetInteger("gold");
			item.Add("id", id.ToString());
			item.Add("kname", kname);
			item.Add ("name", name);
			item.Add ("kdesc", kdesc);
			item.Add ("icon", icon);
			item.Add ("gold", gold.ToString());
			items.Add (item);
		}
		qr.Release();
	}

	void ReadRunes(){
		runes = new List<Dictionary<string, string>> ();
		string querySelect = "select * from Runes order by id;";
		if(!dbOk) return;
		SQLiteQuery qr;
		qr = new SQLiteQuery(db, querySelect);
		while(qr.Step()){
			Dictionary<string, string> item = new Dictionary<string, string>();
			int id = qr.GetInteger("id");
			string name = qr.GetString ("name");
			string desc = qr.GetString ("desc");
			string icon = id + "";
			item.Add("id", id.ToString());
			item.Add ("name", name);
			item.Add ("desc", desc);
			item.Add ("icon", icon);
			runes.Add (item);
		}
		qr.Release();
	}

	void ReadStats(){
		stats = new List<Dictionary<string, string>> ();
		string querySelect = "select * from Stats order by id;";
		if(!dbOk) return;
		SQLiteQuery qr;
		qr = new SQLiteQuery(db, querySelect);
		while(qr.Step()){
			Dictionary<string, string> item = new Dictionary<string, string>();
			int champion = qr.GetInteger("champion");
			string name = qr.GetString ("name");
			string modifier = qr.GetString ("modifier");
			item.Add("champion", champion.ToString());
			item.Add ("name", name);
			item.Add ("modifier", modifier);
			stats.Add (item);
		}
		qr.Release();
	}
}
