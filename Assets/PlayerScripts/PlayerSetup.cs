using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour
{
    private PhotonView PV;

    public GameObject TextName;
    public Camera myCamera;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCamera.enabled = PV.IsMine;
        if (!PV.IsMine)
        {
            TextName.GetComponent<TextMesh>().text = PhotonNetwork.LocalPlayer.NickName;
        }
    }
}
