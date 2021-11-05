using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuStatusScript : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI ticketText;
    public Slider expSlider;

    PlayerData copyData;
    private void Start()
    {
        copyData = null;
    }
    private void Update()
    {
        if(copyData != SaverDatas.p_Data)
        {
            rankText.text = $"{SaverDatas.p_Data.userRank}";
            nameText.text = SaverDatas.p_Data.userName;
            moneyText.text = $"{SaverDatas.p_Data.userMoney}";
            ticketText.text = $"{SaverDatas.p_Data.userTicket}";
            expSlider.value = SaverDatas.p_Data.userExp;
            if(SaverDatas.p_Data.userExp == 0)
            {
                expSlider.minValue = 0;
            }
            else
            {
                expSlider.minValue = -25;
            }
            expSlider.maxValue = 100;

            copyData = SaverDatas.p_Data;
        }
    }
}
