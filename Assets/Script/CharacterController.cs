using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class CharacterController : MonoBehaviour
{
    public GameObject character;
    public Image[] cardImage;
    public Image nextCardImage;
    public Text[] costText;
    public CharacterData charaData;

    public CharacterData data;
    public CharacterSkillData skillData;
    public Vector3 generatePos;
    public Text cost_text;
    public Slider slider;
    public static bool move;
    public List<GameObject> button_fieldCard;
    public GameObject instance;
    public GameObject HpBar;
    public Image coolDownEffect;
    public GameObject canvas;

    private int value;
    private int Value
    {
        set
        {
            if (5 < value)
                value = 0;
            this.value = value;
        }
        get { return value; }
    }
    private bool createUnitClick;
    private int id;

    private Transform parent;//なんのparentだよボケカス
    private Vector3 cameraPos;
    private Vector3 hpPos;
    private List<int> handcard = new List<int>();//手札
    private List<CharacterDatas> team;
    private Cost cost;

    private void Start()
    {
        move = false;
        createUnitClick = true;
        GameObject gameController = GameObject.Find("GameController");

        team = SaverDatas.p_Data.team.ToList();//キャラクターデッキ取得
        team = team.OrderBy(i => Guid.NewGuid()).ToList();//ランダムにする
        Cost.handCardCost = new List<int>();
        for (int i = 0; i < button_fieldCard.Count; i++)
        {
            int ii = i;
            Cost.handCardCost.Add(team[i].cost);
            cardImage[i].sprite = data.sheet[team[i].id].cardImage;//カードイメージ設定
            costText[i].text = team[i].cost.ToString();//コストテキスト設定
            button_fieldCard[i].GetComponent<Button>().onClick.AddListener(() => C(ii));//クリックしたときの処理設定
            handcard.Add(i); //手札追加
        }//クリック時の処理
        nextCardImage.sprite = data.sheet[team[3].id].cardImage;//次のカードイメージの設定
        Value = 3;//手札のindexまわしとく
        cost = GetComponent<Cost>();
        //プレイヤー１の値
        cameraPos = new Vector3(0, -5, -30);
        generatePos = new Vector3(0, -7.5f, 0);
        hpPos = new Vector3(0, -5, 0);

        parent = canvas.transform;
    }
    private void C(int buttonNumber)
    {
        if (createUnitClick)//生成できる状態だったら
        {
            id = handcard[buttonNumber];
            createUnitClick = false;

            instance = Instantiate(character, generatePos, Quaternion.identity);
            instance.tag = "Allies";        
                //s.flipX = s.flipY = false;//キャラクターの絵のFlip(Prefabを変える

            var bar = Instantiate(HpBar, hpPos, Quaternion.identity, parent);
            bar.gameObject.name = "HPSliderBefore";
            bar.GetComponent<Slider>().value = bar.GetComponent<Slider>().maxValue;
            instance.GetComponent<Move>().hpPos = bar.transform.position-instance.transform.position;
            instance.GetComponent<Move>().hpBar = bar;

            var coolDown = Instantiate(coolDownEffect, generatePos, Quaternion.identity, parent);
            instance.GetComponent<Move>().coolDownEffect = coolDown;
            coolDown.enabled = false;

            team[id].icon = data.sheet[team[id].id].Icon;//iconの設定
            var status = instance.GetComponent<CharacterStatus>();//ステータス設定
            status.character = team[id];
            status.skill = skillData.sheet[team[id].id].skill;
            status.SetStatus(team[id], skillData.sheet[team[id].id].skill, false); ;
            instance.name = team[id].name;
            instance.GetComponentInChildren<SpriteRenderer>().sprite = team[id].icon;

            slider.value = cameraPos.y;
            Camera.main.transform.position = cameraPos;//カメラの座標初期化

            StartCoroutine(Process());//移動開始まで待機

            IEnumerator Process()
            {
                yield return new WaitUntil(() => move == true);
                if (cost.currentCost < team[id].cost)
                {
                    //cost_text.text = "コスト足らんよ！";
                }
                else
                {
                    cost.HandCardPanelCheck();
                    CPUController.playerChar.Add(instance);
                    cost.currentCost -= team[id].cost;//コスト除算
                    Cost.slider.value -= team[id].cost;

                    cardImage[buttonNumber].sprite = data.sheet[team[Value].id].cardImage;//カードイメージ設定
                    costText[buttonNumber].text = team[Value].cost.ToString();//コストテキスト設定
                    handcard[buttonNumber] = Value;
                    Cost.handCardCost[buttonNumber] = team[Value].cost;

                    Value++;
                    Check();
                    void Check()
                    {
                        if (handcard.Contains(Value))
                        {
                            Value++;//手札を回す
                            Check();//再度チェック
                        }//手札が被っていたら
                        else
                            nextCardImage.sprite = data.sheet[team[Value].id].cardImage;//NextCardImage設定
                    }
                }
                createUnitClick = true;
                move = false;
            }
        }
    }
}