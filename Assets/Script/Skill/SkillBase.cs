using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class SkillBase : ScriptableObject
{
    //自分に対して
    public virtual IEnumerator Skill(GameObject obj) { yield break; }
    public virtual IEnumerator Skill(GameObject obj,string tag) { yield break; }
}