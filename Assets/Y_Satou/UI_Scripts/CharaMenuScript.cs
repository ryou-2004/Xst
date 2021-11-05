using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CharaMenuScript : MonoBehaviour
{
    [Header("デッキエリア")]public GameObject DeckArea;
    [Header("カード詳細")]public GameObject Card_Details;

    public GameObject[] deckCard;
    public CharacterData characterData;
    public Sprite rarity3Frame;
    public Sprite rarity2Frame;
    public Sprite[] typeIcons;

    [Header("カード詳細画面")]
    public GameObject displayCard;
    public TextMeshProUGUI Text_CharacterName;
    public Text Text_ValueStyle;
    public Text Text_Value;
    public GameObject displayImage;
    private int displayCharaID;

    public void GetCharaDataDetails(int temeNum)
    {
        DeckArea.SetActive(false);
        Card_Details.SetActive(true);
        displayImage.SetActive(false);

        displayCharaID = temeNum;

        Text_CharacterName.text = $"{SaverDatas.p_Data.team[displayCharaID].secondName}\n{SaverDatas.p_Data.team[displayCharaID].name}";

        var charaImage = displayCard.transform.Find("Chara").gameObject.GetComponentInChildren<Image>();
        var charaId = characterData.sheet.Where(x => x.Id == SaverDatas.p_Data.team[displayCharaID].id);
        foreach(var i in charaId)
        {
            charaImage.sprite = i.cardImage;
        }

        charaImage = displayCard.transform.Find("Frame").gameObject.GetComponent<Image>();
        if (SaverDatas.p_Data.team[displayCharaID].rarity == 3)
        {
            charaImage.sprite = rarity3Frame;
        }
        else if (SaverDatas.p_Data.team[displayCharaID].rarity == 2)
        {
            charaImage.sprite = rarity2Frame;
        }
        else
        {
            charaImage.sprite = rarity2Frame;
        }

        charaImage = displayCard.transform.Find("Icon").gameObject.GetComponent<Image>();
        if (SaverDatas.p_Data.team[displayCharaID].type == 1)
        {
            charaImage.sprite = typeIcons[0];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 2)
        {
            charaImage.sprite = typeIcons[1];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 3)
        {
            charaImage.sprite = typeIcons[2];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 4)
        {
            charaImage.sprite = typeIcons[3];
        }
        else
        {
            charaImage.sprite = typeIcons[0];
        }

        Text_ValueStyle.text = "Rarity\nHp\nAtk\nSpeed";
        string rarityText = null;
        switch (SaverDatas.p_Data.team[displayCharaID].rarity)
        {
            case 1:
                rarityText = "☆☆☆☆★";
                break;
            case 2:
                rarityText = "☆☆☆★★";
                break;
            case 3:
                rarityText = "☆☆★★★";
                break;
            case 4:
                rarityText = "☆★★★★";
                break;
            case 5:
                rarityText = "★★★★★";
                break;
        }
        Text_Value.text = $"{rarityText}\n{SaverDatas.p_Data.team[displayCharaID].hp}\n{SaverDatas.p_Data.team[displayCharaID].atk}\n{SaverDatas.p_Data.team[displayCharaID].speed}";
    }

    public void rightDisplay()
    {
        DeckArea.SetActive(false);
        Card_Details.SetActive(true);
        displayImage.SetActive(false);

        displayCharaID += 1;
        if(displayCharaID > 5)
        {
            displayCharaID = displayCharaID % 6;
        }

        Text_CharacterName.text = $"{SaverDatas.p_Data.team[displayCharaID].secondName}\n{SaverDatas.p_Data.team[displayCharaID].name}";

        var charaImage = displayCard.transform.Find("Chara").gameObject.GetComponentInChildren<Image>();
        var charaId = characterData.sheet.Where(x => x.Id == SaverDatas.p_Data.team[displayCharaID].id);
        foreach (var i in charaId)
        {
            charaImage.sprite = i.cardImage;
        }

        charaImage = displayCard.transform.Find("Frame").gameObject.GetComponent<Image>();
        if (SaverDatas.p_Data.team[displayCharaID].rarity == 3)
        {
            charaImage.sprite = rarity3Frame;
        }
        else if (SaverDatas.p_Data.team[displayCharaID].rarity == 2)
        {
            charaImage.sprite = rarity2Frame;
        }
        else
        {
            charaImage.sprite = rarity2Frame;
        }

        charaImage = displayCard.transform.Find("Icon").gameObject.GetComponent<Image>();
        if (SaverDatas.p_Data.team[displayCharaID].type == 1)
        {
            charaImage.sprite = typeIcons[0];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 2)
        {
            charaImage.sprite = typeIcons[1];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 3)
        {
            charaImage.sprite = typeIcons[2];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 4)
        {
            charaImage.sprite = typeIcons[3];
        }
        else
        {
            charaImage.sprite = typeIcons[0];
        }

        Text_ValueStyle.text = "Rarity\nHp\nAtk\nSpeed";
        string rarityText = null;
        switch (SaverDatas.p_Data.team[displayCharaID].rarity)
        {
            case 1:
                rarityText = "☆☆☆☆★";
                break;
            case 2:
                rarityText = "☆☆☆★★";
                break;
            case 3:
                rarityText = "☆☆★★★";
                break;
            case 4:
                rarityText = "☆★★★★";
                break;
            case 5:
                rarityText = "★★★★★";
                break;
        }
        Text_Value.text = $"{rarityText}\n{SaverDatas.p_Data.team[displayCharaID].hp}\n{SaverDatas.p_Data.team[displayCharaID].atk}\n{SaverDatas.p_Data.team[displayCharaID].speed}";
    }
    public void leftDisplay()
    {
        DeckArea.SetActive(false);
        Card_Details.SetActive(true);
        displayImage.SetActive(false);

        displayCharaID -= 1;
        if(displayCharaID < 0)
        {
            displayCharaID = 5;
        }

        Text_CharacterName.text = $"{SaverDatas.p_Data.team[displayCharaID].secondName}\n{SaverDatas.p_Data.team[displayCharaID].name}";

        var charaImage = displayCard.transform.Find("Chara").gameObject.GetComponentInChildren<Image>();
        var charaId = characterData.sheet.Where(x => x.Id == SaverDatas.p_Data.team[displayCharaID].id);
        foreach (var i in charaId)
        {
            charaImage.sprite = i.cardImage;
        }

        charaImage = displayCard.transform.Find("Frame").gameObject.GetComponent<Image>();
        if (SaverDatas.p_Data.team[displayCharaID].rarity == 3)
        {
            charaImage.sprite = rarity3Frame;
        }
        else if (SaverDatas.p_Data.team[displayCharaID].rarity == 2)
        {
            charaImage.sprite = rarity2Frame;
        }
        else
        {
            charaImage.sprite = rarity2Frame;
        }

        charaImage = displayCard.transform.Find("Icon").gameObject.GetComponent<Image>();
        if (SaverDatas.p_Data.team[displayCharaID].type == 1)
        {
            charaImage.sprite = typeIcons[0];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 2)
        {
            charaImage.sprite = typeIcons[1];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 3)
        {
            charaImage.sprite = typeIcons[2];
        }
        else if (SaverDatas.p_Data.team[displayCharaID].type == 4)
        {
            charaImage.sprite = typeIcons[3];
        }
        else
        {
            charaImage.sprite = typeIcons[0];
        }

        Text_ValueStyle.text = "Rarity\nHp\nAtk\nSpeed";
        string rarityText = null;
        switch (SaverDatas.p_Data.team[displayCharaID].rarity)
        {
            case 1:
                rarityText = "☆☆☆☆★";
                break;
            case 2:
                rarityText = "☆☆☆★★";
                break;
            case 3:
                rarityText = "☆☆★★★";
                break;
            case 4:
                rarityText = "☆★★★★";
                break;
            case 5:
                rarityText = "★★★★★";
                break;
        }
        Text_Value.text = $"{rarityText}\n{SaverDatas.p_Data.team[displayCharaID].hp}\n{SaverDatas.p_Data.team[displayCharaID].atk}\n{SaverDatas.p_Data.team[displayCharaID].speed}";
    }

    public void displayChara()
    {
        displayImage.SetActive(true);
        var charaImage = displayImage.transform.Find("CharaPosi").gameObject.GetComponentInChildren<Image>();
        var charaId = characterData.sheet.Where(x => x.Id == SaverDatas.p_Data.team[displayCharaID].id);
        foreach (var i in charaId)
        {
            charaImage.sprite = i.StandingPicture;
        }
    }
    public void hideChara()
    {
        displayImage.SetActive(false);
    }

    public void SetCharaCard()
    {
        DeckArea.SetActive(true);
        Card_Details.SetActive(false);

        for(var i = 0; i < 6; i++)
        {
            var charaID = SaverDatas.p_Data.team[i].id;
            deckCard[i].SetActive(true);
            var charaImage = deckCard[i].transform.Find("Chara").gameObject.GetComponentInChildren<Image>();
            charaImage.sprite = characterData.sheet[charaID].cardImage;

            charaImage = deckCard[i].transform.Find("Frame").gameObject.GetComponent<Image>();
            if(SaverDatas.p_Data.team[i].rarity == 3)
            {
                charaImage.sprite = rarity3Frame;
            }
            else if(SaverDatas.p_Data.team[i].rarity == 2)
            {
                charaImage.sprite = rarity2Frame;
            }
            else
            {
                charaImage.sprite = rarity2Frame;
            }

            charaImage = deckCard[i].transform.Find("Icon").gameObject.GetComponent<Image>();
            if(SaverDatas.p_Data.team[i].type == 1)
            {
                charaImage.sprite = typeIcons[0];
            }
            else if (SaverDatas.p_Data.team[i].type == 2)
            {
                charaImage.sprite = typeIcons[1];
            }
            else if (SaverDatas.p_Data.team[i].type == 3)
            {
                charaImage.sprite = typeIcons[2];
            }
            else if (SaverDatas.p_Data.team[i].type == 4)
            {
                charaImage.sprite = typeIcons[3];
            }
            else
            {
                charaImage.sprite = typeIcons[0];
            }
        }
    }
}
