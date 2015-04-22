using UnityEngine;
using System.Collections;

using UnityEngine.UI;

struct BLOCK{
	public int[,] square;
	public Vector2[] blockPosition;
	public Vector2 nowPosition;
};

enum KEYCODE {
	RIGHT_ARROW,
	LEFT_ARROW,
	DOWN_ARROW,
	Z_KEY_CODE,
	X_KEY_CODE,
	NONE,
};

enum BLOCKSTATE {
	EMPTY,
	USED,
	GHOST,
	USING,
};

public class Game : MonoBehaviour {
	const int I_TETRIMINO = 0;
	const int O_TETRIMINO = 1;
	const int S_TETRIMINO = 2;
	const int Z_TETRIMINO = 3;
	const int J_TETRIMINO = 4;
	const int L_TETRIMINO = 5;
	const int T_TETRIMINO = 6;

	public Text scoreText;
	public Text gameoverText;

	BLOCK myBlock = new BLOCK();
	BLOCK nextBlock = new BLOCK();

	// 20*10
	BLOCKSTATE[,] map = new BLOCKSTATE[20,10];

	bool gameoverFlag;
	bool speedUp;

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
	public float nowSpeed = 1.0f;
	public float downTime = 1.0f;

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

	private GameObject[,] m_aObject = new GameObject[20,10];
	private GameObject[,] m_nextTetrimino = new GameObject[5, 5];

	float downKeyStartTime = 0.0f;
	bool downKeyStart;
	float rigidTime = 0.1f;

	int blockCount = 0;

	// ブロックの種類をランダムで選択し、セットする関数
	void SetBlockType ()
	{
		myBlock.square = new int[5, 5];
		myBlock.blockPosition = new Vector2[4];

		nowBlock = next;
		next = Random.Range(0,7);
		Debug.Log ("NEXT:"+next);

		switch(nowBlock)
		{
		case I_TETRIMINO:
			myBlock.square = new int [,]{{0,0,1,0,0},
										 {0,0,1,0,0},
										 {0,0,1,0,0},
										 {0,0,1,0,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(2, 0);
			myBlock.blockPosition[1] = new Vector2(2, 1);
			myBlock.blockPosition[2] = new Vector2(2, 2);
			myBlock.blockPosition[3] = new Vector2(2, 3);

			nowColor = lightBlue;

			break;
			
		case O_TETRIMINO:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,0,0,0,0},						
										 {0,1,1,0,0},
										 {0,1,1,0,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(1, 2);
			myBlock.blockPosition[1] = new Vector2(1, 3);
			myBlock.blockPosition[2] = new Vector2(2, 2);
			myBlock.blockPosition[3] = new Vector2(2, 3);

			nowColor = yello;

			break;
			
		case S_TETRIMINO:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,0,0,0,0},
										 {0,0,1,1,0},
										 {0,1,1,0,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(1, 3);
			myBlock.blockPosition[1] = new Vector2(2, 2);
			myBlock.blockPosition[2] = new Vector2(2, 3);
			myBlock.blockPosition[3] = new Vector2(3, 2);

			nowColor = yelloGreen;

			break;
			
		case Z_TETRIMINO:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,0,0,0,0},
										 {0,1,1,0,0},
										 {0,0,1,1,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(1, 2);
			myBlock.blockPosition[1] = new Vector2(2, 2);
			myBlock.blockPosition[2] = new Vector2(2, 3);
			myBlock.blockPosition[3] = new Vector2(3, 3);

			nowColor = red;

			break;
			
		case J_TETRIMINO:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,0,1,0,0},	
										 {0,0,1,0,0},
										 {0,1,1,0,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(1, 3);
			myBlock.blockPosition[1] = new Vector2(2, 1);
			myBlock.blockPosition[2] = new Vector2(2, 2);
			myBlock.blockPosition[3] = new Vector2(2, 3);

			nowColor = blue;

			break;
			
		case L_TETRIMINO:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,0,1,0,0},
										 {0,0,1,0,0},
										 {0,0,1,1,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(2, 1);
			myBlock.blockPosition[1] = new Vector2(2, 2);
			myBlock.blockPosition[2] = new Vector2(2, 3);
			myBlock.blockPosition[3] = new Vector2(3, 3);

			nowColor = orange;

			break;
			
		case T_TETRIMINO:
			myBlock.square = new int [,]{{0,0,0,0,0},
										 {0,0,0,0,0},
										 {0,1,1,1,0},
										 {0,0,1,0,0},
										 {0,0,0,0,0}};

			myBlock.blockPosition[0] = new Vector2(1, 2);
			myBlock.blockPosition[1] = new Vector2(2, 2);
			myBlock.blockPosition[2] = new Vector2(2, 3);
			myBlock.blockPosition[3] = new Vector2(3, 2);

			nowColor = purple;

			break;
			
		default:
			break;
		}
		myBlock.nowPosition = new Vector2 (3, -4);
		blockCount++;
		speedUp = false;

		SetNext ();
	}

	// ネクストの設定
	void SetNext () {
		switch(next)
		{
		case I_TETRIMINO:
			nextBlock.square = new int [,]{{0,0,1,0,0},
										   {0,0,1,0,0},
										   {0,0,1,0,0},
										   {0,0,1,0,0},
										   {0,0,0,0,0}};
			
			nextColor = lightBlue;
			
			break;
			
		case O_TETRIMINO:
			nextBlock.square = new int [,]{{0,0,0,0,0},
										   {0,0,0,0,0},						
										   {0,1,1,0,0},
										   {0,1,1,0,0},
										   {0,0,0,0,0}};
			
			nextColor = yello;
			
			break;
			
		case S_TETRIMINO:
			nextBlock.square = new int [,]{{0,0,0,0,0},
										   {0,0,0,0,0},
										   {0,0,1,1,0},
										   {0,1,1,0,0},
										   {0,0,0,0,0}};
			
			nextColor = yelloGreen;
			
			break;
			
		case Z_TETRIMINO:
			nextBlock.square = new int [,]{{0,0,0,0,0},
										   {0,0,0,0,0},
										   {0,1,1,0,0},
										   {0,0,1,1,0},
										   {0,0,0,0,0}};
			
			nextColor = red;
			
			break;
			
		case J_TETRIMINO:
			nextBlock.square = new int [,]{{0,0,0,0,0},
										   {0,0,1,0,0},	
										   {0,0,1,0,0},
										   {0,1,1,0,0},
										   {0,0,0,0,0}};
			
			nextColor = blue;
			
			break;
			
		case L_TETRIMINO:
			nextBlock.square = new int [,]{{0,0,0,0,0},
										   {0,0,1,0,0},
										   {0,0,1,0,0},
										   {0,0,1,1,0},
										   {0,0,0,0,0}};
			
			nextColor = orange;
			
			break;
			
		case T_TETRIMINO:
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

	// ゲームオーバー
	void GameOver () {
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

	// ゴーストの当たり判定などを行う関数
	void Ghost()
	{
		// 使用済みブロック、床に衝突するまでの長さを求める
		int y;
		for(y = 0; y < 20; y++) {
			bool empty = true;
			for(int i = 0; i < 4 && empty; i++) {
				int x = (int)(myBlock.blockPosition[i].x + myBlock.nowPosition.x);
				if(y + myBlock.blockPosition[i].y >= 20) {
					empty = false;
					break;
				}
				BLOCKSTATE state = map[y + (int)myBlock.blockPosition[i].y, x];
				if(state == BLOCKSTATE.USED) {
					empty = false;
				}
			}
			if(!empty) {
				break;
			}
		}

		// ゴーストと落下中のブロックが重なった時、ブロックを表示するため
		for (int i = 0; i < 4; i++) {
			int x = (int)(myBlock.blockPosition[i].x + myBlock.nowPosition.x);
			if(map[y + (int)myBlock.blockPosition[i].y - 1, x] == BLOCKSTATE.USING) {
				return;
			}
		}

		// ゴースト更新
		for(int j = 0; j < 20; j++) {
			for(int i = 0; i < 10; i++) {
				if(map[j, i] == BLOCKSTATE.GHOST) {
					map[j, i] = BLOCKSTATE.EMPTY;
				}
			}
		}
		for(int i = 0; i < 4; i++) {
			int x = (int)(myBlock.blockPosition[i].x + myBlock.nowPosition.x);
			map[y + (int)myBlock.blockPosition[i].y - 1, x] = BLOCKSTATE.GHOST;
		}
	}

	// マップを更新する関数
	void MapCreate ()
	{
		for (int y=0; y<20; y++) {
			for (int x=0; x<10; x++) {
				Renderer renderer = m_aObject [y, x].GetComponent<Renderer> ();
				// BLOCKSTATE.EMPTY:空き
				// BLOCKSTATE.USED:使用済み
				// BLOCKSTATE.GHOST:ゴーストブロック
				// BLOCKSTATE.USING:使用中
				switch(map [y, x]) {
				case BLOCKSTATE.EMPTY:
					// 色を変える
					renderer.material = new Material (backColor);
					break;
				case BLOCKSTATE.GHOST:
					// 色を変える
					renderer.material = new Material (gost);
					break;
				case BLOCKSTATE.USING:
					// 色を変える
					renderer.material = new Material (nowColor);
					break;
				default:
					break;
				}
			}
		}
	}

	// ブロックがそろった時に消して列をずらす関数
	void DeleteBlock ()
	{
		// 横ライン確認
		for (int j = (int)myBlock.nowPosition.y; j < myBlock.nowPosition.y + 5 && j < 20; j++) {
			int count = 0;
			for(int i = 0; i < 10; i++) {
				if(map[j, i] == BLOCKSTATE.USED) {
					count++;
				}
				// 削除処理
				if(count >= 10) {
					for(int x = 0; x < 10; x++) {
						map[j, x] = BLOCKSTATE.EMPTY;
					}
					// 下に下げる処理
					for(int y = j; y > 0; y--) {
						for(int x = 0; x < 10; x++) {
							map[y, x] = map[y-1, x];
						}
					}
					for(int x = 0; x < 10; x++) {
						map[0, x] = BLOCKSTATE.EMPTY;
					}
					scoreLineCount++;
				}
			}
		}
	}

	// スコア
	void Score () {
		if(nextScore==0)
			score+=1;
		else
			score+=nextScore;
		
		DeleteBlock();
		switch(scoreLineCount)
		{
		case 1:
			score+=40;
			break;
			
		case 2:
			score+=100;
			break;
			
		case 3:
			score+=300;
			break;
			
		case 4:
			score+=1200;
			break;
			
		default:
			break;
		}
		scoreLineCount=0;
		nextScore=0;
		scoreText.text = "Score\n"+score;
	}

	// ブロックの移動
	bool MoveBlock (KEYCODE keyCode = KEYCODE.NONE) {
		int moveX = 0;
		int moveY = 0;
		switch (keyCode) {
		// 左が押された場合
		case KEYCODE.LEFT_ARROW:
			moveX = -1;
			break;
		// 右が押された場合
		case KEYCODE.RIGHT_ARROW:
			moveX = 1;
			break;
		// 落下
		case KEYCODE.NONE:
			moveY = 1;
			break;
		default:
			break;
		}

		for(int i = 0; i < 4; i++) {
			int y = (int)(myBlock.blockPosition[i].y + myBlock.nowPosition.y);
			if(y < 0) {
				moveX = 0;
				break;
			}
			if(i == 3) {
				speedUp = true;
			}
		}

		// 移動できるか
		for (int i = 0; i < 4; i++) {
			int x = (int)(myBlock.blockPosition[i].x + myBlock.nowPosition.x + moveX);
			int y = (int)(myBlock.blockPosition[i].y + myBlock.nowPosition.y + moveY);
			if(x < 0 || x > 9) {
				return false;
			}
			if(y > 19) {
				return false;
			}
			if(y >= 0) {
				if(map[y, x] == BLOCKSTATE.USED) {
					return false;
				}
			}
		}
		// 移動
		myBlock.nowPosition.x += moveX;
		myBlock.nowPosition.y += moveY;
		for (int j = 0; j < 20; j++) {
			for(int i = 0; i < 10; i++) {
				if(map[j, i] == BLOCKSTATE.USING) {
					map[j, i] = BLOCKSTATE.EMPTY;
				}
			}
		}
		for (int i = 0; i < 4; i++) {
			int x = (int)(myBlock.nowPosition.x + myBlock.blockPosition[i].x);
			int y = (int)(myBlock.nowPosition.y + myBlock.blockPosition[i].y);
			if(y < 0) {
				continue;
			}
			map[y, x] = BLOCKSTATE.USING;
		}

		return true;
	}

	// ブロックの回転
	void RotateBlockLeft () {
		if (nowBlock != O_TETRIMINO) {
			// 回転後のブロックを作成
			int[,] afterBlock = new int[5, 5];
			int index = 0;
			Vector2[] position = new Vector2[4];
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 5; x++) {
					afterBlock [4-x, y] = myBlock.square [y, x];
					if(afterBlock[4-x, y] == 1) {
						position[index] = new Vector2(y, 4-x);
						index++;
					}
				}
			}

			// 回転できるか
			for(int i = 0; i < 4; i++) {
				int x = (int)(position[i].x + myBlock.nowPosition.x);
				int y = (int)(position[i].y + myBlock.nowPosition.y);
				if(y < 0 || y >= 20) {
					return;
				}
				if(x >= 0 && x < 10) {
					if(map[y, x] == BLOCKSTATE.USED) {
						return;
					}
				}
			}
			for(int y = 0; y < 20; y++) {
				for(int x = 0; x < 10; x++) {
					if(map[y, x] == BLOCKSTATE.USING) {
						map[y, x] = BLOCKSTATE.EMPTY;
					}
				}
			}

			// 壁にぶつかった時に横にずれて回転する
			int leftMove = 0;
			int rightMove = 9;
			for(int i = 0; i < 4; i++) {
				int x = (int)(position[i].x + myBlock.nowPosition.x);
				if(leftMove > x) {
					leftMove = x;
				}
				if(rightMove < x) {
					rightMove = x;
				}
			}
			if(leftMove != 0) {
				int x = (int)(myBlock.nowPosition.x - leftMove);
				int y = (int)(myBlock.nowPosition.y);
				myBlock.nowPosition = new Vector2(x, y);
			}
			if(rightMove != 9) {
				int x = (int)(myBlock.nowPosition.x - (rightMove - 9));
				int y = (int)(myBlock.nowPosition.y);
				myBlock.nowPosition = new Vector2(x, y);
			}

			index = 0;
			for(int y = 0; y < 5; y++) {
				for(int x = 0; x < 5; x++) {
					myBlock.square[y, x] = afterBlock[y, x];
					if(myBlock.square[y, x] == 1) {
						map[y + (int)myBlock.nowPosition.y, x + (int)myBlock.nowPosition.x] = BLOCKSTATE.USING;
						myBlock.blockPosition[index] = new Vector2(x, y);
						index++;
					}
				}
			}
		}
	}

	void RotateBlockRight () {
		if (nowBlock != O_TETRIMINO) {
			// 回転後のブロックを作成
			int[,] afterBlock = new int[5, 5];
			int index = 0;
			Vector2[] position = new Vector2[4];
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 5; x++) {
					afterBlock [x, 4-y] = myBlock.square[y, x];
					if(afterBlock[x, 4-y] == 1) {
						position[index] = new Vector2(4-y, x);
						index++;
					}
				}
			}

			// 回転できるか
			for(int i = 0; i < 4; i++) {
				int x = (int)(position[i].x + myBlock.nowPosition.x);
				int y = (int)(position[i].y + myBlock.nowPosition.y);
				if(y < 0 || y >= 20) {
					return;
				}
				if(x >= 0 && x < 10) {
					if(map[y, x] == BLOCKSTATE.USED) {
						return;
					}
				}
			}
			for(int y = 0; y < 20; y++) {
				for(int x = 0; x < 10; x++) {
					if(map[y, x] == BLOCKSTATE.USING) {
						map[y, x] = BLOCKSTATE.EMPTY;
					}
				}
			}

			// 壁にぶつかった時に横にずれて回転する
			int leftMove = 0;
			int rightMove = 9;
			for(int i = 0; i < 4; i++) {
				int x = (int)(position[i].x + myBlock.nowPosition.x);
				if(leftMove > x) {
					leftMove = x;
				}
				if(rightMove < x) {
					rightMove = x;
				}
			}
			if(leftMove != 0) {
				int x = (int)(myBlock.nowPosition.x - leftMove);
				int y = (int)(myBlock.nowPosition.y);
				myBlock.nowPosition = new Vector2(x, y);
			}
			if(rightMove != 9) {
				int x = (int)(myBlock.nowPosition.x - (rightMove - 9));
				int y = (int)(myBlock.nowPosition.y);
				myBlock.nowPosition = new Vector2(x, y);
			}

			index = 0;
			for(int y = 0; y < 5; y++) {
				for(int x = 0; x < 5; x++) {
					myBlock.square[y, x] = afterBlock[y, x];
					if(myBlock.square[y, x] == 1) {
						map[y + (int)myBlock.nowPosition.y, x + (int)myBlock.nowPosition.x] = BLOCKSTATE.USING;
						myBlock.blockPosition[index] = new Vector2(x, y);
						index++;
					}
				}
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
				map[i,j] = BLOCKSTATE.EMPTY;
			}
		}

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

		for (int y = 0; y < 5; y++) {
			for (int x = 0; x < 5; x++) {
				m_nextTetrimino [y, x] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			}
		}

		// 初期のブロックの設定
		next = Random.Range(0,7);
		Debug.Log ("NEXT:"+next);
		SetBlockType ();
		ghostLeft = 0;

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
			GameOver ();
		} 
		else
		{
			// 時間の取得
			timer += Time.deltaTime;

			// テトリミノの速度を変化させる
			if(blockCount >= 10) {
				if(downTime > 0.01) {
					nowSpeed -= 0.01f;
					downTime = nowSpeed;
				} else {
					downTime = 0.01f;
				}
				blockCount = 0;
			}

			// 右回転をXキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする
			if (Input.GetKeyDown (KeyCode.X)) 
			{
				RotateBlockRight ();
			}

			// 左回転をZキーで行う
			// ブロックがO型以外の場合に回転処理をするようにする
			if (Input.GetKeyDown (KeyCode.Z)) 
			{
				RotateBlockLeft ();
			}

			// ブロックの位置を右に移動させる
			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				downKeyStartTime += Time.deltaTime;

				if(downKeyStartTime >= rigidTime || !downKeyStart) {
					MoveBlock (KEYCODE.RIGHT_ARROW);

					downKeyStartTime = 0.0f;
					downKeyStart = true;
				}
			}

			// ブロックの位置を左に移動させる
			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				downKeyStartTime += Time.deltaTime;

				if(downKeyStartTime >= rigidTime || !downKeyStart) {
					MoveBlock (KEYCODE.LEFT_ARROW);
					
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
			if(speedUp && Input.GetKey(KeyCode.DownArrow))
			{
				downTime = 0.01f;
				nextScore++;
			}
			else {
				downTime = nowSpeed;
			}
			// ブロックを自然落下させる
			if (timer >= downTime) 
			{
				// ブロックが止まった
				if(!MoveBlock ()) {
					// ブロックを使用済みに変更
					for(int y = 0; y < 20; y++) {
						for(int x = 0; x < 10; x++) {
							if(map[y, x] == BLOCKSTATE.USING) {
								map[y, x] = BLOCKSTATE.USED;
							}
						}
					}

					// ブロックが消えるか
					DeleteBlock ();
					
					// スコア
					Score ();

					// 次のブロックをセット
					SetBlockType ();
				}

				timer = 0.0f;
			}

			Ghost ();
		}
		MapCreate ();
	}
}
