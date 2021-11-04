using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float initialForce;
    public float lifeTime;
    private float lifeTimer = 0f;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //add the initial force to the rigidbody (attached to this grenade)
        GetComponent<Rigidbody>().AddRelativeForce(initialForce, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //update the timer
        lifeTimer += Time.deltaTime;

        //destroy projectile (grenade) if the time is up
        if(lifeTimer >= lifeTime)
        {
            Explode();
        }
    }

    private void Explode()
    {
        print("Explode");
        //instantiate the explosion prefab
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);


        //Destroy gameObject grenade
        Destroy(this.gameObject);
    }
}
