using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(menuName = "ScriptableObject/CreateAsset")]
public class CharacterData : ScriptableObject
{
    public List<CharacterDataRecord> sheet;

    [Serializable]
    public class CharacterDataRecord
    {
        public int Id;                  //キャラクターID
        public Sprite StandingPicture;  //キャラクターの立ち絵
        public Sprite Icon;             //キャラクターのアイコン
        public Sprite cardImage;        //キャラカード
    }
}
