using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public int attack;
    public float hp;
    public float maxHp;
    public float maxInterval;
    public float cost;
    public float canAtkDistance;
    public bool safe;
    [HideInInspector] public GameObject opponent;
    private float interval;
    [HideInInspector] public bool isfight = false;
    [HideInInspector] public List<GameObject> attackOpponent;//自分のことを攻撃しているオブジェクト
    public CharacterStatus status;
    private bool subBreak;
    public Slider castleHpBar;
    public bool isBreak;
    public void SetStatus()
    {
        CharacterStatus sta = GetComponent<CharacterStatus>();
        attack = sta.character.atk;
        hp = sta.character.hp;
        maxHp = sta.character.hp;
        maxInterval = sta.character.interval;
        cost = sta.character.cost;
        status = sta;
        GetComponent<CircleCollider2D>().radius = canAtkDistance;
    }
    private void Update()
    {
        if (hp <= 0)
        {
            GameManeger gameManeger = GameObject.FindWithTag("GameController").GetComponent<GameManeger>();
            if (gameObject.tag.Contains("Main"))//自分がMainCastleで
            {
                int playerNumber = 0;
                if (gameObject.tag.Contains("Blue"))//自分が青城だったら
                {
                    GetComponent<SpriteRenderer>().sprite = gameManeger.blueMainBreak;
                    playerNumber = 2;//プレイやー2の勝利
                }
                else if (gameObject.tag.Contains("Red"))//敵の城だったら
                {
                    GetComponent<SpriteRenderer>().sprite = gameManeger.redMainBreak;
                    playerNumber = 1;//プレイヤー1の勝利
                }
                GameManeger.End(playerNumber);
                //スプライト変更
            }
            else if (gameObject.tag.Contains("Sub") && !subBreak && isBreak)
            {
                if (gameObject.tag.Contains("Blue"))//自分が青城だったら
                {
                    GetComponent<SpriteRenderer>().sprite = gameManeger.blueSubBreak;
                    GameManeger.enemyBreakCastle += 1;
                    GameManeger.Situation(-2f);
                    print("SubBreak");
                }
                else if (gameObject.tag.Contains("Red"))//敵の城だったら
                {
                    GameManeger.myBreakCastle += 1;
                    GameManeger.Situation(2f);
                    GetComponent<SpriteRenderer>().sprite = gameManeger.redSubBreak;
                }
                subBreak = true;
            }
            CPUCreate.isCastleAttack = false;
        }
        if (isfight)
        {
            interval -= Time.deltaTime;
            if (opponent == null)
            {
                FightEnd();
                return;
            }
            else if (interval <= 0)
            {
                interval = maxInterval;
                BattleController.Battle(opponent, this.gameObject, attack);
            }//攻撃
        }
    }
    public void FightStart(GameObject opponent)
    {
        isfight = true;
        this.opponent = opponent;
    }
    public void FightEnd()
    {
        isfight = false;
        interval = maxInterval;
        if (gameObject.tag.Contains("Blue") || gameObject.tag.Contains("Red"))
        {
            return;
        }
        else if (gameObject.GetComponent<CharacterStatus>().isCPU)
            StartCoroutine(status.cpuCtrl.Operation(this.gameObject));//戦闘終了時
    }
}