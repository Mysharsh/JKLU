using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject[] localPlayerItems;
    public GameObject[] remotePlayerItems;
    public GameObject FollowCamera; 
    public GameObject MController;     


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // Local Player
            foreach (GameObject g in localPlayerItems)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in remotePlayerItems)
            {
                g.SetActive(false);
            }
            FollowCamera.SetActive(true);
            MController.SetActive(true);
            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<StarterAssetsInputs>().enabled = true;
        }
        else
        {
            // Remote player
            foreach (GameObject g in localPlayerItems)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in remotePlayerItems)
            {
                g.SetActive(true);
            }
            FollowCamera.SetActive(false);
            MController.SetActive(false);
            GetComponent<ThirdPersonController>().enabled = false;
            GetComponent<StarterAssetsInputs>().enabled = false;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
