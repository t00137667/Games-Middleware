using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterObjectSpawner : MonoBehaviourPunCallbacks, IPunObservable
{
    Transform rightHand;
    Transform upperChest;
    Animator animator;
    public GameObject rightHandObject;
    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public Vector3 OrbSpacer = new Vector3(0.2f, 0f, 0.3f);
    static float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
        upperChest = animator.GetBoneTransform(HumanBodyBones.UpperChest);
        if (rightHand != null)
        {
            Debug.Log("RightHand Found");
        }
        else
        {
            Debug.Log("RightHand not found");
        }
    }

    internal Vector3 local_to_global(Transform parent, Vector3 loc)
    {
        return parent.position + loc.x * parent.right + loc.y * parent.up + loc.z * parent.forward;
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //GameObject RightHandObject = PhotonNetwork.Instantiate(rightHandObject.name, local_to_global(upperChest, OrbSpacer), Quaternion.identity);
                //RightHandObject.transform.SetParent(upperChest);
                //lookObj = RightHandObject.transform;
                //rightHandObj = RightHandObject.transform.GetChild(1);
                RPC_Spawn_Orb();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ikActive = !ikActive;
                if (!ikActive) t = 0f;
            }
            if (ikActive) t += 1.5f * Time.deltaTime;
        }
        
    }

    [PunRPC]
    void RPC_Spawn_Orb()
    {
        Debug.Log("Spawn called");
        GameObject RightHandObject = PhotonNetwork.Instantiate(rightHandObject.name, local_to_global(upperChest, OrbSpacer), Quaternion.identity);
        RightHandObject.transform.SetParent(upperChest);
        lookObj = RightHandObject.transform;
        rightHandObj = RightHandObject.transform.GetChild(1);
    }

    // Code from the Unity Inverse Kinematics Tutorial with some minor changes
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    // A lerp for fun
                    animator.SetIKPosition(AvatarIKGoal.RightHand, Vector3.Lerp(rightHand.position, rightHandObj.position, t));
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}
