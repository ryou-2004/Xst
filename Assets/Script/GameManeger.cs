using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class GameManeger : MonoBehaviour
{
    public SkillController allies;
    public SkillController enemy;
    public static SkillController alliesSkill;
    public static SkillController enemySkill;
    public static bool gameNow;//ゲーム中か否か
    public static int myBreakCastle;
    public static int enemyBreakCastle;
    public static bool win;
    public static Slider slider;
    public Slider s;
    public Sprite redMainBreak, redSubBreak, blueMainBreak, blueSubBreak;


    private void Awake()
    {
        slider = s;
    }
    private void Start()
    {
        gameNow = false;
        slider.maxValue = 10f;
        slider.value = 5f;
        alliesSkill = allies;
        enemySkill = enemy;
    }
    public static void End(int playerNumber)
    {
        gameNow = false;
        print("GameEnd");
        print($"player{playerNumber}の勝利");
        if (playerNumber == 1)
        {
            win = true;
            Load("WinResultScene");
        }
        else
        {
            Load("LoseResultScene");
            win = false;
        }
    }
    public static void Situation(float value)
    {
        slider.value += value;
    }
    private static void Load(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
    }
}
