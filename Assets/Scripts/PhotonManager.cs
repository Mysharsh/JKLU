using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TMP_Text PlayerName;
    public TMP_InputField RoomnameText;
    public TMP_InputField maxnotext;

    // public GameObject PlayerNamePanel;
    public GameObject LobbyPanel;
    public GameObject RoomCreatePanel;
    public GameObject ConnectingPanel;

    [Header("Room Panel")]
    public GameObject RoomlistPanel;
    public GameObject roomListPrefab;
    public GameObject roomListParent;

    private Dictionary<string, RoomInfo> roomListData;


    private Dictionary<string, GameObject> roomListGameobject;
    private Dictionary<int, GameObject> PlayerListGameobject;


    [Header("Inside Room Panel")]
    public GameObject InsideRoomPanel;
    public GameObject PlayerListItemPrefab;
    public GameObject PlayerListItemParent;
    public GameObject PlayButton;
    public GameObject PlayQuickBtn;


    #region UnityMethods

    void Awake()
    {
        RoomCreatePanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomlistPanel.SetActive(false);
        InsideRoomPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()

    {
        Application.runInBackground = true;
        string name = GenerateGuestName();
        ConnectingPanel.SetActive(true);
        RoomCreatePanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomlistPanel.SetActive(false);
        InsideRoomPanel.SetActive(false);
        roomListData = new Dictionary<string, RoomInfo>();
        roomListGameobject = new Dictionary<string, GameObject>();
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!string.IsNullOrEmpty(name))
        {
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Empty Name");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Network State: " + PhotonNetwork.NetworkClientState);
    }
    #endregion

    #region UiMethods
    // public void OnLoginClick()
    // {
    //     string name = GenerateGuestName();


    //     if (!string.IsNullOrEmpty(name))
    //     {
    //         PhotonNetwork.LocalPlayer.NickName = name;
    //         PhotonNetwork.ConnectUsingSettings();
    //         ActivateMyPanel(ConnectingPanel.name);
    //     }
    //     else
    //     {
    //         Debug.Log("Empty Name");
    //     }
    // }

    private string GenerateGuestName()
    {
        // Code to generate a random name for the guest user
        return "Guest_" + Random.Range(1000, 9999);

    }
    public void OnClickRoomCreate()
    {
        string roomName = RoomnameText.text;
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room" + Random.Range(123, 999);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)int.Parse(maxnotext.text);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnCancelClick()
    {
        ActivateMyPanel(LobbyPanel.name);
    }
    public void OnRoomlistClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("on list empty");
        }
        ActivateMyPanel(RoomlistPanel.name);
    }
    public void BackFromRoomList()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivateMyPanel(LobbyPanel.name);
    }

    public void BackFromPlayerList()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        ActivateMyPanel(LobbyPanel.name);

    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game");
        /* else{
             PhotonNetwork.LoadLevel("Game");
         }*/

    }
    public void OnClickquickplaybtn()
    {
        PhotonNetwork.CreateRoom("Global", new RoomOptions() { MaxPlayers = 4 }, null);
    }
    #endregion

    #region PHOTON CALLBACKS

    public override void OnConnected()
    {
        Debug.Log("Connected to Network");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Connected to Photon");
        PlayerName.text = PhotonNetwork.LocalPlayer.NickName;
        ActivateMyPanel(LobbyPanel.name);
    }
    /*public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        if (PhotonNetwork.CurrentRoom.Name == "Global")
        {
            PhotonNetwork.JoinRoom("Global");
        }
    }*/

    public override void OnCreatedRoom()
    {
        //Debug.Log(PhotonNetwork.CurrentRoom.Name + "is Created");

    }
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.Name == "Global")
        {
            OnClickPlayButton();
        }
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "Room Joined");
        ActivateMyPanel(InsideRoomPanel.name);
        if (PlayerListGameobject == null)
        {
            PlayerListGameobject = new Dictionary<int, GameObject>();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(true);
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject PlayerListItem = Instantiate(PlayerListItemPrefab);
            PlayerListItem.transform.SetParent(PlayerListItemParent.transform);
            PlayerListItem.transform.localScale = Vector3.one;
            PlayerListItem.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = p.NickName;
            if (p.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                PlayerListItem.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                PlayerListItem.transform.GetChild(1).gameObject.SetActive(false);
            }
            PlayerListGameobject.Add(p.ActorNumber, PlayerListItem);
        }
    }

    // new player join Player List update
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject PlayerListItem = Instantiate(PlayerListItemPrefab);
        PlayerListItem.transform.SetParent(PlayerListItemParent.transform);
        PlayerListItem.transform.localScale = Vector3.one;
        PlayerListItem.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = newPlayer.NickName;
        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PlayerListItem.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            PlayerListItem.transform.GetChild(1).gameObject.SetActive(false);
        }
        PlayerListGameobject.Add(newPlayer.ActorNumber, PlayerListItem);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        Destroy(PlayerListGameobject[otherPlayer.ActorNumber]);
        PlayerListGameobject.Remove(otherPlayer.ActorNumber);

        if (PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }

    }


    public override void OnLeftRoom()
    {

        ActivateMyPanel(LobbyPanel.name);
        foreach (GameObject obj in PlayerListGameobject.Values)
        {
            Destroy(obj);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)

    {
        //Clear List
        ClearRoomList();


        foreach (RoomInfo rooms in roomList)
        {
            Debug.Log("Room Name" + rooms.Name);
            if (!rooms.IsOpen || !rooms.IsVisible || rooms.RemovedFromList)
            {
                if (roomListData.ContainsKey(rooms.Name))
                {
                    roomListData.Remove(rooms.Name);
                }
            }
            else
            {
                if (roomListData.ContainsKey(rooms.Name))
                {
                    //Update List
                    roomListData[rooms.Name] = rooms;
                }
                else
                {
                    roomListData.Add(rooms.Name, rooms);
                }
            }

        }
        // Generate List Item
        foreach (RoomInfo roomItem in roomListData.Values)
        {
            GameObject roomListItemObject = Instantiate(roomListPrefab);
            roomListItemObject.transform.SetParent(roomListParent.transform);
            roomListItemObject.transform.localScale = Vector3.one;
            // rooom name player name button room join
            roomListItemObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = roomItem.Name;
            roomListItemObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = roomItem.PlayerCount + "/" + roomItem.MaxPlayers;
            roomListItemObject.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => RoomJoinFromList(roomItem.Name));
            roomListGameobject.Add(roomItem.Name, roomListItemObject);
        }
    }

    public override void OnLeftLobby()
    {
        ClearRoomList();
        roomListData.Clear();
    }


    #endregion

    #region Public_Methods

    public void RoomJoinFromList(string roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(roomName);
    }
    public void ClearRoomList()
    {
        if (roomListGameobject.Count > 0)
        {
            foreach (var v in roomListGameobject.Values)
            {
                Destroy(v);
            }
            roomListGameobject.Clear();
        }

    }
    public void ActivateMyPanel(string panelName)
    {
        LobbyPanel.SetActive(panelName.Equals(LobbyPanel.name));
        // PlayerNamePanel.SetActive(panelName.Equals(PlayerNamePanel.name));
        RoomCreatePanel.SetActive(panelName.Equals(RoomCreatePanel.name));
        ConnectingPanel.SetActive(panelName.Equals(ConnectingPanel.name));
        RoomlistPanel.SetActive(panelName.Equals(RoomlistPanel.name));
        InsideRoomPanel.SetActive(panelName.Equals(InsideRoomPanel.name));

    }

    #endregion
}
