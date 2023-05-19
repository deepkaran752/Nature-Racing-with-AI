using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class PlayerContoller : MonoBehaviourPunCallbacks
{
    
    [SerializeField] float walkSpeed, running, jumpSpeed, smoothness, mouseSensitivity;

    [SerializeField] GameObject Holder;

    //[SerializeField] Item[] items;
    //int itemIndex;
    //int previousIndex = -1; //by default there's no previous item.


    //variables;
    float verticalRotation; Vector3 smoothVelocity, movingAmount;
    // bool isGrounded;
    Rigidbody rbMain = null;
    PhotonView PV = null;
    private void Awake()
    {
        rbMain = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!PV.IsMine)
            return;
        LookingAround();
        Movement();
        //EquipWeaponWithKeys();
    }
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rbMain);
        }
    }
    void LookingAround()
    { 
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity); //for horizontal looking!

        verticalRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;   //so that the player can look smoothly in Y //vertical Direction as well!
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Holder.transform.localEulerAngles = Vector3.left * verticalRotation;
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized; // to avoid conflicts we normalize.

        bool shiftKey = Input.GetKey(KeyCode.LeftShift); //if pressed -> run, if not -> walk!
        if (shiftKey)
            movingAmount = Vector3.SmoothDamp(movingAmount, moveDirection * running, ref smoothVelocity, smoothness);
        else
            movingAmount = Vector3.SmoothDamp(movingAmount, moveDirection * walkSpeed, ref smoothVelocity, smoothness);

        //for jumping
        /*
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rbMain.AddForce(transform.up * jumpSpeed);
        }*/
    }

    //checking the status if the player is grounded or not, so the player doesnt jump mid air!
    /*public void GroundedStatus(bool _isGrounded)
    {
        isGrounded = _isGrounded;
    }*/

    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rbMain.MovePosition(rbMain.position + transform.TransformDirection(movingAmount)* Time.fixedDeltaTime);
    }
    /*
    void EquipWeapon(int _index) //TOEQUIP THE WEAPONS!
    {
        if (_index == previousIndex)
            return;

        itemIndex = _index;
        items[itemIndex].itemGameObj.SetActive(true);

        if (previousIndex != -1)
            items[previousIndex].itemGameObj.SetActive(false);

        previousIndex = itemIndex;

        //now the synching problem arises! for that we are using here the concept of hashtable!
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            //THE CODE ABOVE SENDS THE PROPERTIES OF THE PLAYER OVER THE NETWORK!
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipWeapon((int)(changedProps["itemIndex"]));  //this code checks if the player is not the owner and sends the request to
            //change the prop so that the owner can see as well
            //basically syncs the weapon change and viewability. 
        }

    }
    void EquipWeaponWithKeys() //to equip the weapons with numpad!
    {
        for(int i=0; i< items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())){
                EquipWeapon(i);
                break;
            }
        }

    }*/
}
