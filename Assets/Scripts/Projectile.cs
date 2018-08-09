using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Environment")
        {
            if(explosionPrefab == null)
            {
                return;
            }

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
        }
    }
}
