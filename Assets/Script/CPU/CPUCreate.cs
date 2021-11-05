using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUCreate : MonoBehaviour
{
    public GameObject character;
    public float speed;
    public GameObject hpBar;
    public GameObject hpBarParent;
    public CharacterData imageData;
    public CharacterSkillData skillData;
    public CPUController cpuCtrl;

    private List<CharacterDatas> team;
    private List<CharacterDatas> handcard = new List<CharacterDatas>();

    private float currentCost;
    private Vector3 generatePos = new Vector3(0, 25.5f, 0);
    private Vector3 hpPos = new Vector3(0, 23, 0);

    public static List<Attack> castle = new List<Attack>();

    public static bool isCastleAttack;
    public static bool isCreatePossible;
    private int createNumber;

    public static bool isCreate;
    public static int stageNumber;

    private bool handcardDuplicate;
    private int value;

    public static int cpuCount;

    private int Value
    {
        set
        {
            if (5 < value)
                value = 0;
            this.value = value;
        }
        get { return value; }
    }//0～5で次の手札の番号
    private void Start()
    {
        cpuCount = 0;

        handcardDuplicate = true;
        var s = SaverDatas.c_Datas;
        switch (stageNumber)
        {
            case 1:
                {
                    team = new List<CharacterDatas>() { s[0], s[0], s[5], s[5], s[5], s[5] };
                    break;
                }
            case 2:
                {
                    team = new List<CharacterDatas>() { s[3], s[3], s[4], s[4], s[5], s[5] };
                    break;
                }
            case 3:
                {
                    team = new List<CharacterDatas>() { s[0], s[1], s[2], s[3], s[4], s[5] };
                    handcardDuplicate = false;
                    break;
                }
        }
        isCastleAttack = isCreatePossible = false;
        isCreate = true;
        currentCost = 0;
        //team = SaverDatas.p_Data.team.ToList();
        for (int i = 0; i < 6; i++)
        {
            team[i].icon = imageData.sheet[team[i].id].Icon;
        }

        team = team.OrderBy(i => Guid.NewGuid()).ToList();

        for (int i = 0; i < 3; i++)
        {
            handcard.Add(team[i]);
        }
        Value = 3;
        castle.Add(GameObject.FindWithTag("RedMain").GetComponent<Attack>());
        var v = GameObject.FindGameObjectsWithTag("RedSub").Select(x => x.GetComponent<Attack>()).ToList();
        castle.Add(v[0]);
        castle.Add(v[1]);
        cpuCtrl = GameObject.FindWithTag("CPUController").GetComponent<CPUController>();
    }
    private GameObject Create(int handCardNumber)//IDは手札0～2で
    {
        CharacterDatas data = handcard[handCardNumber];
        GameObject instance = Instantiate(character, generatePos, Quaternion.identity);
        instance.tag = "Enemy";
        instance.name = data.name;
        var s = instance.GetComponentInChildren<SpriteRenderer>();
        s.sprite = data.icon;
        s.flipX = s.flipY = true;
        instance.GetComponent<CharacterStatus>().SetStatus(data, skillData.sheet[data.id].skill, true);//ステータス設定

        GameObject hP = Instantiate(hpBar, hpPos, Quaternion.identity, hpBarParent.transform);
        hP.gameObject.name = "HPSliderBefore";

        instance.GetComponent<Move>().hpPos = hP.transform.position - instance.transform.position;
        instance.GetComponent<Move>().hpBar = hP;

        currentCost -= data.cost;

        handcard[handCardNumber] = team[Value];
        Value++;
        if (!handcardDuplicate)
            Check();
        void Check()
        {
            if (handcard.Contains(team[Value]))
            {
                Value++;
                Check();
            }
        }//手札回し
        return instance;
    }
    private void Update()
    {
        if (GameManeger.gameNow)
            currentCost += Time.deltaTime / speed;
        currentCost = Mathf.Clamp(currentCost, 0, 10);

        int cost = Mathf.CeilToInt(currentCost);//切り捨てしたコスト;
        var createPossibleNumber = handcard.Where(x => x.cost < cost).ToList();//生成可能なキャラだけのlist
        if (createPossibleNumber.Count == 3)//生成可能なキャラが1以上あったら
        {
            createNumber = createPossibleNumber.IndexOf(createPossibleNumber[UnityEngine.Random.Range(0, createPossibleNumber.Count)]);
            isCreatePossible = true;
        }
        else//生成可能なキャラが1未満だったら
            isCreatePossible = false;

        if (isCreatePossible && GameManeger.gameNow && isCreate && cpuCount <= 3)//生成可能なら
        {
            cpuCount++;

            GameObject unit = Create(createNumber);//キャラクター生成
            StartCoroutine(cpuCtrl.Operation(unit));//生成時移動
            isCreate = false;
        }
    }
}