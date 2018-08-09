using System.Collections;
using UnityEngine;

public class HorizontalCam : MonoBehaviour {
    [SerializeField]
    private Transform target;
    private Vector3 targetPosition;

	// Update is called once per frame
	void Update () {
        targetPosition = transform.position;
        targetPosition.z = target.transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
	}
}
