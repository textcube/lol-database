using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	UILabel label;

	void Start () {
		label = GetComponentInChildren<UILabel>();
	}
	
	void OnClick() {
		Application.LoadLevel(label.text + "Scene");
	}
	
	void Update () {
	
	}
}
