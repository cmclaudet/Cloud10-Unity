using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class playSound : MonoBehaviour {
    private AudioSource sound;
	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
	}
	
    public void play()
    {
        sound.Play();
    }
}
