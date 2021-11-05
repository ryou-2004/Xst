using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public GameObject secondHand;
    private float remainingTime = 300;
    private float count = 3;
    [SerializeField] private GameObject countdown;
    [SerializeField] private Text countDownText;
    private bool isCountDown = false;
    private float diameter;
    private void Start()
    {

        switch (CPUCreate.stageNumber)
        {
            case 1:
                {
                    remainingTime = 420;
                    break;
                }
            case 2:
                {
                    remainingTime = 420;
                    break;
                }
            case 3:
                {
                    remainingTime = 540;
                    break;
                }
        }
        diameter = 360 / remainingTime;
    }
    private void Update()
    {
        if (0 < count)//カウントダウンされてなかったら
        {
            count -= Time.deltaTime;
            CountDownAnimationStart();
        }
        else
        {
            remainingTime -= Time.deltaTime;
            secondHand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (remainingTime) * diameter));
            if (remainingTime <= 0)
            {
                GameManeger.End(0);
            }
        }
    }
    private void CountDownAnimationStart()
    {
        if (!isCountDown)
        {
            //アニメーションスタートはコ↑コ↓
            countdown.SetActive(true);
            StartCoroutine(falsecountDown());
            isCountDown = true;
        }
    }
    IEnumerator falsecountDown()
    {
        yield return new WaitForSeconds(0.85f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "START";
        yield return new WaitForSeconds(0.5f);
        countdown.SetActive(false);
        GameManeger.gameNow = true;//ゲーム開始
    }
}