using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    public List<CapsuleCollider2D> blueCastle;
    public List<Attack> redCastle;
    public static List<Attack> redCastle_static;
    public static List<GameObject> playerChar = new List<GameObject>();
    public static List<CapsuleCollider2D> blueCastle_static;//0.1サブ2メイン
    private void Start()
    {
        redCastle_static = redCastle;
        blueCastle_static = blueCastle;
        playerChar = new List<GameObject>();
    }
    public IEnumerator Operation(GameObject unit)
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        if (unit.GetComponent<Attack>())
        {
            if (unit.GetComponent<Attack>().isfight) yield break;
        }
        if (unit == null) yield break;
        CPUCreate.isCreate = true;
        if (CPUCreate.isCastleAttack)//城が攻撃されていたら
        {
            for (int i = 0; i < 3; i++)
            {
                if (0 < redCastle_static[i].attackOpponent.Count)
                {
                    MoveStart(unit, redCastle_static[i].attackOpponent[0]);
                    //城を攻撃しているやからに向かって移動
                }
            }
        }
        else if (0 < playerChar.Count)//プレイヤーのキャラ数が0以上なら
        {
            float nearDis = 0;
            GameObject target = null;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Allies"))
            {
                float dis = Vector3.Distance(enemy.transform.position, unit.transform.position);
                if (nearDis == 0 || nearDis > dis)
                {
                    nearDis = dis;
                    target = enemy;
                }
            }
            if (target != null&&unit!=null)
                MoveStart(unit, target);
            //プレイヤーのキャラに向かって移動
        }
        else
        {
            GameObject target = null;
            if (blueCastle_static[0] != null && blueCastle_static[1] != null)
            {
                target = blueCastle_static[Random.Range(0, 2)].gameObject;
            }
               
            else if (blueCastle_static[0] == null && blueCastle_static[1] != null)
                target = blueCastle_static[1].gameObject;
            else if (blueCastle_static[0] != null && blueCastle_static[1] == null)
                target = blueCastle_static[0].gameObject;
            else
                target = blueCastle_static[2].gameObject;
            if (target != null)
                MoveStart(unit, target.gameObject);
            //敵の城に向かって移動
        }
    }
    private void MoveStart(GameObject instance, GameObject target)
    {
        if (instance == null) return;
        else if (target == null)
            StartCoroutine(Operation(instance));
        instance.transform.eulerAngles = new Vector3(0, 180, OperationUnit.GetAngle(instance.transform.position, target.transform.position));
        instance.GetComponent<Move>().movePossible = true;
    }

}