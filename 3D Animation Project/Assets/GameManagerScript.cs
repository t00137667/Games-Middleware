using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    public GameObject characterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(characterPrefab.name, new Vector3(0, 2f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
