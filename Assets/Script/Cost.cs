using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Cost : MonoBehaviour
{
    public string Player { get; set; }
    public float currentCost;
    public GameObject[] handCardPanel;
    public Text costText;
    public static List<int> handCardCost = new List<int>();
    public Slider s;
    public static Slider slider;
    private int speed;

    private void Awake()
    {
        slider = s;
    }
    private void Start()
    {
        currentCost = 5;
        slider.value = 5;
        slider.maxValue = 10;
        speed = 5; 
    }
    private void Update()
    {
        currentCost += Time.deltaTime / speed;
        slider.value = currentCost + 0.05f;

        currentCost = Mathf.Clamp(currentCost, 0, 10);
        costText.text = Mathf.FloorToInt(currentCost).ToString();
        HandCardPanelCheck();
    }
    public  void HandCardPanelCheck()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Mathf.FloorToInt(currentCost) == i)
            {
                for (int l = 0; l < 3; l++)
                {
                    if (i + 1 <= handCardCost[l])
                        handCardPanel[l].SetActive(true);
                    else
                        handCardPanel[l].SetActive(false);
                }
                break;
            }
        }
    }
}