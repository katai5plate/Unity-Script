/********************************************************************************
	
	H2A_CharaChip

	用途：
	RPGツクール2000、2003、VX、MVの歩行グラフィックをUnityで扱いやすくするスクリプト。
	H2A_2dPlayerがADDされたオブジェクトに一緒に付けることで、
	ツクール風の歩行アニメーションを実現することができる。

	前提：
	使用する歩行グラフィック素材はRTPの素材規格そのままの構成の画像を使用し、
	MultipleSprite化して96枚の画像に分割すること。また、透過PNGにすることを推奨。

	最終更新：2017/09/18

 ********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 歩行グラフィック（４方向）の設定クラス
/// </summary>
[System.Serializable]
public class WalkSprite{
	public Vector3 up = 	 new Vector3(0,0,0);
	public Vector3 down =	 new Vector3(0,0,0);
	public Vector3 left =	 new Vector3(0,0,0);
	public Vector3 right = 	 new Vector3(0,0,0);
	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="args">上下左右各３つのMultipleSprite番号</param>
	public WalkSprite(params int[] args){
		up = 	 new Vector3(args[0],args[1],args[2]);
		down =	 new Vector3(args[3],args[4],args[5]);
		left =	 new Vector3(args[6],args[7],args[8]);
		right =  new Vector3(args[9],args[10],args[11]);
	}
}

/// <summary>
/// 歩行グラフィック配列タイプ列挙
/// </summary>
public enum WalkSpriteType {
	RPG_Maker_2000_3 = 0,
	RPG_Maker_VX_MV,
	User
}

/// <summary>
/// メインクラス
/// </summary>
public class H2A_CharaChip : MonoBehaviour {
	
	//Resourceフォルダ内の画像ファイルの名前（相対パス・拡張子不要）
	public string fileName;

	//歩行グラフィック配列データを選択
	public WalkSpriteType type;

	//RPGツクール2000～2003の歩行グラフィック配列データ
	[HideInInspector]
	public WalkSprite[] rpg2kMap = {
		new WalkSprite(0,1,2,24,25,26,36,37,38,12,13,14),
		new WalkSprite(3,4,5,27,28,29,39,40,41,15,16,17),
		new WalkSprite(6,7,8,30,31,32,42,43,44,18,19,20),
		new WalkSprite(9,10,11,33,34,35,45,46,47,21,22,23),
		new WalkSprite(48,49,50,72,73,74,84,85,86,60,61,62),
		new WalkSprite(51,52,53,75,76,77,87,88,89,63,64,65),
		new WalkSprite(54,55,56,78,79,80,90,91,92,66,67,68),
		new WalkSprite(57,58,59,81,82,83,93,94,95,69,70,71)
	};

	//RPGツクールVX～MVの歩行グラフィック配列データ
	[HideInInspector]
	public WalkSprite[] rpgVxMap = {
		new WalkSprite(36,37,38,0,1,2,12,13,14,24,25,26),
		new WalkSprite(39,40,41,3,4,5,15,16,17,27,28,29),
		new WalkSprite(42,43,44,6,7,8,18,19,20,30,31,32),
		new WalkSprite(45,46,47,9,10,11,21,22,23,33,34,35),
		new WalkSprite(84,85,86,48,49,50,60,61,62,72,73,74),
		new WalkSprite(87,88,89,51,52,53,63,64,65,75,76,77),
		new WalkSprite(90,91,92,54,55,56,66,67,68,78,79,80),
		new WalkSprite(93,94,95,57,58,59,69,70,71,81,82,83)
	};

	//ユーザー定義の歩行グラフィック配列データ
	public WalkSprite[] userMap = new WalkSprite[8];

	/// <summary>
	/// MultipleSpriteから１枚抽出する
	/// ただし画像名が「[fileName]_[num]」の形になっている場合に限る。
	/// </summary>
	/// <param name="num">MultipleSprite番号</param>
	public Sprite LoadMulSprite(int num){
		Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
		return System.Array.Find<Sprite>( sprites, (sprite) => sprite.name.Equals(fileName+"_"+num));
	}

	/// <summary>
	/// MultipleSpriteから条件に合った１枚を抽出する
	/// </summary>
	/// <param name="actor">キャラクター番号（左上から右に0123、左下から右に4567）</param>
	/// <param name="dir">向いている方角（テンキーを参照：2468）</param>
	/// <param name="foot">足の状態（-1/0/1）</param>
	public Sprite ChoiceCharaSprite(int actor,int dir,int foot){
		WalkSprite w;
		Vector3 v;
		float id;
		switch(type){
			case WalkSpriteType.RPG_Maker_2000_3:
				w = rpg2kMap[actor];
				break;
			case WalkSpriteType.RPG_Maker_VX_MV:
				w = rpgVxMap[actor];
				break;
			default:
				w = userMap[actor];
				break;
		}
		switch(dir){
			case 8:	v = w.up;		break;
			case 2:	v = w.down;		break;
			case 4:	v = w.left;		break;
			case 6:	v = w.right;	break;
			default:				return null;
		}
		switch(foot){
			case -1:	id = v.x;	break;
			case 0:		id = v.y;	break;
			case 1:		id = v.z;	break;
			default:				return null;
		}
		return LoadMulSprite((int)id);
	}
}
