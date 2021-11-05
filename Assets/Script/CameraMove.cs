using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public Slider slider;

    private Vector3 cameraPos;
    private void Start()
    {
        cameraPos = new Vector3(0, 0, -30);
        Camera.main.transform.position = new Vector3(0, -5, -30);//Player1のカメラの位置
    }
    private void Update()
    {
        cameraPos.y = slider.value;
        Camera.main.transform.position = cameraPos;
    }
}