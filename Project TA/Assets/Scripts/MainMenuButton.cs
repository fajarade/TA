using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour {

	public void Button1(){
		Application.LoadLevel("Ujian1");
	}

	public void Button2(){
		Application.LoadLevel("Ujian2");
	}

	public void Button3(){
		Application.LoadLevel("Ujian3");
	}

	public void Button4(){
		Application.LoadLevel("Ujian4");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
