using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "ScriptableObject/Skill/Rapunzel")]
public class Rapunzel : SkillBase
{
    public Vector3 nearRange;
    public float recover;
    public override IEnumerator Skill(GameObject obj)
    {
        nearRange = new Vector3(3, 3, 1);
        recover = 8;
        string str = obj.tag;

        List<GameObject> charList = GameObject.FindGameObjectsWithTag(str).ToList();//Character取得
        //charList.Where(x => x.GetComponent<Photon.Pun.PhotonView>().IsMine == false);//自分以外が生成したキャラクター
        if (charList.Count < 0)
            foreach (GameObject chara in charList)
            {
                Vector3 comparePos = obj.transform.position - chara.transform.position;
                if (comparePos.x > nearRange.x && comparePos.y > nearRange.y)
                {
                    charList.Remove(chara);
                }
            }
        foreach (GameObject chara in charList)
        {
            Attack attack = chara.GetComponent<Attack>();
            Move move = chara.GetComponent<Move>();
            Slider hpBar = move.hpBar.GetComponent<Slider>();
            attack.hp += recover;
            hpBar.value = attack.hp / (attack.maxHp / hpBar.maxValue);
        }
        yield break;
    }
}