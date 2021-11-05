using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Skill/Fureria")]
public class Fureria : SkillBase
{
    [Header("元に戻るまでの秒数")] public float time;
    private Attack attack;
    public override IEnumerator Skill(GameObject obj)
    {
        attack = obj.GetComponent<Attack>();
        attack.safe = true;

        return safeEnd(obj);
    }
    public IEnumerator safeEnd(GameObject obj)
    {
        yield return new WaitForSeconds(time);
        if (obj == null)
            yield break;
        attack.safe = false;
    }
}
