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
	BLOCK myBlock = new BLOCK();

	// 20*10
	int[,] map = new int[20,10];
	
	public int nowBlock;
	public int nowUnder;

	public int moveX = 0;
	public int moveY = 0;
	
	bool underFlag;
	bool rightFlag;
	bool leftFlag;
	bool gameoverFlag;

	public float timer = 0.0f;
	public float frontTime = 0.0f;

	public int score=0;
	public int scoreCount=0;

	int ghostUnder=19;
	int ghostLeft=0;

	private GameObject[,] m_aObject = new GameObject[20,10];

	void SetBlockType ()
	{
		nowBlock = Random.Range(0,7);

		switch(nowBlock)
		{
		case 0:
			myBlock.square = new int [,]{{0,0,1,0,0},
										{0,0,1,0,0},
										{0,0,1,0,0},
										{0,0,1,0,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -4;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 4;
			myBlock.rightBlock = 4;
			break;
			
		case 1:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,0,0,0},						
										{0,1,1,0,0},
										{0,1,1,0,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -2;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 4;
			break;
			
		case 2:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,0,0,0},
										{0,0,1,1,0},
										{0,1,1,0,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -2;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 5;
			break;
			
		case 3:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,0,0,0},
										{0,1,1,0,0},
										{0,0,1,1,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -2;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 5;
			break;
			
		case 4:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,1,0,0},	
										{0,0,1,0,0},
										{0,1,1,0,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -3;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 4;
			break;
			
		case 5:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,1,0,0},
										{0,0,1,0,0},
										{0,0,1,1,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -3;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 4;
			myBlock.rightBlock = 5;
			break;
			
		case 6:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,0,0,0},
										{0,0,1,0,0},
										{0,1,1,1,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -2;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 5;
			break;
			
		default:
			break;
		}
	}

	// Use this for initialization
	void Start () {

		gameoverFlag = false;

		// 配列の初期化
		for (int i=0; i<20; i++) 
		{
			for(int j=0;j<10;j++)
			{
				map[i,j] = 0;
			}
		}

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

		// 初期のブロックの設定
		nowUnder = 0;
		SetBlockType ();
		ghostLeft = myBlock.leftBlock;
	}
	

	// Update is called once per frame
	void Update () {
		// ゲームオーバーの時
		// spaceキーでシーンを切り替える
		if (gameoverFlag == true) {
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

			// ブロックを落下させる＆xボタンで落下させる処理
			if ((timer - frontTime) > 0.5 || Input.GetKeyDown (KeyCode.X)) {
				if((timer - frontTime) < 0.5)
					scoreCount +=1;

				for (int i=0; i<20; i++) {
					for (int j=0; j<10; j++) {
						Destroy(m_aObject[i,j]);
						m_aObject[i,j] = GameObject.CreatePrimitive (PrimitiveType.Cube);
						m_aObject[i,j].transform.Translate (-8+j,9-i,0);
						if (map [i, j] == 0) {
							// 色を変える
							Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.0f, 0.0f, 1.0f);
						}
						else if (map [i, j] == 1) {
							// 色を変える
							Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.0f, 1.0f, 1.0f);
						}


						if(ghostUnder > myBlock.underBlock)
						{
							// 色を変える
							Renderer renderer = m_aObject [ghostUnder, ghostLeft].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.8f, 0.8f, 0.8f);
						}
					}
				}

				// ゴースト表示のプログラミング(仮)
				for(int i=myBlock.leftBlock;i<=myBlock.rightBlock;i++)
				{
					for(int j=myBlock.underBlock+1;j<=19;j++)
					{
						if(map[j,i]==1 && ghostUnder > j)
						{
							ghostUnder=j-1;
							ghostLeft=i;
						}
					}
				}
				if(ghostUnder==19)
					ghostLeft=myBlock.leftBlock;

				// 下にブロックがあるかどうか調べる
				if (myBlock.underBlock + 1 < 20 && nowUnder - 1 >= 0) {

					for (int i=0; myBlock.leftBlock+i<=myBlock.rightBlock; i++) {
						if (map [nowUnder - 1, myBlock.leftBlock + i] == 1) {
							underFlag = true;
							break;
						}
					}

					// もうつめない場合
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
					SetBlockType ();
					moveX = 2;
					moveY = 0;
					underFlag = false;
					rightFlag = false;
					leftFlag = false;
					ghostUnder=19;
					ghostLeft=myBlock.leftBlock;

					if(scoreCount==0)
						score+=1;
					else if(scoreCount<18)
						score+=scoreCount;
					else
						score+=18;

					Debug.Log(score);
					scoreCount=0;
				}

				nowUnder += 1;
				myBlock.underBlock += 1;
				myBlock.topBlock += 1;

				frontTime = timer;

			}



			// 右回転をSpaceキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする
			if (Input.GetKeyDown (KeyCode.Space) && nowBlock != 1) 
			{

				int[,] blockCopy = new int[5,5];
				int right = 0;
				int left = 4;
				int top = 4;
				int under = 0;

				for(int i=0;i<5;i++)
				{
					for(int j=0;j<5;j++)
					{
						blockCopy[i,j] = myBlock.square[4-j,i];
						if(blockCopy[i,j]==1)
						{
							if(under<i)
								under=i;
							if(top>i)
								top=i;
							if(left>j)
								left=j;
							if(right<j)
								right=j;

						}
					}
				}

				// コピーに作った回転させたデータを移す
				if(left+moveX >=0 && right+moveX <=9)
				{
					for(int i=0;i<5;i++)
					{
						for(int j=0;j<5;j++)
						{
							myBlock.square[i,j]=0;
						}
					}
					for(int i=0;i<under;i++)
					{
						for(int j=0;j<5;j++)
						{
							myBlock.square[3-i,j]=blockCopy[under-i,j];
							blockCopy[under-i,j] = 0;
						}
					}
					myBlock.leftBlock=left+moveX;
					myBlock.rightBlock=right+moveX;
					myBlock.topBlock=myBlock.underBlock-(under-top);
				}
			}



			// ブロックの位置を移動させる
			if (Input.GetKeyDown (KeyCode.D) && myBlock.topBlock >= 0) {
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

			}	
			if (Input.GetKeyDown (KeyCode.S) && myBlock.topBlock >= 0) {
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

			}
		}
	}
}
