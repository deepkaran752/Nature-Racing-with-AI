using UnityEngine;

public class raycastscript : MonoBehaviour
{
    public float damage= 10f;
    public bool gunhit = false;
    //public GameObject gObj;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem f1;
    void Update() {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }

    }
    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) 
        {
            Debug.Log(hit.transform.name);
            Damage dam = hit.transform.GetComponent<Damage>();
            if(hit.transform.gameObject.CompareTag("Enemy")){
                if(dam != null){
                    dam.healthDamage(damage);
                    f1.Play();
                }
            }
            //when raycast hits the enemy
            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(-hit.normal * 20f);
            }
            /*gunhit = true;
            if(hit.transform.name != "Terrain"){
                GameObject gObj = new GameObject (hit.transform.name);
                Destroy(gObj);
            }*/
        }
    }

}
