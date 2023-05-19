using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineDetection : MonoBehaviour
{
    public GameObject playerTag = null;
    public GameObject enemyTag = null;
    string player = "Player";
    string enemy = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == player)
        {
            Debug.Log("You won!");
            SceneManager.LoadScene(3);
        }
        else if(other.tag == enemy)
        {
            Debug.Log("AI WON!");
            SceneManager.LoadScene(4);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
