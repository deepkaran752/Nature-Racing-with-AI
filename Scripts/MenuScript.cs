using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public void StartGame(){
		Debug.Log("Start!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		//FindObjectOfType<AudioManager>().Play("Click");
	}

	public void Back(){
		Debug.Log("Back!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		//FindObjectOfType<AudioManager>().Play("Click");
	}

	public void Restart(){
		Debug.Log("Restart!");
		SceneManager.LoadScene(1);
		//FindObjectOfType<AudioManager>().Play("Click");
	}

	public void Escape(){
		Debug.Log("Escape");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		//FindObjectOfType<AudioManager>().Play("Click");
	}

	public void ExitGame(){
		Debug.Log("Bye!");
		Application.Quit();
	}
}
