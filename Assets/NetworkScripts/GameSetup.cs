using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetup : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate("PhotonPlayer", Vector3.up, Quaternion.identity);
    }
}
