using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public static bool CreateUnitClick { get; set; }
    public static bool MoveUnitChoice { get; set; }
    private static RaycastHit2D hit;
    private GameObject gameController;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, 50);
            if (hit.collider != null)
            {
                print(hit.collider.tag);
                print(hit.collider.transform.position.z);
                if (hit.collider.tag == "coolDown")
                {
                    print("クールダウンに触ってます");
                    return;
                }
                else if (hit.collider.CompareTag("Allies"))//自分のキャラクターだったら
                {
                    print("味方に触ってます");
                    OperationUnit.MoveUnitChoiceDown(hit.transform.gameObject);//ユニットを動かす
                    MoveUnitChoice = true;
                    hit.transform.GetComponent<Move>().movePossible = false;
                }
            }     
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (MoveUnitChoice)
            {
                MoveUnitChoice = false;
                gameController.GetComponent<OperationUnit>().MoveUnitChoiceUp();
            }
        }
    }
}
