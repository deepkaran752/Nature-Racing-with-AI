using UnityEngine.UI;
using UnityEngine;

public class Damage : MonoBehaviour
{
        public float health = 50f;
        public Image img;
        public GameObject showHealth;
        
        public void healthDamage(float amt)
        { 
                health -= amt;
                if(health < 50f){
                        showHealth.SetActive(true);
                }
                
                if(health <=0f){
                        Destroy(gameObject); 
                        //Scores.scoreVal += 10;
                }
                
                float chealth = health / 50f;
                if(chealth >=.75f){
                img.color = Color.green;
                img.fillAmount = chealth;
                }
                else if(chealth >= .4f && chealth <= .75f)
                {
                img.color = Color.yellow;
                img.fillAmount = chealth;
                }
                else if(chealth < .4f)
                {
                img.color = Color.red;
                img.fillAmount = chealth;
                }
        }
}
