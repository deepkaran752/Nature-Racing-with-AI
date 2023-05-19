using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManagement : MonoBehaviour
{
    PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)
        {
            ControllerCreation();
        }
    }

    void ControllerCreation()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "GrayCar"),Vector3.zero,Quaternion.identity);
    }
}
