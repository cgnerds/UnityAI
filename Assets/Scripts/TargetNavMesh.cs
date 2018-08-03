using UnityEngine;
using UnityEngine.AI;

public class TargetNavMesh : MonoBehaviour {
    private NavMeshAgent[] navAgents;
    public Transform targetMarker;

	// Use this for initialization
	void Start () {
        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];	
	}
	
    private void UpdateTargets(Vector3 targetPosition)
    {
        foreach(NavMeshAgent agent in navAgents)
        {
            agent.destination = targetPosition;
        }
    }


	// Update is called once per frame
	void Update () {
		if(GetInput())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                UpdateTargets(targetPosition);
                targetMarker.position = targetPosition;
            }
        }
	}

    private bool GetInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(targetMarker.position, targetMarker.position + Vector3.up * 5, Color.red);
    }
}
