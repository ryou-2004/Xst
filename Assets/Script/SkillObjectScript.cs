using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObjectScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(deleteObject());
    }
    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
