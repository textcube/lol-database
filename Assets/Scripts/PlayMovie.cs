using UnityEngine;
using System.Collections;
public class PlayMovie : MonoBehaviour {
	MovieTexture movieTexture;
	AudioClip movieAudioClip;
	AudioSource movieAudioSource;
	void Start () {
		/*
		movieAudioSource = gameObject.AddComponent<AudioSource>();
		string movieTexturePath = "ogv/0007_05";
		movieTexture = (MovieTexture)Resources.Load( movieTexturePath, typeof( MovieTexture ));
		renderer.material.mainTexture = movieTexture;
		//movieTexture = renderer.material.mainTexture as MovieTexture;
		movieAudioClip = movieTexture.audioClip;
		movieAudioSource.clip = movieAudioClip;
		movieTexture.Play();
		movieAudioSource.Play();
		*/
		movieTexture = renderer.material.mainTexture as MovieTexture;
	}
	
	void OnClick(){
		Debug.Log ("OnClick");
		movieTexture = renderer.material.mainTexture as MovieTexture;
		movieTexture.Stop();
		movieTexture.Play();
	}
	
	void Update(){
		if (movieTexture.isReadyToPlay && !movieTexture.isPlaying) {
			movieTexture.Play();
		}
	}
}
