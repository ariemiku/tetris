using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// 押された瞬間の処理
		// spaceキーでシーンを切り替える
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Tetris");
		}
	}
}
