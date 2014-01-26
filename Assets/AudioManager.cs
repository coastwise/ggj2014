using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip explosion;
	public AudioListener audioListener;
	public AudioSource sound_source;

	// Use this for initialization
	void Awake () {
		explosion = (AudioClip)Resources.Load("explosion");
		sound_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playExplosion(){
		sound_source.clip = explosion;
		sound_source.Play();
	}
}
