using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {
    private Vector3 targetPosition;

    private float movementSpeed = 5.0f;
    private float rotationSpeed = 2.0f;
    private float targetPositionTolerence = 3.0f;
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

	// Use this for initialization
	void Start () {
        minX = -45.0f;
        minX = 45.0f;

        minZ = -45.0f;
        maxZ = 45.0f;

        // Get Wander Position
        GetNextPosition();
	}
	
    void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerence)
        {
            GetNextPosition();
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }
}
