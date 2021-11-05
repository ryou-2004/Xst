using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerPartScript : MonoBehaviour
{
    [SerializeField] private Animator lowerAnim;
    private bool isLowerPart;
    [SerializeField] private GameObject Arrow;

    void Start()
    {
        lowerAnim.SetBool("isDownUp", false);
        isLowerPart = false;
    }

    public void ClickKey()
    {
        if (!isLowerPart)
        {
            isLowerPart = true;
            lowerAnim.SetBool("isDownUp", true);
        }
        else if (isLowerPart)
        {
            isLowerPart = false;
            lowerAnim.SetBool("isDownUp", false);
        }
    }
}
