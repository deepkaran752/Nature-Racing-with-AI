using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Starter : MonoBehaviourPunCallbacks
{
    public static Starter instance;
    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform PlayerList;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject StartButton;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to master!");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    { //when the server is connecting for joining the lobby.
        Debug.Log("Joining Lobby");
        PhotonNetwork.JoinLobby(); //to join the lobby.
        PhotonNetwork.AutomaticallySyncScene=true; //so that the scene loads at the same time for all the clients!
    }
    public override void OnJoinedLobby()
    { //when you have joined the lobby!
        MenuManagement.Instance.OpenMenu("home");
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = "Player : " + Random.Range(0,100).ToString("000");
    }
    
    public void CreateRoom()
    { //function to create a room
        if (string.IsNullOrEmpty(roomName.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomName.text);
        MenuManagement.Instance.OpenMenu("loading");
    }
    public override void OnJoinedRoom()
    { //when you have joined the room!
        MenuManagement.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;


        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform trans in PlayerList)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, PlayerList).GetComponent<PlayerListItem>().Setup(players[i]);
        }
        StartButton.SetActive(PhotonNetwork.IsMasterClient); //only the host can start the game!
    }

    //if the host leaves then no one will be able to play the game for this reason photon has a built in host migration system!
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManagement.Instance.OpenMenu("loading");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    { //basically is going to give an update regarding what all rooms are present.
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
            Debug.Log("Deleted!");
        }
        for(int i=0; i< roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    { //if the server fails to create the room!
        errorText.text = "ROOm creation Failed!"+message;
        MenuManagement.Instance.OpenMenu("error");
    }
    public void LeaveRoom()
    { // when you want to leave the room!
        PhotonNetwork.LeaveRoom();
        MenuManagement.Instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {//redirect you to the home page.
        MenuManagement.Instance.OpenMenu("home");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, PlayerList).GetComponent<PlayerListItem>().Setup(newPlayer);
    }

    //to start the game!
    public void Play()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
