using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

public class GetSaverData_PlayFab : MonoBehaviour
{
    [SerializeField]GetPlayerCombinedInfoRequestParams InfoRequestParams;

    [SerializeField] private GameObject loadingObject;
    
    private string loadSceneName;
    private bool isLogin = false;
    private void Awake()
    {
        loadingObject.SetActive(false);
    }
    public void Login()
    {
        if(!isLogin)
        {
            isLogin = true;

            PlayFabAuthService.Instance.InfoRequestParams = InfoRequestParams;
            PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
            PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);

            loadingObject.SetActive(true);
        }
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        SaverDatas.c_Datas = PlayFabSimpleJson.DeserializeObject<List<CharacterDatas>>(success.InfoResultPayload.TitleData["CharaData"]);
        SaverDatas.p_Data = new PlayerData();

        //新規作成をしたかどうか
        if (success.NewlyCreated)
        {
            //ユーザー名を入力する画面に移管
            Debug.Log("登録しました");

            loadSceneName = "AccountCreationScene";
        }
        else
        {
            //メインメニューに移管
            Debug.Log("ログインしました");

            loadSceneName = "MainMenuScene";

            SaverDatas.p_Data = PlayFabSimpleJson.DeserializeObject<PlayerData>(success.InfoResultPayload.UserData["PlayerData"].Value);
        }
        StartCoroutine(loadScene());
    }
    IEnumerator loadScene()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(loadSceneName);
        loadScene.allowSceneActivation = false;
        
        yield return new WaitForSeconds(6);
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if(SaverDatas.c_Datas != null)
            {
                break;
            }
        }

        loadScene.allowSceneActivation = true;
    }
}
