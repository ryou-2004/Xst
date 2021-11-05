using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_ObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;

    public void ClickMenuButton()
    {
        menuObject.SetActive(true);
    }
    public void CloseMenuButton()
    {
        menuObject.SetActive(false);
    }
}
