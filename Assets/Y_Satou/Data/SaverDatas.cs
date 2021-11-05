using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverDatas
{
    public static List<CharacterDatas> c_Datas { get; set; } = new List<CharacterDatas>();
    public static PlayerData p_Data { get; set; } = new PlayerData();
}

//キャラクターデータの参照元クラス
public class CharacterDatas
{
    public string name { get; set; }
    public string secondName { get; set; }
    public int id { get; set; }
    public int rarity { get; set; }
    public int atk { get; set; }
    public int cost { get; set; }
    public int hp { get; set; }
    public int interval { get; set; }
    public int speed { get; set; }
    public int type { get; set; }
    public int coolTime { get; set; }
    public Sprite icon { get; set; }
}

//プレイヤーデータの参照元クラス
public class PlayerData
{
    public string userName { get; set; } //ユーザーネーム
    public int userRank { get; set; } //ユーザーランク
    public int userExp { get; set; } //ユーザーの経験値
    public int userMoney { get; set; } //ユーザーの所持金
    public int userTicket { get; set; } //ユーザーの所持ガチャチケット
    public CharacterDatas[] team { get; set; } = new CharacterDatas[6]; //編成
    public List<CharacterDatas> c_UserDatas { get; set; } = new List<CharacterDatas>(); //ユーザーの所持しているキャラ
}