using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMSceneChange : MonoBehaviour
{

    public AudioClip BGMTitle;
    public AudioClip BGMMenu;
    public AudioClip BattleIntro;
    public AudioClip BattleLoop;
    public AudioClip WinIntro;
    public AudioClip WinLoop;
    public AudioClip LoseIntro;
    public AudioClip LoseLoop;

    public float delay = 0f;

    private AudioSource source;
    private AudioSource source1;

    //１つ前のシーン名
    private string beforeScene = "test_playfab";

    void Start()
    {
        
        DontDestroyOnLoad(gameObject);

        source = gameObject.AddComponent<AudioSource>();
        source1 = gameObject.AddComponent<AudioSource>();

        source.clip = BGMTitle;
        source.volume = VolumeData.bgmVol * VolumeData.masterVol;
        source.Play();

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        //タイトルからメインへ
        if (beforeScene == "test_playfab" && nextScene.name == "MainMenuScene")
        {
            source.Stop();
            source.clip = BGMMenu;
            source.volume = VolumeData.bgmVol * VolumeData.masterVol;
            source.Play();
        }
        //メインからマッチングへはBGM変わらず
        
        //マッチングからバトルへ
        if (beforeScene == "Login" && nextScene.name == "BattleScene")
        {
            source.Stop();
            source1.Stop();

            if (BattleIntro != null)
            {
                source.playOnAwake = false;
                source.clip = BattleIntro;
                source.volume = VolumeData.bgmVol * VolumeData.masterVol;
                source.PlayScheduled(AudioSettings.dspTime + delay);
                if (BattleLoop != null)
                {
                    source1.playOnAwake = false;
                    source1.clip = BattleLoop;
                    source1.volume = VolumeData.bgmVol * VolumeData.masterVol;
                    source1.loop = true;
                    source1.PlayScheduled(AudioSettings.dspTime + delay + BattleIntro.length);
                }
            }
        }
        //バトルから勝リザルトへ
        if (beforeScene == "BattleScene" && nextScene.name == "WinResultScene")
        {
            source.Stop();
            source1.Stop();

            if (BattleIntro != null)
            {
                source.playOnAwake = false;
                source.clip = WinIntro;
                source.volume = VolumeData.bgmVol * VolumeData.masterVol;
                source.PlayScheduled(AudioSettings.dspTime + delay);
                if (BattleLoop != null)
                {
                    source1.playOnAwake = false;
                    source1.clip = WinLoop;
                    source1.volume = VolumeData.bgmVol * VolumeData.masterVol;
                    source1.loop = true;
                    source1.PlayScheduled(AudioSettings.dspTime + delay + WinIntro.length);
                }
            }
        }
        //バトルから負リザルトへ
        if (beforeScene == "BattleScene" && nextScene.name == "LoseResultScene")
        {
            source.Stop();
            source1.Stop();

            if (BattleIntro != null)
            {
                source.playOnAwake = false;
                source.clip = LoseIntro;
                source.volume = VolumeData.bgmVol * VolumeData.masterVol;
                source.PlayScheduled(AudioSettings.dspTime + delay);
                if (BattleLoop != null)
                {
                    source1.playOnAwake = false;
                    source1.clip = LoseLoop;
                    source1.volume = VolumeData.bgmVol * VolumeData.masterVol;
                    source1.loop = true;
                    source1.PlayScheduled(AudioSettings.dspTime + delay + LoseIntro.length);
                }
            }
        }
        //負けリザルトからメインへ
        if (beforeScene == "LoseResultScene" && nextScene.name == "MainMenuScene")
        {
            source.Stop();
            source1.Stop();
            source.clip = BGMMenu;
            source.volume = VolumeData.bgmVol * VolumeData.masterVol;
            source.Play();
        }
        //勝リザルトからメインへ
        if (beforeScene == "WinResultScene" && nextScene.name == "MainMenuScene")
        {
            source.Stop();
            source1.Stop();
            source.clip = BGMMenu;
            source.volume = VolumeData.bgmVol * VolumeData.masterVol;
            source.Play();
        }
        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
    private void Update()
    {
        source.volume = VolumeData.bgmVol * VolumeData.masterVol;
    }
}
