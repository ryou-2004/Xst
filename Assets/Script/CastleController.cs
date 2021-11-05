using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CastleController : MonoBehaviour
{ 
//{
//    public Vector2 BlueMainCastle;
//    public Vector2 BlueSubCastle;
//    public Vector2 RedMainCastle;
//    public Vector2 RedSubCastle;
//    public Sprite blueMainCastle, blueSubCastle, redMainCastle, redSubCastle, redMainBreak, redSubBreak;
//    private const string castle = "Castle";
//    private const string player1 = "Player1";
//    private const string player2 = "Player2";
//    private const string main = "Main";
//    private const string sub = "Sub";
//    private void Start()
//    {
//        if (SimplePun.IsMaster)//マスターの処理
//        {
//            NetInstantiate(main + castle, BlueMainCastle, Quaternion.identity, blueMainCastle, player1 + main + castle);
//            NetInstantiate(sub + castle, BlueSubCastle, Quaternion.identity, blueSubCastle, player1 + sub + castle + "0");//右
//            NetInstantiate(sub + castle, new Vector2(BlueSubCastle.x * -1, BlueSubCastle.y), Quaternion.identity, blueSubCastle, player1 + sub + castle + "1");//左
//            view.RPC("CastleSpriteChange", RpcTarget.Others, player1);
//        }
//        else
//        {
//            List<GameObject> obj = new List<GameObject>();
//            obj.Add(NetInstantiate(main + castle, RedMainCastle, Quaternion.identity, blueMainCastle, player2 + main + castle));
//            obj.Add(NetInstantiate(sub + castle, RedSubCastle, Quaternion.identity, blueSubCastle, player2 + sub + castle + "0"));
//            obj.Add(NetInstantiate(sub + castle, new Vector2(RedSubCastle.x * -1, RedSubCastle.y), Quaternion.identity, blueSubCastle, player2 + sub + castle + "1"));
//            foreach (var v in obj)
//            {
//                v.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
//            }
//            obj[1].GetComponent<SpriteRenderer>().flipX = true;
//            obj[2].GetComponent<SpriteRenderer>().flipX = true;
//            view.RPC("CastleSpriteChange", RpcTarget.Others, player2);
//        }
//    }
//    public void ValueSync(string gameObjectName, int hp)
//    {
//        view.RPC("CastleValueSync", RpcTarget.Others, gameObjectName, hp);
//    }
//    [PunRPC]
//    public void CastleValueSync(string type, int hp)
//    {
//        GameObject.Find(type).GetComponent<Attack>().hp = hp;
//        print("PunRPC");
//    }
//    [PunRPC]
//    private void CastleSpriteChange(string name)
//    {
//        List<GameObject> list = new List<GameObject>();
//        Set(name + main + castle, redMainCastle, GameObject.Find(main + castle + "(Clone)"));
//        Set(name + sub + castle, redSubCastle, GameObject.Find(sub + castle + "(Clone)"));
//        Set(name + sub + castle, redSubCastle, GameObject.Find(sub + castle + "(Clone)"));
//        if (name == player1)
//            foreach (var v in list)
//            {
//                v.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
//            }
//        list[1].name = name + sub + castle + "0";
//        list[2].name = name + sub + castle + "1";
//        void Set(string playerName, Sprite sprite, GameObject obj)
//        {
//            obj.name = playerName;
//            obj.GetComponent<SpriteRenderer>().sprite = sprite;
//            list.Add(obj);
//        }
//    }
//    private GameObject NetInstantiate(string prefabName, Vector3 vector3, Quaternion quaternion, Sprite sprite, string name)
//    {
//        GameObject obj = PhotonNetwork.Instantiate(prefabName, vector3, quaternion);
//        obj.GetComponent<SpriteRenderer>().sprite = sprite;
//        obj.name = name;
//        return obj;
//    }
//    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//    {
//    }
}