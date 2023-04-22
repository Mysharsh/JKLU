using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using Photon.Realtime;


public class Lobby : MonoBehaviourPunCallbacks
{
    public int playerTTL = -1;

    public TMP_InputField JRoomname;
    public GameObject PlayQuickBtn;
    public TMP_Text PlayerName;
    public TMP_Text ChangeNameTxt;
    public TMP_InputField ChangeName;
    public TMP_InputField RoomnameText;
    public TMP_InputField maxnotext;

    [Header("Panels")]
    public GameObject LobbyPanel;
    public GameObject ConnectingPanel; 
    public GameObject RoomlistPanel;
    public GameObject RoomCreatePanel;




    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        ActivateMyPanel(ConnectingPanel.name);
        PhotonNetwork.AutomaticallySyncScene = true;
        RoomCreatePanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomlistPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Network State: " + PhotonNetwork.NetworkClientState);
    }
    #endregion

    #region UiMethods

    public void Playernameupadtebtn()
    {
        PhotonNetwork.LocalPlayer.NickName = ChangeName.text;
        OnConnectedToMaster();
    }
    public void QuickPlay()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public void OnClickCreate()
    {
        string Roomname = RoomnameText.text;
        if (string.IsNullOrEmpty(Roomname))
        {
            Roomname = "Room" + Random.Range(123, 999);
        }
        RoomOptions roomOptions = new RoomOptions
        {
            IsVisible = false,
            MaxPlayers = (byte)int.Parse(maxnotext.text)
        };
        PhotonNetwork.CreateRoom(Roomname, roomOptions,null);
    }
    public override void OnJoinedLobby()
    {
        ActivateMyPanel(LobbyPanel.name);
        Debug.Log(PhotonNetwork.CurrentLobby+"Yes");
        Debug.Log(PhotonNetwork.InLobby);
    }
    public void Joinroom()
    {
        string jroom = JRoomname.text;
        PhotonNetwork.JoinRoom(jroom);

    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Connected");
        PhotonNetwork.JoinLobby();
        PlayerName.text = PhotonNetwork.LocalPlayer.NickName;
        ChangeNameTxt.text = PhotonNetwork.LocalPlayer.NickName;
    }
    public override void OnCreatedRoom()
    {
        PhotonNetwork.JoinRoom(RoomnameText.text);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 4 };
        if (playerTTL >= 0)
            roomOptions.PlayerTtl = playerTTL;

        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }
    public override void OnJoinedRoom()
    {

        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(2);
    }
    #endregion
    #region Public_Methods
    public void ActivateMyPanel(string panelName)
    {
        LobbyPanel.SetActive(panelName.Equals(LobbyPanel.name));
        ConnectingPanel.SetActive(panelName.Equals(ConnectingPanel.name));

    }
    #endregion
}
