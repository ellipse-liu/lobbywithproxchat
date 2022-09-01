using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskPower : MonoBehaviour
{
    private PhotonView PV;

    public GameObject play;
    public float abilitycooldown = 1f;

    private bool huskOut = false;
    private GameObject huskobj;
    private PlayerMovement pm;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && pm.grounded && !huskOut && PV.IsMine)
        {
            createHusk();
        }
        if(Input.GetKeyDown(KeyCode.E) && huskOut  && PV.IsMine)
        {
            activateHusk();
        }
    }

    private void createHusk()
    {
        huskobj = PhotonNetwork.Instantiate("Husk", rb.transform.position, Quaternion.identity);
        huskOut = true;
    }

    private void activateHusk()
    {
        PV.Synchronization = ViewSynchronization.Off;
        play.transform.position = huskobj.transform.position;
        PhotonNetwork.Destroy(huskobj);
        PV.Synchronization = ViewSynchronization.UnreliableOnChange;
        huskOut = false;
    }
}
