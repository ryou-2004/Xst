using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="ScriptableObject/CreateSkillTest")]
public class CharacterSkillData : ScriptableObject
{
    public List<Data> sheet;

    [System.Serializable]
    public class Data
    {
        public SkillBase skill;
    }
}
