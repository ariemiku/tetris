using UnityEngine;
using System.Collections;

using UnityEngine.UI;

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
	public Text scoreText;
	public Text gameoverText;

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
	public float downTime = 0.5f;

	public int score=0;
	public int scoreCount=0;
	public int scoreLineCount=0;

	int ghostUnder=19;
	int ghostLeft=0;

	int leftSpace=0;
	int rightSpace=0;

	private GameObject[,] m_aObject = new GameObject[20,10];
	

	void SetBlockType ()
	{
		nowBlock = Random.Range(0,7);

		switch(nowBlock)
		{
		case 0:
			myBlock.square = new int [,]{{0,1,0,0,0},
										 {0,1,0,0,0},
										 {0,1,0,0,0},
										 {0,1,0,0,0},
										 {0,0,0,0,0}};
			myBlock.topBlock = -4;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 3;

			leftSpace=0;
			rightSpace=0;

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

			leftSpace=0;
			rightSpace=0;

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

			leftSpace=0;
			rightSpace=1;

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

			leftSpace=1;
			rightSpace=0;

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

			leftSpace=0;
			rightSpace=0;

			break;
			
		case 5:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,1,0,0,0},
										 {0,1,0,0,0},
										 {0,1,1,0,0},
										 {0,0,0,0,0}};
			myBlock.topBlock = -3;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 4;

			leftSpace=0;
			rightSpace=0;

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

			leftSpace=0;
			rightSpace=0;

			break;
			
		default:
			break;
		}
	}

	void Ghost()
	{
		for (int i=0; i<20; i++) {
			for (int j=0; j<10; j++) {
				if(map[i,j]==2)
					map[i,j]=0;
			}
		}
		ghostUnder=19;
		ghostLeft=myBlock.leftBlock;
		
		
		for(int i=myBlock.leftBlock;i<=myBlock.rightBlock;i++)
		{
			for(int j=myBlock.underBlock+1;j<=19;j++)
			{
				if(map[j,i]==1 && j-1<ghostUnder)
				{
					ghostUnder=j-1;
				}
			}
		}

		if(ghostUnder+1 <=19)
		{
			// 左下 右下に空白がある場合
			if(leftSpace!=0 && rightSpace!=0)
			{
				if(map[ghostUnder+1,myBlock.leftBlock+1]==0)
					ghostUnder+=1;
			}
			// 左下に1つ空白がある場合
			else if(leftSpace==1)
			{
				if(myBlock.rightBlock - myBlock.leftBlock == 1 &&
				   map[ghostUnder+1,myBlock.rightBlock]==0)
				{
					ghostUnder+=1;
				}
				else if(myBlock.rightBlock - myBlock.leftBlock == 2 &&
				        map[ghostUnder+1,myBlock.leftBlock+1]==0 &&
				        map[ghostUnder+1,myBlock.rightBlock]==0)
				{
					ghostUnder+=1;
				}
			}
			// 左下に2つ空白がある場合
			else if(leftSpace==2)
			{
				if(ghostUnder+2 <=19 && map[ghostUnder+1,myBlock.rightBlock]==0 &&
				   map[ghostUnder+2,myBlock.rightBlock]==0)
				{
					ghostUnder+=2;
				}
				else if(map[ghostUnder+1,myBlock.rightBlock]==0)
				{
					ghostUnder+=1;
				}
			}
			// 右下に1つ空白がある場合
			else if(rightSpace==1)
			{	
				if(myBlock.rightBlock - myBlock.leftBlock == 1 &&
				   map[ghostUnder+1,myBlock.leftBlock]==0)
				{
					ghostUnder+=1;
				}
				else if(myBlock.rightBlock - myBlock.leftBlock == 2 &&
				        map[ghostUnder+1,myBlock.leftBlock+1]==0 &&
				        map[ghostUnder+1,myBlock.leftBlock]==0)
				{
					ghostUnder+=1;
				}
			}
			// 右下に2つ空白がある場合
			else if(rightSpace==2)
			{
				if(ghostUnder+2 <=19 &&	map[ghostUnder+1,myBlock.leftBlock]==0 &&
				   map[ghostUnder+2,myBlock.leftBlock]==0)
				{
					ghostUnder+=2;
				}
				else if(map[ghostUnder+1,myBlock.leftBlock]==0)
				{
					ghostUnder+=1;
				}
			}
		}

		for(int i=0;i<4;i++)
		{
			for(int j=1;j<5;j++)
			{
				if(ghostUnder-(3-i)>=0 && myBlock.leftBlock-1+j<=9 && myBlock.square[i,j]==1 &&map[ghostUnder-(3-i),myBlock.leftBlock-1+j]!=1)
					map[ghostUnder-(3-i),myBlock.leftBlock-1+j]=2;
			}
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
		ghostLeft = 0;


		scoreText = GameObject.Find ("Canvas/TextScore").GetComponent<Text>();
		scoreText.text = "Score\n"+score;
		gameoverText = GameObject.Find ("Canvas/TextGameover").GetComponent<Text> ();
		gameoverText.text = "";
	}
	

	// Update is called once per frame
	void Update () {
		Resources.UnloadUnusedAssets ();
		// ゲームオーバーの時
		// spaceキーでシーンを切り替える
		if (gameoverFlag == true) {
			gameoverText.text = "GAMEOVER\npush space";

			if (Input.GetKeyDown (KeyCode.Space)) {
				Application.LoadLevel ("Title");
			}	
		} else {
			// 時間の取得
			timer += Time.deltaTime;

			// テトリミノの速度を経過した時間で変化させる
			if(timer > 180.0f)
				downTime=0.3f;
			else if(timer > 240.0f)
				downTime=0.2f;

			// ブロックを落下させる＆xボタンで落下させる処理
			if ((timer - frontTime) > downTime || Input.GetKeyDown (KeyCode.X)) {
				if((timer - frontTime) < downTime)
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

						else if (map [i, j] == 2) {
							// 色を変える
							Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
							renderer.material = new Material (renderer.material);
							renderer.material.color = new Color (0.8f, 0.8f, 0.8f);
						}
					}
				}

				// ゴースト表示のプログラミング
				if(myBlock.leftBlock!=ghostLeft)
				{
					Ghost();
				}


				// 下にブロックがあるかどうか調べる
				if (myBlock.underBlock + 1 < 20 && nowUnder - 1 >= 0) {
					if(myBlock.underBlock==0)
					{
						for (int i=myBlock.leftBlock;i<=myBlock.rightBlock; i++) {
							if (map [myBlock.underBlock, i] == 1) {
								underFlag = true;
								break;
							}
						}
					}

					// もうつめない場合
					if(underFlag == true && myBlock.underBlock == ghostUnder+1 && myBlock.topBlock<0)
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

				if (myBlock.underBlock < 20 && underFlag == false && myBlock.underBlock != ghostUnder+1) {
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
					} 
					else {
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

				}
				else {
					if(myBlock.topBlock<0)
						gameoverFlag = true;

					for (int i=0; i<5; i++) {
						for (int j=0; j<5; j++) {
							if (myBlock.square [4 - i, j] == 1 && (nowUnder - i + moveY)>0) {
								map [nowUnder - 1 - i + moveY, j + moveX] = 1;							
							}
						}
					}


					// 削除処理
					for (int i=19; i>=0; i--) {
						int count = 0;
						for (int j=0; j<10; j++) {
							if (map [i, j] == 0) {
								break;
							}
							else if(map [i, j] == 1)
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
							scoreLineCount+=1;
							i++;
						}
					}

					if(scoreCount==0)
						score+=1;
					else if(scoreCount<18)
					{
						if(myBlock.underBlock < scoreCount)
							score+=myBlock.underBlock;
						else
							score+=scoreCount;
					}
					else
					{
						if(myBlock.underBlock < scoreCount)
							score+=myBlock.underBlock;
						else
							score+=18;
					}
					switch(scoreLineCount)
					{
					case 1:
						score+=40;
						scoreLineCount=0;
						break;

					case 2:
						score+=100;
						scoreLineCount=0;
						break;

					case 3:
						score+=300;
						scoreLineCount=0;
						break;

					case 4:
						score+=1200;
						scoreLineCount=0;
						break;

					default:
						scoreLineCount=0;
						break;
					}

					scoreCount=0;

					for (int i=0; i<20; i++) {
						for (int j=0; j<10; j++) {
							if(map[i,j]==2)
								map[i,j]=0;
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
					ghostLeft=0;

					scoreText.text = "Score\n"+score;
				}

				nowUnder += 1;
				myBlock.underBlock += 1;
				myBlock.topBlock += 1;

				frontTime = timer;

			}



			// 右回転をSpaceキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする
			if (Input.GetKeyDown (KeyCode.Space) && nowBlock != 1 && ghostUnder > myBlock.underBlock) 
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


				for(int i=0;i<2;i++)
				{
					for(int j=4;j>=0;j--)
					{
						//Debug.Log(myBlock.square[j,i]);
						if(myBlock.square[j,i]==1 && j==3)
						{
							leftSpace=0;
							break;
						}
						else if(myBlock.square[j,i]==1)
						{
							leftSpace=3-j;
							break;
						}
					}
				}
				rightSpace=0;
				bool rightSpaceFlag=false;
				for(int i=4;i>1;i--)
				{
					for(int j=4;j>=0;j--)
					{
						if(myBlock.square[j,i]==1 && j==3)
						{
							rightSpace=0;
							rightSpaceFlag=true;
							break;
						}
						else if(myBlock.square[j,i]==1)
						{
							rightSpace=3-j;
							rightSpaceFlag=true;
							break;
						}
					}
					if(rightSpaceFlag==true)
						break;
				}

				Ghost();
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
