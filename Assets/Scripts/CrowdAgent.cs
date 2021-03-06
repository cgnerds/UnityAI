﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class CrowdAgent : MonoBehaviour {

    public Transform target;

    private UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = Random.Range(4.0f, 5.0f);
        agent.SetDestination(target.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
