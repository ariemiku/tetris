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
	BLOCK nextBlock = new BLOCK();

	// 20*10
	int[,] map = new int[20,10];
	
	bool underFlag;
	bool rightFlag;
	bool leftFlag;
	bool gameoverFlag;

	public Material nowColor;
	public Material nextColor;
	public Material backColor;
	public Material gost;
	public Material red;
	public Material blue;
	public Material lightBlue;
	public Material yello;
	public Material yelloGreen;
	public Material purple;
	public Material orange;

	public float timer = 0.0f;
	public float frontTime = 0.0f;
	public float downTime = 0.5f;

	public int score=0;
	public int scoreLineCount=0;
	public int nextScore=0;
	public int frontUnder=1;

	public int ghostUnder=19;
	public int ghostLeft=0;
	
	public int leftSpace=0;
	public int rightSpace=0;
	public int nowBlock;
	public int nowUnder;
	public int next;
	
	public int moveX = 0;

	private GameObject[,] m_aObject = new GameObject[20,10];
	private GameObject[,] m_nextTetrimino = new GameObject[5, 5];

	float downKeyStartTime = 0.0f;
	bool downKeyStart;
	float rigidTime = 0.1f;

	// ブロックの種類をランダムで選択し、セットする関数
	void SetBlockType ()
	{
		nowBlock = next;
		next = Random.Range(0,7);
		Debug.Log ("NEXT:"+next);

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

			leftSpace=0;
			rightSpace=0;

			nowColor = lightBlue;

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

			nowColor = yello;

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

			nowColor = yelloGreen;

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

			nowColor = red;

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

			nowColor = blue;

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

			leftSpace=0;
			rightSpace=0;

			nowColor = orange;

			break;
			
		case 6:
			myBlock.square = new int [,]{{0,0,0,0,0},
										{0,0,0,0,0},
										{0,1,1,1,0},
										{0,0,1,0,0},
										{0,0,0,0,0}};
			myBlock.topBlock = -2;
			myBlock.underBlock = -1;
			myBlock.leftBlock = 3;
			myBlock.rightBlock = 5;

			leftSpace=1;
			rightSpace=1;

			nowColor = purple;

			break;
			
		default:
			break;
		}

		SetNext ();
	}

	// ネクストの設定
	void SetNext () {
		switch(next)
		{
		case 0:
			nextBlock.square = new int [,]{{0,0,1,0,0},
				{0,0,1,0,0},
				{0,0,1,0,0},
				{0,0,1,0,0},
				{0,0,0,0,0}};
			
			nextColor = lightBlue;
			
			break;
			
		case 1:
			nextBlock.square = new int [,]{{0,0,0,0,0},
				{0,0,0,0,0},						
				{0,1,1,0,0},
				{0,1,1,0,0},
				{0,0,0,0,0}};
			
			nextColor = yello;
			
			break;
			
		case 2:
			nextBlock.square = new int [,]{{0,0,0,0,0},
				{0,0,0,0,0},
				{0,0,1,1,0},
				{0,1,1,0,0},
				{0,0,0,0,0}};
			
			nextColor = yelloGreen;
			
			break;
			
		case 3:
			nextBlock.square = new int [,]{{0,0,0,0,0},
				{0,0,0,0,0},
				{0,1,1,0,0},
				{0,0,1,1,0},
				{0,0,0,0,0}};
			
			nextColor = red;
			
			break;
			
		case 4:
			nextBlock.square = new int [,]{{0,0,0,0,0},
				{0,0,1,0,0},	
				{0,0,1,0,0},
				{0,1,1,0,0},
				{0,0,0,0,0}};
			
			nextColor = blue;
			
			break;
			
		case 5:
			nextBlock.square = new int [,]{{0,0,0,0,0},
				{0,0,1,0,0},
				{0,0,1,0,0},
				{0,0,1,1,0},
				{0,0,0,0,0}};
			
			nextColor = orange;
			
			break;
			
		case 6:
			nextBlock.square = new int [,]{{0,0,0,0,0},
				{0,0,0,0,0},
				{0,1,1,1,0},
				{0,0,1,0,0},
				{0,0,0,0,0}};
			
			nextColor = purple;
			
			break;
			
		default:
			break;
		}

		for (int y = 0; y < 5; y++) {
			for(int x = 0; x < 5; x++) {
				m_nextTetrimino[y, x].transform.position = new Vector3(5.75f + x, 2 - y, 0);
				Renderer renderer = m_nextTetrimino[y, x].GetComponent<Renderer> ();
				if(nextBlock.square[y, x] == 1) {
					renderer.material = new Material (nextColor);
				}
				else if(nextBlock.square[y, x] == 0) {
					renderer.material = new Material (backColor);
				}
			}
		}
	}

	// ゴーストの当たり判定などを行う関数
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
				        map[ghostUnder+1,myBlock.rightBlock]==0 &&
				        nowBlock == 4)
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
				        map[ghostUnder+1,myBlock.leftBlock]==0 &&
				        nowBlock == 5)
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

		int underSpace = 0;
		for(int i=3;i>=0;i--)
		{
			for(int j=0;j<5;j++)
			{
				if(myBlock.square[i,j]==1)
				{
					underSpace=3-i;
					goto EXITLOOP;
				}
			}
		}

		EXITLOOP:;

		for(int i=0;i<5;i++)
		{
			for(int j=1;j<5;j++)
			{
			if(ghostUnder-(3-underSpace-i)>=0 && moveX+j<=9 &&
				   myBlock.square[i,j]==1 && map[ghostUnder-(3-underSpace-i),moveX+j]!=1)
				map[ghostUnder-(3-underSpace-i),moveX+j]=2;
			}
		}
	}

	// マップを更新する関数
	void MapCreate()
	{
		for (int i=0; i<20; i++) {
			for (int j=0; j<10; j++) {
				// 0:空き
				// 1:使用済み
				// 2:ゴーストブロック
				if (map [i, j] == 0) {
					// 色を変える
					Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
					renderer.material = new Material (backColor);
				}
				else if (map [i, j] == 2) {
					// 色を変える
					Renderer renderer = m_aObject [i, j].GetComponent<Renderer> ();
					renderer.material = new Material (gost);
				}
			}
		}
	}

	// 利用中のブロックの色を変える関数
	void ChangeMyBlockColor()
	{
		for (int i=0; i<5; i++) {
			for (int j=0; j<5; j++) {
				if (nowUnder - i <= ghostUnder && nowUnder - i >= 0 && myBlock.square [4 - i, j] == 1) {
					// 色を変える
					Renderer renderer = m_aObject [nowUnder - i, j + moveX].GetComponent<Renderer> ();
					renderer.material = new Material (nowColor);
					
				}
			}
		}
	}

	// ブロックがそろった時に消して列をずらす関数
	void DeleteBlock()
	{
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

		// キューブを生成し、位置と色を設定して表示する
		for (int i=0; i<20; i++) 
		{
			for(int j=0;j<10;j++)
			{
				m_aObject[i,j] = GameObject.CreatePrimitive (PrimitiveType.Cube);
				m_aObject[i,j].transform.Translate (-8+j,9-i,0);
				Renderer renderer = m_aObject[i,j].GetComponent<Renderer> ();
				renderer.material = new Material (backColor);
			}
		}

		rightFlag = false;
		leftFlag = false;
		underFlag = false;

		for (int y = 0; y < 5; y++) {
			for (int x = 0; x < 5; x++) {
				m_nextTetrimino [y, x] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			}
		}

		// 初期のブロックの設定
		nowUnder = 0;
		next = Random.Range(0,7);
		Debug.Log ("NEXT:"+next);
		SetBlockType ();
		ghostLeft = 0;

		frontUnder = myBlock.topBlock;

		// テキストの変更
		scoreText = GameObject.Find ("Canvas/TextScore").GetComponent<Text>();
		scoreText.text = "Score\n"+score;
		gameoverText = GameObject.Find ("Canvas/TextGameover").GetComponent<Text> ();
		gameoverText.text = "";
	}
	

	// Update is called once per frame
	void Update () {
		Resources.UnloadUnusedAssets ();
		// ゲームオーバーの時
		if (gameoverFlag == true)
		{
			gameoverText.text = "GAMEOVER\npush space";

			// spaceキーでシーンを切り替える
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				for(int i=0;i<20;i++)
				{
					for(int j=0;j<10;j++)
					{
						Destroy(m_aObject[i,j]);
					}
				}

				Application.LoadLevel ("Title");
			}	
		} 
		else
		{
			// 時間の取得
			timer += Time.deltaTime;

			// テトリミノの速度を経過した時間で変化させる
			if(timer > 180.0f)
				downTime=0.3f;
			else if(timer > 240.0f)
				downTime=0.2f;

			// 右回転をXキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする
			if (Input.GetKeyDown (KeyCode.X)/* && 
			    (myBlock.leftBlock + (myBlock.underBlock-myBlock.topBlock)) <=9 &&
			    nowBlock != 1 && ghostUnder > myBlock.underBlock*/) 
			{
				int right = 0;
				int left = 4;
				int top = 4;
				int under = 0;

				if(nowBlock==0)
				{
					int[,] blockCopy = new int[4,4];

					for(int i=0;i<4;i++)
					{
						for(int j=0;j<4;j++)
						{
							blockCopy[i,j]=myBlock.square[3-j,1+i];
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

					for(int i=0;i<4;i++)
					{
						for(int j=0;j<4;j++)
						{
							if(moveX+1 >= 0 && (myBlock.underBlock-3)+i >= 0 &&
							   blockCopy[i,j] == 1 && map[(myBlock.underBlock-3)+i,moveX+1] == 1)
							{
								goto EXITLOOP;
							}
						}
					}
					// コピーに作った回転させたデータを移す
					if(left+moveX >=0 && right+moveX <=9)
					{
						for(int i=0;i<4;i++)
						{
							for(int j=0;j<4;j++)
							{
								myBlock.square[i,1+j]=blockCopy[i,j];
							}
						}
						myBlock.leftBlock=(1+left)+moveX;
						myBlock.rightBlock=(1+right)+moveX;

						myBlock.underBlock=(nowUnder-1)-(3-under);
						myBlock.topBlock=myBlock.underBlock-(under-top);
					}
				EXITLOOP:;

				}
				else if(nowBlock>1)
				{
					int[,] blockCopy = new int[3,3];

					for(int i=0;i<3;i++)
					{
						for(int j=0;j<3;j++)
						{
							blockCopy[i,j]=myBlock.square[3-j,1+i];
				
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

					for(int i=0;i<3;i++)
					{
						for(int j=0;j<3;j++)
						{
							if(moveX+1 >= 0 && (myBlock.underBlock-2)+i>=0 &&
							   blockCopy[i,j] == 1 && map[(myBlock.underBlock-2)+i,moveX+1] == 1)
							{
								goto EXITLOOP;
							}
						}
					}
					// コピーに作った回転させたデータを移す
					if(left+moveX >=0 && right+moveX <=9)
					{
						for(int i=0;i<3;i++)
						{
							for(int j=0;j<3;j++)
							{
								myBlock.square[1+i,1+j]=blockCopy[i,j];
							}
						}
						myBlock.leftBlock=(1+left)+moveX;
						myBlock.rightBlock=(1+right)+moveX;
						myBlock.underBlock=(nowUnder-1)-(2-under);
						myBlock.topBlock=myBlock.underBlock-(under-top);
					}

					// 回転させたブロックの情報の更新
					if(myBlock.square[1+under,1+left]==1)
					{
						leftSpace=0;
					}
					else
					{
						for(int i=1+under;i>=0;i--)
						{
							if(myBlock.square[i,1+left]==1)
							{
								leftSpace=(1+under)-i;
								break;
							}
						}
					}

					if(myBlock.square[1+under,1+right]==1)
					{
						rightSpace=0;
					}
					else
					{
						for(int i=1+under;i>=0;i--)
						{
							if(myBlock.square[i,1+right]==1)
							{
								rightSpace=(1+under)-i;
								break;
							}
						}
					}
				EXITLOOP:;
				}

				Ghost();
				MapCreate();
				ChangeMyBlockColor();
			}

			// 左回転をZキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする
			if (Input.GetKeyDown (KeyCode.Z)/* && 
			    (myBlock.leftBlock + (myBlock.underBlock-myBlock.topBlock)) <=9 &&
			    nowBlock != 1 && ghostUnder > myBlock.underBlock*/) 
			{
				int right = 0;
				int left = 4;
				int top = 4;
				int under = 0;
				
				if(nowBlock==0)
				{
					int[,] blockCopy = new int[4,4];
					
					for(int i=0;i<4;i++)
					{
						for(int j=0;j<4;j++)
						{
							blockCopy[i,j]=myBlock.square[j,4-i];
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

					for(int i=0;i<4;i++)
					{
						for(int j=0;j<4;j++)
						{
							if(moveX+1 >= 0 && (myBlock.underBlock-3)+i >= 0 &&
							   blockCopy[i,j] == 1 && map[(myBlock.underBlock-3)+i,moveX+1] == 1)
							{
								goto EXITLOOP;
							}
						}
					}
					// コピーに作った回転させたデータを移す
					if(left+moveX >=0 && right+moveX <=9)
					{
						for(int i=0;i<4;i++)
						{
							for(int j=0;j<4;j++)
							{
								myBlock.square[i,1+j]=blockCopy[i,j];
							}
						}
						myBlock.leftBlock=(1+left)+moveX;
						myBlock.rightBlock=(1+right)+moveX;
						
						myBlock.underBlock=(nowUnder-1)-(3-under);
						myBlock.topBlock=myBlock.underBlock-(under-top);
					}
				EXITLOOP:;
				}
				else if(nowBlock>1)
				{
					int[,] blockCopy = new int[3,3];
					
					for(int i=0;i<3;i++)
					{
						for(int j=0;j<3;j++)
						{
							blockCopy[i,j]=myBlock.square[1+j,3-i];
							
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
					for(int i=0;i<3;i++)
					{
						for(int j=0;j<3;j++)
						{
							if(moveX+1 >= 0 && (myBlock.underBlock-2)+i>=0 &&
							   blockCopy[i,j] == 1 && map[(myBlock.underBlock-2)+i,moveX+1] == 1)
							{
								goto EXITLOOP;
							}
						}
					}
					// コピーに作った回転させたデータを移す
					if(left+moveX >=0 && right+moveX <=9)
					{
						for(int i=0;i<3;i++)
						{
							for(int j=0;j<3;j++)
							{
								myBlock.square[1+i,1+j]=blockCopy[i,j];
							}
						}
						myBlock.leftBlock=(1+left)+moveX;
						myBlock.rightBlock=(1+right)+moveX;
						myBlock.underBlock=(nowUnder-1)-(2-under);
						myBlock.topBlock=myBlock.underBlock-(under-top);
					}
					
					// 回転させたブロックの情報の更新
					if(myBlock.square[1+under,1+left]==1)
					{
						leftSpace=0;
					}
					else
					{
						for(int i=1+under;i>=0;i--)
						{
							if(myBlock.square[i,1+left]==1)
							{
								leftSpace=(1+under)-i;
								break;
							}
						}
					}
					
					if(myBlock.square[1+under,1+right]==1)
					{
						rightSpace=0;
					}
					else
					{
						for(int i=1+under;i>=0;i--)
						{
							if(myBlock.square[i,1+right]==1)
							{
								rightSpace=(1+under)-i;
								break;
							}
						}
					}
				EXITLOOP:;
				}
				Ghost();
				MapCreate();
				ChangeMyBlockColor();
			}

			// ブロックの位置を右に移動させる
			if (Input.GetKey (KeyCode.RightArrow) && myBlock.topBlock >= 0 && myBlock.rightBlock<9) 
			{
				downKeyStartTime += Time.deltaTime;

				if(downKeyStartTime >= rigidTime || !downKeyStart) {
					rightFlag = false;
					int underSpace = 0;
					for(int i=3;i>=0;i--)
					{
						for(int j=0;j<5;j++)
						{
							if(myBlock.square[i,j]==1)
							{
								underSpace=3-i;
								goto EXITLOOP;
							}
						}
					}
					EXITLOOP:;
				
					for(int i=0;myBlock.underBlock-i>=myBlock.topBlock;i++)
					{
						if(myBlock.square[(3-underSpace)-i,myBlock.rightBlock-moveX] == 1 &&
						 map[myBlock.underBlock-i,myBlock.rightBlock+1] == 1)
						{
							rightFlag=true;
							break;
						}
					}

					if(rightFlag==false)
					{
						moveX += 1;
						myBlock.leftBlock += 1;
						myBlock.rightBlock += 1;
					}

					Ghost();
					MapCreate();
					ChangeMyBlockColor();

					downKeyStartTime = 0.0f;
					downKeyStart = true;
				}
			}

			// ブロックの位置を左に移動させる
			if (Input.GetKey (KeyCode.LeftArrow) && myBlock.topBlock >= 0 && myBlock.leftBlock>0) 
			{
				downKeyStartTime += Time.deltaTime;

				if(downKeyStartTime >= rigidTime || !downKeyStart) {
					leftFlag=false;
					int underSpace = 0;
					for(int i=3;i>=0;i--)
					{
						for(int j=0;j<5;j++)
						{
							if(myBlock.square[i,j]==1)
							{
								underSpace=3-i;
								goto EXITLOOP;
							}
						}
					}
					EXITLOOP:;

					for(int i=0;myBlock.underBlock-i>=myBlock.topBlock;i++)
					{
						if(myBlock.square[(3-underSpace)-i,myBlock.leftBlock-moveX] == 1 &&
						 map[myBlock.underBlock-i,myBlock.leftBlock-1] == 1)
						{
							leftFlag=true;
							break;
						}
					}

					if(leftFlag==false)
					{
						moveX -= 1;
						myBlock.leftBlock -= 1;
						myBlock.rightBlock -= 1;
					}

					Ghost();
					MapCreate();
					ChangeMyBlockColor();
					
					downKeyStartTime = 0.0f;
					downKeyStart = true;
				}
			}

			// 両方押されていない
			if(!Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
				downKeyStartTime = 0.0f;
				downKeyStart = false;
			}

			// 下キーで落下させ続ける処理
			if(Input.GetKey(KeyCode.DownArrow) && myBlock.underBlock >=0)
			{
				MapCreate();

				if(myBlock.leftBlock!=ghostLeft)
					Ghost();

				// もうつめない場合
				if(myBlock.underBlock == ghostUnder && myBlock.topBlock<0)
				{
					gameoverFlag = true;

					for(int i=0;i<10;i++)
					{
						if(map[0,i]==1)
						{
							underFlag=true;
							break;
						}
					}

					if(underFlag==false)
					{
						for (int i=0; i<=nowUnder; i++)
						{
							for (int j=0; j<5; j++)
							{
								if (myBlock.square [4 - i, j] == 1)
								{
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - i, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (nowColor);
								}
							}
						}
					}
				}
				// 落下する処理
				else if(myBlock.underBlock < ghostUnder)
				{
					if (nowUnder < 5) 
					{
						for (int i=0; i<=nowUnder; i++)
						{
							for (int j=0; j<5; j++)
							{
								if (myBlock.square [4 - i, j] == 1)
								{
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - i, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (nowColor);
								}
							}
						}
					} 
					else 
					{
						ChangeMyBlockColor();
					}
				}
				// 積み上げる処理
				else 
				{
					ChangeMyBlockColor();
					for (int i=0; i<5; i++) 
					{
						for (int j=0; j<5; j++)
						{
							if (myBlock.square [4 - i, j] == 1 && (nowUnder - i)>=0) 
							{
								map [nowUnder - i, j + moveX] = 1;							
							}
						}
					}

					for (int i=0; i<20; i++) {
						for (int j=0; j<10; j++) {
							if(map[i,j]==2)
								map[i,j]=0;
						}
					}


					if(myBlock.topBlock - frontUnder >1)
					{
						if(myBlock.topBlock - frontUnder >= 19)
							score+=18;
						else
							score += myBlock.topBlock - frontUnder;
					}
										
					DeleteBlock();
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

					nowUnder = 0;
					// 次のブロックに合わせて値を変更する
					SetBlockType ();
					moveX = 2;
					rightFlag = false;
					leftFlag = false;
					ghostUnder=19;
					ghostLeft=0;
					frontUnder=1;
					nextScore=0;

					scoreText.text = "Score\n"+score;					

					for (int i=0; i<20; i++) {
						for (int j=0; j<10; j++) {
							if(map[i,j]==2)
								map[i,j]=0;
						}
					}

				}
				nowUnder += 1;
				myBlock.underBlock += 1;
				myBlock.topBlock += 1;
			}
			// ブロックを自然落下させる
			else if ((timer - frontTime) > downTime) 
			{
				MapCreate();

				if(myBlock.leftBlock!=ghostLeft)
					Ghost();

				// もうつめない場合
				if(myBlock.underBlock == ghostUnder && myBlock.topBlock<0)
				{
					gameoverFlag = true;

					for(int i=0;i<10;i++)
					{
						if(map[0,i]==1)
						{
							underFlag=true;
							break;
						}
					}
					if(underFlag==false)
					{
						for (int i=0; i<=nowUnder; i++)
						{
							for (int j=0; j<5; j++)
							{
								if (myBlock.square [4 - i, j] == 1)
								{
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - i, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (nowColor);
								}
							}
						}
					}
				}
				// 落下する処理
				else if(myBlock.underBlock < ghostUnder)
				{
					if (nowUnder < 5) 
					{
						for (int i=0; i<=nowUnder; i++) 
						{
							for (int j=0; j<5; j++)
							{
								if (myBlock.square [4 - i, j] == 1)
								{
									// 色を変える
									Renderer renderer = m_aObject [nowUnder - i, j + moveX].GetComponent<Renderer> ();
									renderer.material = new Material (nowColor);
								}
							}
						}
					} 
					else 
					{
						ChangeMyBlockColor();
					}

					if(myBlock.topBlock - frontUnder >1)
						nextScore += myBlock.topBlock - frontUnder;

					frontUnder = myBlock.topBlock;

				}
				// 積み上げる処理
				else if(myBlock.underBlock==ghostUnder)
				{
					ChangeMyBlockColor();
					for (int i=0; i<5; i++) 
					{
						for (int j=0; j<5; j++)
						{
							if (myBlock.square [4 - i, j] == 1 && (nowUnder - i)>=0) 
							{
								map [nowUnder - i, j + moveX] = 1;							
							}
						}
					}
					
					for (int i=0; i<20; i++) {
						for (int j=0; j<10; j++) {
							if(map[i,j]==2)
								map[i,j]=0;
						}
					}

					if(nextScore==0)
						score+=1;
					else
						score+=nextScore;

					DeleteBlock();
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

					nowUnder = 0;
					// 次のブロックに合わせて値を変更する
					SetBlockType ();
					moveX = 2;
					rightFlag = false;
					leftFlag = false;
					ghostUnder=19;
					ghostLeft=0;
					frontUnder=1;
					nextScore=0;
					
					scoreText.text = "Score\n"+score;					

					for (int i=0; i<20; i++) {
						for (int j=0; j<10; j++) {
							if(map[i,j]==2)
								map[i,j]=0;
						}
					}
				}

				nowUnder += 1;
				myBlock.underBlock += 1;
				myBlock.topBlock += 1;
				frontTime = timer;
			}

		}
	}
}
