using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    RoomInfo[] rooms;

    public InputField CodeInput;
    public Button JoinButton;
    public Button CreateButton;
    public Text DebugText;

    public InputField NameInput;

    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PLayer has connected to Master Server");
        PhotonNetwork.AutomaticallySyncScene = true;
        JoinButton.interactable = true;
        CreateButton.interactable = true;
    }

    public void OnJoinButtonClicked()
    {
        PhotonNetwork.LocalPlayer.NickName = NameInput.text;
        PhotonNetwork.JoinRoom("Room" + CodeInput.text);
        Debug.Log("Room joined");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join room, but no game with that code");
        DebugText.text = "No room with that code";
    }

    public void OnCreateButtonClicked()
    {
        PhotonNetwork.LocalPlayer.NickName = NameInput.text;
        CreateRoom();
    }

    void CreateRoom()
    {
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room" + CodeInput.text, roomOps);
        Debug.Log("Created Room" + CodeInput.text);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room name already exists");
        DebugText.text = "Cannot create room with same code";
    }
}
