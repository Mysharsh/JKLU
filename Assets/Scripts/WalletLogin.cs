using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WalletLogin: MonoBehaviour
{
    void Update()
    {
        Debug.Log("Network State: " + PhotonNetwork.NetworkClientState);
    }

    #region LoginMethods
    public void OnLoginclick()
    {
        Application.runInBackground = true;
        string name = GenerateGuestName();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LocalPlayer.NickName = name;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.LoadLevel("Lobby");

    }
    private string GenerateGuestName()
    {
        // Code to generate a random name for the guest user
        return "Guest_" + Random.Range(1000, 9999);

    }
   
    
    #endregion
    
}
