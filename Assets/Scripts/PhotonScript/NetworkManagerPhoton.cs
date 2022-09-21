using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class NetworkManagerPhoton : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;

    #region Unity Methods

    private void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    #endregion 

    #region Public Methods
    public void ConnectToPhotonServer(){
        if(!PhotonNetwork.IsConnected){
            PhotonNetwork.ConnectUsingSettings();

            EnterGamePanel.SetActive(false);
            ConnectionStatusPanel.SetActive(true);
        }
    }

    public void JoinRandomRoom(){
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion



    #region Photon Callbacks
    public override void OnConnected() {
        print("Connected to Internet");
    }
    
    public override void OnConnectedToMaster() {
        print(PhotonNetwork.NickName +  " Connected to Server");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode , string message){
        base.OnJoinRandomFailed(returnCode,message);
        Debug.Log(message); // No Match Found
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom(){
        Debug.Log(PhotonNetwork.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        Debug.Log(newPlayer.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    // public override void OnDisconnected( DisconnectCause cause ){
    //     // print("Disconnected from Server for reason " + cause.ToString());
    // }
    #endregion 


    #region Private methods
    void CreateAndJoinRoom(){
        string randomRoomName = "Room " + Random.Range(0,10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }
    #endregion 

}
