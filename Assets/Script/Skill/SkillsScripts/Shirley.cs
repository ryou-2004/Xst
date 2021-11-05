using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Skill/Shirley")]
public class Shirley : SkillBase
{
    [Header("元に戻るまでの秒数")]public float time;
    [SerializeField]private int atk;
    [SerializeField]private int hp;
    private int beforeAtk;
    private float beforeHp;
    private Attack attack;
    public override IEnumerator Skill(GameObject obj)
    {
        attack = obj.GetComponent<Attack>();
        beforeAtk = attack.attack;
        beforeHp = attack.hp;
        attack.attack += atk;
        attack.hp += hp;
        return ValueBefore(beforeAtk,beforeHp,obj);
    }
    public  IEnumerator ValueBefore(int atk,float hp,GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if (obj == null)
            yield break;
        attack.attack = atk;
        attack.hp = hp;
    }
}
