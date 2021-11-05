using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public CharacterDatas character { get; set; }
    public SkillBase skill { get; set; }
    public SkillController skillCtrl;
    public CPUController cpuCtrl;
    public bool isCPU;
    public void SetStatus(CharacterDatas data, SkillBase skill,bool isCPU)
    {
        cpuCtrl = GameObject.FindWithTag("CPUController").GetComponent<CPUController>();
        character = data;
        this.skill = skill;
        GetComponent<Move>().SetStatus();
        GetComponent<Attack>().SetStatus();
        this.isCPU = isCPU;
        if (isCPU)
            skillCtrl = GameManeger.enemySkill;
        else
            skillCtrl = GameManeger.alliesSkill;
    }
}