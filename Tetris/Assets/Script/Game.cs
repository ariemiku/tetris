using UnityEngine;
using System.Collections;

struct BLOCK{
	public int[,] square;
	public int leftBlock;
	public int rightBlock;
	public int underBlock;
	public int topBlock;

	// コンストラクタ
	public BLOCK(int leftBlock,int rightBlock,int underBlock,int topBlock)
	{            
		this.leftBlock = leftBlock;
		this.rightBlock = rightBlock;
		this.underBlock = underBlock;
		this.topBlock = topBlock;

		this.square = new int[5, 5];
	}            
};

public class Game : MonoBehaviour {

	BLOCK blockI = new BLOCK();
	BLOCK blockO = new BLOCK();
	BLOCK blockS = new BLOCK();
	BLOCK blockZ = new BLOCK();
	BLOCK blockJ = new BLOCK();
	BLOCK blockL = new BLOCK();
	BLOCK blockT = new BLOCK();

	BLOCK myBlock = new BLOCK();

	// 20*10
	int[,] map = new int[20,10];
	public int leftBlock = 0;
	public int rightBlock = 0;
	public int underBlock = 0;
	public int topBlock = 0;

	//int[,] myBlock = new int[5,5];

	public int nowBlock;
	public int nowUnder;

	public float x = 0.0f;
	public float y = 0.0f;

	public int moveX = 0;
	public int moveY = 0;

	GameObject cube;
	bool underFlag;
	bool rightFlag;
	bool leftFlag;


	bool gameoverFlag;

	public float timer = 0.0f;
	public float frontTime = 0.0f;


	private GameObject[,] m_aObject = new GameObject[20,10];
	//private GameObject[,] m_aO = new GameObject[2,2];

	void SetBlockType (int blockType)
	{
		blockType = Random.Range(0,7);

		switch(blockType)
		{
		case 0:
			myBlock = blockI;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		case 1:
			myBlock = blockO;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		case 2:
			myBlock = blockS;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		case 3:
			myBlock = blockZ;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		case 4:
			myBlock = blockJ;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		case 5:
			myBlock = blockL;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		case 6:
			myBlock = blockT;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
			
		default:
			break;
		}
	}

	// Use this for initialization
	void Start () {

		gameoverFlag = false;

		// ブロックの情報----------------------------------
		blockI.square = new int [,]{{0,0,1,0,0},
									{0,0,1,0,0},
									{0,0,1,0,0},
									{0,0,1,0,0},
									{0,0,0,0,0}};
		blockI.leftBlock = 2;
		blockI.rightBlock = 2;
		blockI.topBlock = 0;
		blockI.underBlock = 3;

		blockO.square = new int [,]{{0,0,0,0,0},
									{0,0,0,0,0},						
									{0,1,1,0,0},
									{0,1,1,0,0},
									{0,0,0,0,0}
									};
		blockO.leftBlock = 1;
		blockO.rightBlock = 2;
		blockO.topBlock = 2;
		blockO.underBlock = 3;

		blockS.square = new int [,]{{0,0,0,0,0},
									{0,0,0,0,0},
									{0,0,1,1,0},
									{0,1,1,0,0},
									{0,0,0,0,0}};
		blockS.leftBlock = 1;
		blockS.rightBlock = 3;
		blockS.topBlock = 2;
		blockS.underBlock = 3;
		
		blockZ.square = new int [,]{{0,0,0,0,0},
									{0,0,0,0,0},
									{0,1,1,0,0},
									{0,0,1,1,0},
									{0,0,0,0,0}};
		blockZ.leftBlock = 1;
		blockZ.rightBlock = 3;
		blockZ.topBlock = 2;
		blockZ.underBlock = 3;
		
		blockJ.square = new int [,]{{0,0,0,0,0},
									{0,0,1,0,0},	
									{0,0,1,0,0},
									{0,1,1,0,0},
									{0,0,0,0,0}};
		blockJ.leftBlock = 1;
		blockJ.rightBlock = 2;
		blockJ.topBlock = 1;
		blockJ.underBlock = 3;
		
		blockL.square = new int [,]{{0,0,0,0,0},
									{0,0,1,0,0},
									{0,0,1,0,0},
									{0,0,1,1,0},
									{0,0,0,0,0}};
		blockL.leftBlock = 2;
		blockL.rightBlock = 3;
		blockL.topBlock = 1;
		blockL.underBlock = 3;
		
		blockT.square = new int [,]{{0,0,0,0,0},
									{0,0,0,0,0},
									{0,0,1,0,0},
									{0,1,1,1,0},
									{0,0,0,0,0}};
		blockT.leftBlock = 1;
		blockT.rightBlock = 3;
		blockT.topBlock = 2;
		blockT.underBlock = 3;
		
		//-----------------------------------------------

		// ゲームオブジェクトの取得
		cube = GameObject.Find ("Cube");
		//Debug.Log (cube);

		// I型が落ちてきた場合
		/*
		underBlock = 3;		// 本来は0からだが仮で3から
		map [underBlock, 3] = 1;
		map [underBlock, 4] = 1;
		map [underBlock, 5] = 1;
		map [underBlock, 6] = 1;
		leftBlock = 3;
		rightBlock = 6;
		topBlock = 0;
		*/
		// 初期のブロックの設定(仮でIとする)
		/*
		for (int i=0; i<5; i++) 
		{
			for(int j=0;j<5;j++)
			{
				myBlock[i,j] = blockI[i,j];
			}
		}

		Debug.Log (cube.transform.position);
		*/

		//cube.transform.position = new Vector3 (0.0f,9.0f,0.0f);
		/*
		Debug.Log (cube.transform.position);
		cube.transform.position = new Vector3 (0.0f,9.0f,0.0f);
		Debug.Log (cube.transform.position);
		*/

		// 配列の初期化
		for (int i=0; i<20; i++) 
		{
			for(int j=0;j<10;j++)
			{
				map[i,j] = 0;
			}
		}

		leftBlock = 4;
		rightBlock = 0;
		underBlock = 0;
		topBlock = 0;

		x = -4;
		y = 9;

		moveX = 2;
		moveY = 0;


		// キューブを生成し、位置と色を設定して表示する
		for (int i=0; i<20; i++) 
		{
			for(int j=0;j<10;j++)
			{
				m_aObject[i,j] = GameObject.CreatePrimitive (PrimitiveType.Cube);
				m_aObject[i,j].transform.Translate (-8+j,9-i,0);
				Renderer renderer = m_aObject[i,j].GetComponent<Renderer> ();
				renderer.material = new Material (renderer.material);
				renderer.material.color = new Color (0.0f,0.0f,1.0f);
			}
		}

		underFlag = false;
		rightFlag = false;
		leftFlag = false;

		// 初期のブロックの設定(仮でIとする)
		nowBlock = 0;
		nowUnder = 0;
		switch(nowBlock)
		{
		case 0:
			myBlock = blockI;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;

		case 1:
			myBlock = blockO;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -2;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;

		case 2:
			myBlock = blockS;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -2;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;

		case 3:
			myBlock = blockZ;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -2;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;

		case 4:
			myBlock = blockJ;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;
		
		case 5:
			myBlock = blockL;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -1;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;

		case 6:
			myBlock = blockT;
			myBlock.topBlock = -5+myBlock.topBlock;
			myBlock.underBlock = -2;
			myBlock.leftBlock = myBlock.leftBlock + 2;
			myBlock.rightBlock = myBlock.rightBlock + 2;
			break;

		default:
			break;
		}
	}
	

	// Update is called once per frame
	void Update () {
		if (gameoverFlag == true) {
			// 押された瞬間の処理
			// spaceキーでシーンを切り替える
			if (Input.GetKeyDown (KeyCode.Space)) {
				Application.LoadLevel ("Title");
			}	
		} else {
			// 時間の取得
			timer += Time.deltaTime;

			// 削除処理
			for (int i=19; i>=0; i--) {
				int count = 0;
				for (int j=0; j<10; j++) {
					if (map [i, j] == 0) {
						break;
					}
					count += 1;
				}

				if (count == 10) {
					for (int j=0; j<10; j++) {
						map [i, j] = 0;
					}

					for (int a = i; a>0; a--) {
						for (int b=0; b<10; b++) {
							map [a, b] = map [a - 1, b];
						}
					}
				}
			}


			// 確認用
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				// 削除処理
				//Destroy(m_aObject[0,0]);
			}

			// ブロックを落下させる処理
			if ((timer - frontTime) > 0.5) {
				for (int i=0; i<20; i++) {
					for (int j=0; j<10; j++) {
						if (map [i, j] == 0) {
							// 色を変える
							Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.0f, 0.0f, 1.0f);
						}
						if (map [i, j] == 1) {
							// 色を変える
							Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.0f, 1.0f, 1.0f);
						}
					}
				}

				// 下にブロックがあるかどうか調べる
				if (myBlock.underBlock + 1 < 20 && nowUnder - 1 >= 0) {
					for (int i=0; myBlock.leftBlock+i<=myBlock.rightBlock; i++) {
						if (map [nowUnder - 1, myBlock.leftBlock + i] == 1) {
							underFlag = true;
							break;
						}
					}

					if(underFlag == true && myBlock.topBlock<0)
					{
						gameoverFlag = true;

						for (int i=0; i<5; i++) {
							for (int j=0; j<5; j++) {
								if (myBlock.square [4 - i, j] == 1 && nowUnder - i + moveY > 0) {
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - 1 - i + moveY, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (renderer.material);
									renderer.material.color = new Color (0.0f, 1.0f, 1.0f);
									
								}
							}
						}
					}
				}

				if (myBlock.underBlock < 20 && underFlag == false) {
					if (nowUnder < 5) {
						for (int i=0; i<=nowUnder; i++) {
							for (int j=0; j<5; j++) {
								if (myBlock.square [4 - i, j] == 1) {
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - i + moveY, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (renderer.material);
									renderer.material.color = new Color (1.0f, 0.0f, 1.0f);
								}
							}
						}
					} else {
						for (int i=0; i<5; i++) {
							for (int j=0; j<5; j++) {
								if (myBlock.square [4 - i, j] == 1) {
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - i + moveY, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (renderer.material);
									renderer.material.color = new Color (1.0f, 0.0f, 1.0f);
								
								}
							}
						}
					}

				} else {
					if(myBlock.topBlock<0)
						gameoverFlag = true;

					for (int i=0; i<5; i++) {
						for (int j=0; j<5; j++) {
							if (myBlock.square [4 - i, j] == 1 && (nowUnder - i + moveY)>0) {
								map [nowUnder - 1 - i + moveY, j + moveX] = 1;							
							}
						}
					}

					nowUnder = 0;
					// 次のブロックに合わせて値を変更する
					SetBlockType (nowBlock);
					moveX = 2;
					moveY = 0;
					underFlag = false;
					rightFlag = false;
					leftFlag = false;
				}

				nowUnder += 1;
				myBlock.underBlock += 1;
				myBlock.topBlock += 1;



				/*

			// 下にブロックがない時
			if(y>-10 && map[underBlock+1,leftBlock] ==0)
			{
				y -= 1.0f;
				underBlock +=1;
			}
			else
			{
				Debug.Log(underBlock);
				Debug.Log(leftBlock);

				map[underBlock,leftBlock] = 1;

				// ブロックがすでに積まれているところの色を変える
				for (int i=0; i<20; i++) 
				{
					for(int j=0;j<10;j++)
					{
						if(map[i,j] == 1)
						{
							// 色を変える
							Renderer renderer = m_aObject[i,j].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.0f,1.0f,1.0f);
						}
					}
				}

				y=9.0f;
				x=-4.0f;
				underBlock = 0;
				leftBlock = 4;
			}*/

				frontTime = timer;
			}



			// 右回転をrightShiftキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする(未)
			/*
		if (Input.GetKeyDown (KeyCode.RightShift)) 
		{
			print ("rightShift push");

			int[,] blockCopy = new int[5,5];
			for(int i=0;i<5;i++)
			{
				for(int j=0;j<5;j++)
				{
					blockCopy[i,j] = myBlock[4-j,i];
				}
			}
			// コピーに作った回転させたデータを移す
			for(int i=0;i<5;i++)
			{
				for(int j=0;j<5;j++)
				{
					myBlock[i,j] = blockCopy[i,j];
					blockCopy[i,j] = 0;

					// myBlockの確認用
					Debug.Log(myBlock[i,j]);
				}
			}
		}
*/


			// ブロックの位置を移動させる
			if (Input.GetKeyDown (KeyCode.D) && myBlock.topBlock >= 0) {
				/*
			if(x<1 && map[underBlock,leftBlock+1] ==0)
			{
				x+=1.0f;
				leftBlock +=1;
			}
*/

				// 右にブロックがあるかどうか調べる
				if (myBlock.rightBlock < 9 && myBlock.underBlock < 20) {
					for (int i=0; myBlock.underBlock-i>myBlock.topBlock; i++) {
						if (map [myBlock.underBlock - i, myBlock.rightBlock + 1] == 1) {
							rightFlag = true;
							break;
						}
					}
				}

				if (myBlock.rightBlock < 9 && rightFlag == false) {
					moveX += 1;
					myBlock.leftBlock += 1;
					myBlock.rightBlock += 1;
					if (leftFlag == true)
						leftFlag = false;
				}


				// ブロックの右移動
				// ブロックの下に積み上げたブロックがあった場合以外の処理
				/*
			if(rightBlock<9)
			{

				rightBlock += 1;

				map[0,rightBlock] = 1;
				map[0,leftBlock] = 0;

				leftBlock += 1;
			}*/
			}	
			if (Input.GetKeyDown (KeyCode.S) && myBlock.topBlock >= 0) {
				/*
			if(x>-8 && map[underBlock,leftBlock-1] ==0)
			{
				x-=1.0f;
				leftBlock -=1;
			}
			*/

				// 左にブロックがあるかどうか調べる
				if (myBlock.leftBlock > 0 && myBlock.underBlock < 20) {
					for (int i=0; myBlock.underBlock-i>myBlock.topBlock; i++) {
						if (map [myBlock.underBlock - i, myBlock.leftBlock - 1] == 1) {
							leftFlag = true;
							break;
						}
					}
				}

				if (myBlock.leftBlock > 0 && leftFlag == false) {
					moveX -= 1;
					myBlock.leftBlock -= 1;
					myBlock.rightBlock -= 1;
					if (rightFlag == true)
						rightFlag = false;
				}

				// ブロックの左移動
				// ブロックの下に積み上げたブロックがあった場合以外の処理
				/*
			if(leftBlock>0)
			{
				leftBlock -= 1;
				map[0,leftBlock] = 1;
				map[0,rightBlock] = 0;
				rightBlock -= 1;
			}*/
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				// 下にブロックがあるかどうか調べる
				if (myBlock.underBlock + 1 < 20) {
					for (int i=0; myBlock.leftBlock-i>=myBlock.rightBlock; i++) {
						if (map [myBlock.underBlock + 1, myBlock.leftBlock + i] == 1) {
							underFlag = true;
							Debug.Log ("NO");
							break;
						}
					}
				}

				/*
			if(myBlock.underBlock+1 < 20 && underFlag == false)
			{
				moveY+=1;
				myBlock.underBlock += 1;
				myBlock.topBlock += 1;
			}
*/

				/*
			if(y>-10 && map[underBlock+1,leftBlock] ==0)
			{
				y-=1.0f;
				underBlock +=1;
			}*/
				// ブロックの下移動(自分で)
				// ブロックの下に積み上げたブロックがあった場合以外の処理
				/*
			if(underBlock <19)
			{
				// 現在動かしているブロックの情報をコピーする
				// (回転操作を行ったときに更新するように変更すること)------
				int[,] copy = new int[4,4];
				for (int i=0; i<4; i++) 
				{
					for(int j=0;j<4;j++)
					{
						copy[i,j] = map[topBlock+i,leftBlock+j];
						map[topBlock+i,leftBlock+j] = 0;
					}
				}
				//---------------------------------------------------
				underBlock += 1;
				topBlock += 1;

				for (int i=0; i<4; i++) 
				{
					for(int j=0;j<4;j++)
					{
						map[topBlock+i,leftBlock+j] = copy[i,j];
					}
				}

			}*/
			}

			// ブロックの位置更新
			cube.transform.position = new Vector3 (10, y, 0.0f);

		}
	}
}
