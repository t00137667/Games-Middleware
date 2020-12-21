using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Windows.Speech;

public class Shoot : MonoBehaviourPunCallbacks
{
    public Transform projectileSpawn;
    public OrbMovement projectile;
    public KeywordRecognizer recognizer;
    public string[] keywords = new string[] { "shoot", "spawn" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    

    // Start is called before the first frame update
    void Start()
    {
        recognizer = new KeywordRecognizer(keywords, confidence);
        recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        recognizer.Start();
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_Shoot", RpcTarget.All);
            Debug.Log("Tried Firing");
        }
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

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
