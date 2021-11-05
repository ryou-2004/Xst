using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class BattleController : MonoBehaviour
{
    private static CastleController castleController;

    private void Start()
    {
        castleController = GetComponent<CastleController>();
    }
    public static void Battle(GameObject opponent, GameObject mine, float damage)
    {
        Slider hpBar = null;
        Attack attack = opponent.GetComponent<Attack>();
        if (opponent.CompareTag("Allies") || opponent.CompareTag("Enemy"))//キャラクターだったら
        {
            if (!attack.safe)//無敵状態じゃなければ
            {
                if ((attack.hp - damage) <= 0)
                {

                    if (opponent.GetComponent<CharacterStatus>())
                    {
                        //CPU死亡処理
                        if (attack.status.isCPU)
                        {
                            CPUCreate.cpuCount--;
                        }
                    }
                    CPUController.playerChar.Remove(opponent.gameObject);
                    Destroy(opponent.GetComponent<Move>().hpBar);
                    Destroy(opponent.GetComponent<Move>().coolDownEffect);
                    Destroy(opponent);
                }
                attack.hp -= damage;//ダメージを与える
                if(opponent!=null)
                {
                    hpBar = opponent.GetComponent<Move>().hpBar.GetComponent<Slider>();
                    hpBar.value = attack.hp / (attack.maxHp / hpBar.maxValue);//HPバー削る
                } 
            }
        }
        else if(opponent.tag.Contains("Main")|| opponent.tag.Contains("Sub"))
        {
            
            if ((attack.hp - damage) <= 0)
            {
                attack.isBreak = true;
                Destroy(opponent.GetComponent<CapsuleCollider2D>());
                attack.castleHpBar.GetComponent<Slider>().value = 0;
                Destroy(attack.castleHpBar);
            }
            attack.hp -= damage;//ダメージを与える
            if (!attack.isBreak)
            {
                hpBar = attack.castleHpBar.GetComponent<Slider>();
                hpBar.value = attack.hp / (attack.maxHp / hpBar.maxValue);//HPバー削る
            }
        }
    }
}