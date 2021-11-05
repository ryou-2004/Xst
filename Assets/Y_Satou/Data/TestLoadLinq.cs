using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestLoadLinq : MonoBehaviour
{
    [SerializeField] CharacterData data;

    void Start()
    {
        var chara1 = data.sheet
            .Where(x => x.Id == 1);

        foreach(var x in chara1) { Debug.Log(x.Id); }
    }
}
