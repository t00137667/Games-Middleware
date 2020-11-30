using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviourPunCallbacks
{
    public Transform projectileSpawn;
    public OrbMovement projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("RPC_Shoot", RpcTarget.All);
                Debug.Log("Tried Firing");
            }
        }
    }

    // Synchronise the call
    [PunRPC]
    void RPC_Shoot()
    {
        OrbMovement activeProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        activeProjectile.isActive = true;
        Debug.Log("Fired");
    }
}
