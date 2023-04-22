using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int randomno = Random.Range(2, 20);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            //Vector3 randomPosition = new Vector3(Random.Range(2, 10),1f, Random.Range(2,10));

            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomno, 1f, randomno), Quaternion.Euler(0f, 0f, 0f));
        }
        // else
        // {
        //     PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(randomno,1f,randomno),Quaternion.identity);
        // }


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.NetworkClientState);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If we're connected to a Photon room, leave it and destroy the player object
            if (PhotonNetwork.IsConnectedAndReady)
            {
                // If we're connected to the Photon server, disconnect from it
                PhotonNetwork.LeaveRoom();

            }
        }
        

    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
