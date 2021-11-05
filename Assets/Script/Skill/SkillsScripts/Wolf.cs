using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Wolf")]
public class Wolf : SkillBase
{
    public float damege;
    public override IEnumerator Skill(GameObject obj)
    {
        string str = null;
        if (obj.tag == "Allies")
            str = "Enemy";
        else
            str = "Allies";
        List<GameObject> charList = GameObject.FindGameObjectsWithTag(str).ToList();//Character取得
        //charList.Where(x => x.GetComponent<Photon.Pun.PhotonView>().IsMine == false);//自分以外が生成したキャラクター

        if (charList.Any())
        {
            GameObject nearObj = charList.First();
            Vector3 nearPos = obj.transform.position - nearObj.transform.position;
            foreach (GameObject compareObj in charList.Skip(1))
            {
                Vector3 comparePos = obj.transform.position - compareObj.transform.position;
                if (nearPos.x > comparePos.x && nearPos.y > comparePos.y)
                {
                    nearObj = compareObj;
                    nearPos = obj.transform.position - nearObj.transform.position;
                }
            }
            Attack attack = nearObj.GetComponent<Attack>();
            Move move = nearObj.GetComponent<Move>();
            Slider hpBar = move.hpBar.GetComponent<Slider>();
            if ((attack.hp - damege) <= 0)
            {
                if (nearObj.GetComponent<CharacterStatus>())
                {
                    //CPU死亡処理
                    if (attack.status.isCPU)
                    {
                        CPUCreate.cpuCount--;
                    }
                }
                CPUController.playerChar.Remove(nearObj.gameObject);
                Destroy(nearObj.GetComponent<Move>().hpBar);
                Destroy(nearObj);
            }
            attack.hp -= damege;
            hpBar.value = attack.hp / (attack.maxHp / hpBar.maxValue);
        }
        yield break;
    }
}
