/********************************************************************************
	
	H2A_2dPlayer

	用途：
	ツクール風の2D移動を再現するスクリプト。
	H2A_CharaChipがADDされたオブジェクトに一緒に付けることで、
	ツクール風の歩行アニメーションを実現することができる。（勿論無くても動作する。）

	前提：
	このスクリプトがADDされたオブジェクトの子供オブジェクトには、
	SpriteRendererがADDされていなければならない。

	最終更新：2017/09/18

 ********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動に関するパラメーター
/// </summary>
[System.Serializable]
public class WalkProcess{
	//１歩の長さ
	public float moveGrid = 1.0f;
	//４方向の方角
	public Vector3 up = new Vector3(0,1,0);
	public Vector3 down = new Vector3(0,-1,0);
	public Vector3 left = new Vector3(-1,0,0);
	public Vector3 right = new Vector3(1,0,0);
	//移動スピード
	public float moveSpeed = 0.05f;
	//デフォルトの向き
	public int direction = 2;
}

/// <summary>
/// メインクラス
/// </summary>
public class H2A_2dPlayer : MonoBehaviour {
	
	//移動に関するパラメーター
	public WalkProcess walk;

	//座標移動アニメ用
	float walkTime;
	Vector3 aPos,bPos;

	//足踏みアニメ用
	int footStat=0;
	int pfs=1;

	//連携するコンポーネント
	H2A_CharaChip h2A_CharaChip;
	SpriteRenderer image;

	//当たり判定用
	RaycastHit2D raycast;

	//雑用
	bool k;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		//h2A_CharaChipとの連携
		h2A_CharaChip = GetComponent<H2A_CharaChip>();
		//子オブジェクトのSpriteと連携
		image = GetComponentInChildren<SpriteRenderer>();
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		//描画
		DrawProcess();
		//メイン
		MainProcess();
	}

	/// <summary>
	/// 正面を調べて当たり判定
	/// </summary>
	/// <param name="dir">方角</param>
	public RaycastHit2D nextStepCast(Vector2 dir){
		return Physics2D.BoxCast(transform.position,Vector2.one*(walk.moveGrid/2),0f,dir,walk.moveGrid);
	}

	/// <summary>
	/// 描画プロセス
	/// </summary>
	void DrawProcess(){
		if(h2A_CharaChip!=null){
			image.sprite = h2A_CharaChip.ChoiceCharaSprite(0,walk.direction,footStat);
		}
	}
	
	/// <summary>
	/// メインプロセス
	/// </summary>
	void MainProcess(){
		//立ち止まっているなら
		if(walkTime==0){
			//上に移動
			if(Input.GetAxisRaw("Vertical")==1){
				walk.direction=8;
				bPos=walk.up;
				k=true;
			}
			//下に移動
			if(Input.GetAxisRaw("Vertical")==-1){
				walk.direction=2;
				bPos=walk.down;
				k=true;
			}
			//左に移動
			if(Input.GetAxisRaw("Horizontal")==-1){
				walk.direction=4;
				bPos=walk.left;
				k=true;
			}
			//右に移動
			if(Input.GetAxisRaw("Horizontal")==1){
				walk.direction=6;
				bPos=walk.right;
				k=true;
			}
			//移動キーが押されたなら
			if(k){
				//正面に障害物がある
				if(!!nextStepCast(bPos)){
					//移動キー入力は無かったことに
					k=false;
				//正面に障害物はない
				}else{
					//歩き始める
					walkTime=walk.moveSpeed;
					footStat=-pfs; pfs=footStat;
					aPos=transform.position;
					bPos=aPos+(bPos*walk.moveGrid);
					k=false;
				}
			}
		//歩いているなら
		}else{
			//walk.moveSpeedの早さで0から1まで到達したら一度立ち止まる
			transform.position = Vector3.Lerp(aPos,bPos,walkTime);
			if(walkTime>=1){
				transform.position = bPos;
				walkTime=0;
				footStat=0;
			}else{
				if(walkTime>=0.5f)footStat=0;
				walkTime+=walk.moveSpeed;
			}
		}
	}
}

