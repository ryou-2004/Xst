using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Data;

public class Move : MonoBehaviour
{
    public float speed;
    public float canMoveDistance;
    public bool movePossible;
    public float maxMoveDistance;
    public bool skill = false;
    public int moveCount;
    public float coolTime;
    public GameObject hpBar;
    public Vector3 hpPos;
    public Vector3 pos;
    public Image coolDownEffect;
    public float degree;

    private CharacterStatus status;

    private bool isFirst;
    private bool coolDown;
    public float maxSpeed;

    private Vector2 characterPos;
    private Vector2 objectPos;

    private const string allies = "Allies";
    private const string enemy = "Enemy";
    private const string main = "Main";
    private const string sub = "Sub";
    private const string red = "Red";
    private const string blue = "Blue";
    private const string verticalWall = "VerticalWall";
    private const string horizontalWall = "HorizontalWall";

    private Attack attack;
    private CircleCollider2D coolcolider;
    private void Start()
    {
        attack = gameObject.GetComponent<Attack>();
        maxMoveDistance = canMoveDistance;
        if (gameObject.tag == "Allies")
        {
            coolcolider = coolDownEffect.GetComponent<CircleCollider2D>();
            coolcolider.enabled = false;
        }
    }
    public void SetStatus()
    {
        CharacterStatus sta = GetComponent<CharacterStatus>();
        speed = maxSpeed = sta.character.speed;
        coolTime = sta.character.coolTime;
        status = sta;

    }
    private void Update()
    {
        if (hpBar != null)
        {
            pos = transform.position;
            pos.z = 0;
            transform.position = pos;
            hpBar.transform.position = pos + hpPos;//HPバーの設定

            if (gameObject.tag == "Allies")
            {
                Vector3 coolPos = new Vector3(pos.x, pos.y, pos.z - 5);
                coolDownEffect.transform.position = coolPos;//coolDownEffectの設定

            }

        }
        if (movePossible)
        {
            Vector3 velocity = transform.rotation * new Vector3(0, speed, 0);
            transform.position += velocity * Time.deltaTime;
            canMoveDistance -= Time.deltaTime;
        }//移動処理
        if (canMoveDistance <= 0)
        {
            canMoveDistance = maxMoveDistance;//移動距離初期化
            movePossible = false;
            speed = maxSpeed;
            pos = transform.position;
            pos.z = 0;
            transform.position = pos;

            if (gameObject.tag == "Allies")
            {
                coolDown = true;
            }


            if (status == null)
                status = GetComponent<CharacterStatus>();
            status.skillCtrl.MoveEnd(this.gameObject);//クロスオーバー終了

            if (status.isCPU && !attack.isfight)
                StartCoroutine(status.cpuCtrl.Operation(this.gameObject));//移動終了時
        }
        if (coolDown)
        {
            if (coolDownEffect.fillAmount >= 1)
            {
                coolcolider.enabled = true;
                coolDownEffect.enabled = true;
            }
            coolDownEffect.fillAmount -= Time.deltaTime / coolTime;
            if (coolDownEffect.fillAmount <= 0)
            {
                coolDown = false;
                coolDownEffect.fillAmount = 1;
                coolDownEffect.enabled = false;
                coolcolider.enabled = false;
            }
        }

    }
    public void MoveStart(float angle)
    {
        movePossible = true;
        angle += 180;
        transform.eulerAngles = new Vector3(0, 180, angle);
        degree = angle;
        if (moveCount >= 1)
        {
            skill = true;
        }
        if (!isFirst)
        {
            isFirst = true;
            CharacterController.move = true;
        }
        if (moveCount <= 2)
        {
            moveCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case allies://当たった相手がPlayer側
                {
                    if (gameObject.CompareTag(enemy))//このゲームオブジェクトがCPU側
                    {
                        if (movePossible)
                            FightStart();
                    }
                    else if (gameObject.CompareTag(allies))//このゲームオブジェクトがPlayer側
                    {
                        if (!movePossible) return;
                        Move c_move = collision.gameObject.GetComponent<Move>();
                        degree = OperationUnit.degree;
                        movePossible = false;
                        c_move.canMoveDistance = 0.5f;
                        c_move.MoveStart(degree);
                    }
                    break;
                }
            case enemy://当たった相手がCPU側
                {
                    if (gameObject.CompareTag(allies))//このゲームオブジェクトがPlayer側
                    {
                        FightStart();
                    }
                    break;
                }
            case blue + main:
            case blue + sub://青の城に当たったら
                {
                    if (gameObject.CompareTag(enemy))//赤が青の城に当たった
                    {
                        EnemyCastleCollision(collision);
                    }
                    else//青が赤の城に当たった
                    {
                        AlliesCastleCollision(collision);
                    }
                    break;
                }
            case red + main:
            case red + sub://赤の城に当たったら
                {
                    if (gameObject.CompareTag(allies))//青が赤に当たったら
                    {
                        EnemyCastleCollision(collision);
                        var castleAttack = collision.GetComponent<Attack>();
                        if (!castleAttack.attackOpponent.Contains(this.gameObject))//なかったら
                        {
                            castleAttack.attackOpponent.Add(this.gameObject);
                        }
                        CPUCreate.isCastleAttack = true;
                    }
                    else//赤が青にあったら
                    {
                        AlliesCastleCollision(collision);
                    }
                    break;
                }
            case horizontalWall:
                {
                    status.skillCtrl.MoveEnd(this.gameObject);//クロスオーバー終了
                    Reflect(180);
                    break;
                }
            case verticalWall:
                {
                    status.skillCtrl.MoveEnd(this.gameObject);//クロスオーバー終了
                    Reflect(360);
                    break;
                }
        }
        void FightStart()
        {
            if (!movePossible) return;
            attack.FightStart(collision.gameObject);
            collision.transform.GetComponent<Attack>().FightStart(this.gameObject);
            movePossible = false;
        }
    }
    private void AlliesCastleCollision(Collider2D collision)
    {
        status.skillCtrl.MoveEnd(this.gameObject);
        Vector2 objVec = collision.gameObject.transform.position;
        Vector2 castleOffset = collision.gameObject.GetComponent<CapsuleCollider2D>().offset;
        degree = OperationUnit.degree;
        characterPos = new Vector3(transform.position.x, transform.position.y);
        objectPos = new Vector3(objVec.x - castleOffset.x, objVec.y - castleOffset.y);
        Vector2 dt = objectPos - characterPos;
        float rad = Mathf.Atan2(dt.x, dt.y);
        float angle = rad * Mathf.Rad2Deg;
        angle += 90;
        if (angle > 45 && angle < 135)
        {
            degree = angle - degree + 180 - angle;
        }
        if (angle > 225 && angle < 315)
        {
            degree = angle - degree + 180 - angle;
        }
        else
        {
            degree = angle - degree - angle;
        }

        MoveStart(degree);
        OperationUnit.degree = degree;
    }
    private void EnemyCastleCollision(Collider2D collision)
    {
        attack.FightStart(collision.gameObject);
        movePossible = false;
    }
    private void Reflect(int angle)
    {
        degree = OperationUnit.degree;
        degree = angle - degree;
        MoveStart(degree);
        OperationUnit.degree = degree;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case enemy:
                {
                    if (gameObject.CompareTag(allies))//自分のキャラでなければ                    
                    {
                        if (attack.opponent != null && attack.opponent.GetComponent<Attack>())
                        {
                            attack.opponent.GetComponent<Attack>().FightEnd();
                        }
                        else
                            print("呼ばれてない");
                    }
                    break;
                }
            case allies:
                {
                    if (gameObject.CompareTag(enemy))//自分のキャラでなければ                    
                    {
                        if (attack.opponent != null && attack.opponent.GetComponent<Attack>())
                        {

                            attack.opponent.GetComponent<Attack>().FightEnd();
                        }
                        else
                            print("呼ばれてない");
                    }
                    break;
                }
            case red + main:
            case red + sub:
                {
                    if (this.gameObject.CompareTag(allies))
                    {
                        var castleAttack = collision.GetComponent<Attack>();
                        if (castleAttack.attackOpponent.Contains(this.gameObject))//なかったら
                        {
                            castleAttack.attackOpponent.Remove(this.gameObject);
                        }
                        if (castleAttack.attackOpponent.Count == 0)
                        {
                            CPUCreate.isCastleAttack = false;
                        }

                    }
                    attack.FightEnd();
                    break;
                }//自分の城でなければ
            case blue + main:
            case blue + sub:
                {

                    attack.FightEnd();
                    break;
                }
        }
    }

}