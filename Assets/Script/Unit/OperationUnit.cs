using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationUnit : MonoBehaviour
{
    private static GameObject linePrefub;
    private static GameObject moveLine;
    private static GameObject moveUnit;
    private static GameObject beforeMoveUnit;
    private static LineRenderer line;
    private static Vector2 posA, posB;
    public static float degree;
    private void Start()
    {
        linePrefub = (GameObject)Resources.Load("LineRenderer");//Path指定
        moveLine = null;
        moveUnit = null;
        beforeMoveUnit = null;
    }
    private void Update()
    {
        if (ClickController.MoveUnitChoice)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            posB = new Vector2(mousePos.x, mousePos.y);
            line.SetPosition(1, mousePos);
        }//Unitを押しているときの処理
    }
    public static void MoveUnitChoiceDown(GameObject unit)
    {
        moveUnit = beforeMoveUnit = unit;
        moveLine = Instantiate(linePrefub, Vector3.zero, Quaternion.identity);
        line = moveLine.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, moveUnit.transform.position);
        line.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1)));
        posA = new Vector2(moveUnit.transform.position.x, moveUnit.transform.position.y);
    }//Unitを押したときの処理
    public void MoveUnitChoiceUp()
    {
        if (moveUnit != null)
        {
            var status = moveUnit.GetComponent<CharacterStatus>();
            status.skillCtrl.MoveEnd(beforeMoveUnit);
            status.skillCtrl.MoveStart(moveUnit);//クロスオーバー開始
            float angle = GetAngle(posA, posB);
            moveUnit.GetComponent<Move>().MoveStart(angle);//移動開始;
        }
        Destroy(moveLine);//LineRenderer削除
    }//Unitから離したときの処理
    public static float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.x, dt.y);
        degree = rad * Mathf.Rad2Deg;
        if (degree < 0)
        {
            degree += 360;
        }
        if (degree > 0)
        {
            degree -= 360;
        }
        return degree;
    }//2点間の角度取得
}