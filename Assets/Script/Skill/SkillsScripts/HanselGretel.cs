using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Skill/HanselGretel")]
public class HanselGretel : SkillBase
{
    [Header("元に戻るまでの秒数")]
    [SerializeField] private float time;
    [SerializeField] private float hp_default = 28;

    [Header("グレーテルのステータス")]
    [SerializeField] private Sprite img_Hansel;
    [SerializeField] private Sprite img_Gretel;
    [SerializeField] private int g_hp = 32;
    [SerializeField] private int g_atk = 7;
    [SerializeField] private int g_speed = 11;

    private Attack attack;
    private Move move;
    private int beforeAtk;
    private float beforeHp;
    private float beforeSpeed;
    public override IEnumerator Skill(GameObject obj)
    {
        var image = obj.transform.Find("Empty").GetComponent<SpriteRenderer>();
        image.sprite = img_Gretel;
        attack = obj.GetComponent<Attack>();
        beforeAtk = attack.attack;
        beforeHp = attack.hp;
        move = obj.GetComponent<Move>();
        beforeSpeed = move.speed;

        attack.attack = g_atk;
        move.speed = g_speed;
        var damage = attack.hp - hp_default;
        attack.hp = g_hp - damage;
        return ValueBefore(beforeAtk, beforeHp, beforeSpeed, obj);
    }
    public IEnumerator ValueBefore(int atk, float hp, float speed, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if (obj == null)
            yield break;
        var image = obj.transform.Find("Empty").GetComponent<SpriteRenderer>();
        image.sprite = img_Hansel;
        attack.attack = atk;
        move.speed = speed;
        var damege = 32 - attack.hp;
        if(damege > 27)
        {
            attack.hp = 5;
        }
        else
        {
            attack.hp = hp - damege;
        }
        
    }
}
