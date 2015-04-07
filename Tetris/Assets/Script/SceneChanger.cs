using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	//public string nextSceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetButtonDown("submit"))
		{
			//Application.LoadedLevel("Tetris");

		}*/

		// 押された瞬間の処理
		if(Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("push_A");
			Application.LoadLevel("Tetris");
		}

		if(Input.GetKeyDown(KeyCode.B))
		{
			Debug.Log("push_A");
			Application.LoadLevel("Tetris");
		}
		
		
	}
}
