using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitialize
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("After Scene is loaded and game is running");
        //スクリーンサイズの指定
        Screen.SetResolution(576, 1024, false);
    }
}