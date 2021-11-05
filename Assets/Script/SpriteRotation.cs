using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    private void Start()
    {
        //var s = GetComponent<SpriteRenderer>();
        //if (!transform.parent.GetComponent<Photon.Pun.PhotonView>().IsMine && !SimplePun.IsMaster)
        //{
        //    print("SpriteChange");
        //    s.flipX = s.flipY = false;
        //}
        //else if(transform.parent.GetComponent<Photon.Pun.PhotonView>().IsMine&&SimplePun.IsMaster)
        //{
        //    s.flipX = s.flipY = true;
        //}
    }
    void Update()
    {
        Vector3 parent = transform.parent.transform.localRotation.eulerAngles;
        parent.x = 0;
        this.transform.localRotation = Quaternion.Euler(parent);
    }
}