using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [SerializeField] private GameObject skillAnimation;
    [SerializeField] private GameObject skillObject;
    [SerializeField] private string player;
    [SerializeField] private ParticleSystem skillEffect;

    [Header("治療系SE/プンツ")]
    [Header("風切り音SE/シャ")]
    [Header("チリチリSE/フレリア")]
    [Header("魔法系SE/ヘングレ")]
    [Header("殴り(素振り)系SE/ミミと狼")]
    public AudioClip[] skillSound;

    //public SoundManager audioManager_SkillSe;
    public PlaySE playSE;

    private GameObject crossObj;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 normal;
    private float distance;
    private bool isInvocableSkill;
    private bool isInvoked;
    private bool isStart;
    private Move move;
    private GameObject skillObj;

    private RaycastHit2D hit;
    private Ray2D ray;
    private void Start()
    {
        crossObj = null;
        isStart = false;
        isInvocableSkill = false;
        isInvoked = false;
        startPos = Vector3.zero;
        endPos = Vector3.zero;
        normal = Vector3.zero;

    }
    private void Update()
    {
        if (isInvocableSkill && isStart)
        {
            hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, distance);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Move>())
            {
                move = hit.collider.gameObject.GetComponent<Move>();
                if (hit.collider.CompareTag(player) && hit.transform.gameObject != crossObj && move.skill)//Ray上にキャラクターがあったら
                {
                    if (!isInvoked)//発動したかどうか
                    {
                        StartCoroutine(hit.collider.GetComponent<CharacterStatus>().skill.Skill(hit.collider.gameObject));
                        SkillEffect();//Skill発動時のアニメーションはコ↑コ↓
                        SkillInvokedAnimation();
                        var i = hit.collider.GetComponent<CharacterStatus>().character.id - 1;
                        playSE.AudioPlay(skillSound[i]);
                        {
                            //string skillSEName = "";


                            //switch (hit.collider.GetComponent<CharacterStatus>().character.id - 1)
                            //{
                            //    //赤ずきん・狼男
                            //    case 0:
                            //    case 5:
                            //        skillSEName = "Strike";
                            //        break;
                            //    //ヘングレ
                            //    case 2:
                            //        skillSEName = "Magic";
                            //        break;
                            //    //フレリア
                            //    case 1:
                            //        skillSEName = "Shine";
                            //        break;
                            //    //シャーレイ
                            //    case 3:
                            //        skillSEName = "Wind";
                            //        break;
                            //    //ラプンツェル
                            //    case 4:
                            //        skillSEName = "Heal";
                            //        break;
                            //}

                            //audioManager_SkillSe.GetSkillSeIndex(skillSEName);
                        }


                        isInvoked = true;//発動済み
                    }
                }
            }
        }
    }
    //移動開始したとき
    public void MoveStart(GameObject obj)
    {
        isStart = true;
        isInvocableSkill = true;
        isInvoked = false;
        startPos = obj.transform.position;
    }
    //移動中の値更新
    public void MoveNow(GameObject obj)
    {
        normal = (startPos - obj.transform.position).normalized;
        normal *= -1;
        ray = new Ray2D(startPos, normal);
    }
    //壁に反射した時
    public void MoveEnd(GameObject obj)
    {
        if (isStart)
        {
            crossObj = obj;
            endPos = obj.transform.position;
            normal = (startPos - endPos).normalized;//正規化
            normal *= -1;//ベクトルを反対に
            distance = Vector3.Distance(startPos, endPos);
            ray = new Ray2D(startPos, normal);
            isStart = false;
        }
    }
    private void SkillInvokedAnimation()
    {
        var obj = Instantiate(skillObject, skillAnimation.transform.position, Quaternion.identity);
        var image = obj.GetComponent<Image>();
        image.sprite = hit.collider.GetComponent<CharacterStatus>().character.icon;
        obj.transform.SetParent(skillAnimation.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
    private void SkillEffect()
    {
        ParticleSystem effect = Instantiate(skillEffect,hit.collider.transform.position,Quaternion.identity);
        ParticleSystem crossEffect = Instantiate(skillEffect, hit.collider.transform.position,Quaternion.identity);
        effect.startRotation = move.degree * Mathf.Deg2Rad;
        Vector2 angle = ray.origin - ray.direction;
        crossEffect.startRotation = Mathf.Atan2(angle.x,angle.y);
    }
}