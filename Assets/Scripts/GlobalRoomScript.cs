using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRoomScript : MonoBehaviourPunCallbacks
{
    public byte MaxPlayers = 4;

    public int playerTTL = -1;

    
    private void Update()
    {
        Debug.Log("state" + PhotonNetwork.NetworkClientState);
    }

    public void ConnectNow()
    {
        Debug.Log("ConnectAndJoinRandom.ConnectNow() ");


        PhotonNetwork.ConnectUsingSettings();

    }


    // below, we implement some callbacks of the Photon Realtime API.
    // Being a MonoBehaviourPunCallbacks means, we can override the few methods which are needed here.


    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. This client is now connected to Master Server in region [" + PhotonNetwork.CloudRegion +
            "] and can join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.CreateRoom("Global", new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is now connected to Relay in region [" + PhotonNetwork.CloudRegion + "]. This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRoom("Global");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available in region [" + PhotonNetwork.CloudRegion + "], so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = this.MaxPlayers };
        if (playerTTL >= 0)
            roomOptions.PlayerTtl = playerTTL;

        PhotonNetwork.CreateRoom("Global", new RoomOptions() { MaxPlayers = 4 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected(" + cause + ")");
    }

    public override void OnJoinedRoom()
    {

        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room in region [" + PhotonNetwork.CloudRegion + "]. Game is now running.");
    }
}

