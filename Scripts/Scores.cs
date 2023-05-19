using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System; //for time

public class Scores : MonoBehaviour
{
    bool tim = false;
    float ctime; //current time
    int stime = 1; //static time
    public Text timtext;
    //public Text sctext;
    //public static int scoreVal = 0;
    
    void Start(){
        ctime = stime*240;
        tim=true;
    }
    // Update is called once per frame
    void Update()
    {
        //sctext.text = "Score: " + scoreVal; 
        if (tim == true){
            ctime = ctime - Time.deltaTime;
        }
        if(ctime <= 0){
            SceneManager.LoadScene(2);
        }
        //to run the game timer without the involvement of the processor....
        TimeSpan time = TimeSpan.FromSeconds(ctime);
        timtext.text = "Timer- "+ time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }
}
