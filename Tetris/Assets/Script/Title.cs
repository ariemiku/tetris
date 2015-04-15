using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	//private GameObject m_aObject;
	private GameObject[,] m_aObject = new GameObject[2,2];

	// Use this for initialization
	void Start () {
		for (int i=0; i<2; i++) 
		{
			for(int j=0;j<2;j++)
			{
				// キューブを生成し、位置と色を設定して表示する
				m_aObject[i,j] = GameObject.CreatePrimitive (PrimitiveType.Cube);
				m_aObject[i,j].transform.Translate (5+i,5+j,0);
				Renderer renderer = m_aObject[i,j].GetComponent<Renderer> ();
				renderer.material = new Material (renderer.material);
				renderer.material.color = new Color (0.0f,(float)i*j,1.0f);
			}
		}
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
