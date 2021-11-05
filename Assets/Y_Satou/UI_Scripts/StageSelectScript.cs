using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScript : MonoBehaviour
{
    public GameObject ConfirmationScreen;
    public Text stageName;

    public void StageSelect(int stageID)
    {
        CPUCreate.stageNumber = stageID;
        ConfirmationScreen.SetActive(true);
        switch (stageID)
        {
            case 1:
                stageID = 3;
                break;
            case 3:
                stageID = 1;
                break;
        }
        stageName.text = $"Stage {stageID}";
    }
}
