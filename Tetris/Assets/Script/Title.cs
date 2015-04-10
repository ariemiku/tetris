using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	private GameObject m_aObject;

	// Use this for initialization
	void Start () {

		// キューブを生成し、位置と色を設定して表示する
		m_aObject = GameObject.CreatePrimitive (PrimitiveType.Cube);
		m_aObject.transform.Translate (2,2,0);
		Renderer renderer = m_aObject.GetComponent<Renderer> ();
		renderer.material = new Material (renderer.material);
		renderer.material.color = new Color (0.0f,0.0f,1.0f);
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
