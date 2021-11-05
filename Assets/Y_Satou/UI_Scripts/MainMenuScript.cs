using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject[] menuObject;
    [SerializeField] private GameObject[] backGroundObject;
    [SerializeField] private GameObject[] buttonObject;

    [Header("表示されている時のボタンの色")]
    [SerializeField] private Color color_ButtonOn;
    [Header("表示されていない時のボタンの色")]
    [SerializeField] private Color color_ButtonOff;

    [Header("フェード イン・アウトの設定")]
    [SerializeField] Animator fadeAnim;
    [SerializeField] private GameObject fadeObject;

    private int activeMenuNo = 100;
    private void Awake()
    {
        ClickMenuButton(0);
    }

    public void RoomScene()
    {
        SceneManager.LoadScene("Login");
    }
    public void ClickMenuButton(int menuNo)
    {
        //メニューの数よりボタンの数が多かった場合、返す
        if(menuNo > menuObject.Length || menuNo == activeMenuNo)
        {
            return;
        }
        //ボタンのSetActiveをoffに。フェード中に移行しないようにするため
        foreach(var button in buttonObject)
        {
            button.SetActive(false);
        }
        //フェードイン
        activeMenuNo = menuNo;
        //fadeObject.SetActive(true);
        StartCoroutine(Wait_Fade(menuNo));
        
    }
    IEnumerator Wait_Fade(int menuNo)
    {
        //fadeAnim.SetBool("is_Fade", true);
        yield return new WaitForSeconds(0.2f);
        
        //画面のSetActiveの初期化・ボタンの色の初期化
        for (var i = 0; i < menuObject.Length; i++)
        {
            menuObject[i].SetActive(false);
            backGroundObject[i].GetComponent<Image>().color = color_ButtonOff;
        }
        //指定したメニューのActiveをtrueに。ボタンの色も変更
        menuObject[menuNo].SetActive(true);
        backGroundObject[menuNo].GetComponent<Image>().color = color_ButtonOn;
        //fadeAnim.SetBool("is_Fade", false);
        yield return new WaitForSeconds(0.2f);
        //フェードアウト
        //fadeObject.SetActive(false);
        //offにしたボタンをtrueに。
        foreach (var button in buttonObject)
        {
            button.SetActive(true);
        }
    }
}
