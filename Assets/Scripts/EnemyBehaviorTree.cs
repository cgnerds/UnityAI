using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorTree : MonoBehaviour {

    private Player playerData;
    private Player ownData;

    public RandomBinaryNode buffCheckRandomNode;
    public ActionNode buffCheckNode;
    public ActionNode healthCheckNode;
    public ActionNode attackCheckNode;
    public Sequence buffCheckSequence;
    public Selector rootNode;

    public delegate void TreeExecuted();
    public event TreeExecuted onTreeExecuted;
    public delegate void NodePassed(string trigger);


	// Use this for initialization
	void Start () {
        healthCheckNode = new ActionNode(CriticalHealthCheck);
        attackCheckNode = new ActionNode(CheckPlayerHealth);

        buffCheckRandomNode = new RandomBinaryNode();
        buffCheckNode = new ActionNode(BuffCheck);
        buffCheckSequence = new Sequence(new List<BTNode>
        {
            buffCheckRandomNode,
            buffCheckNode,
        });

        rootNode = new Selector(new List<BTNode>
        {
            healthCheckNode,
            attackCheckNode,
            buffCheckSequence,
        });
	}
	

    private IEnumerator Execute()
    {
        Debug.Log("The AI is thinking...");
        yield return new WaitForSeconds(2.5f);

        if(healthCheckNode.nodeState == NodeStates.SUCCESS)
        {
            Debug.Log("The AI decided to heal itself");
            ownData.Heal();
        }
        else if(attackCheckNode.nodeState == NodeStates.SUCCESS)
        {
            Debug.Log("The AI decided to attack the player!");
            playerData.Damage();
        }
        else if(buffCheckSequence.nodeState == NodeStates.SUCCESS)
        {
            Debug.Log("The AI decided to defend itself");
            ownData.Buff();
        }
        else
        {
            Debug.Log("The AI finally decided to attack the player");
            playerData.Damage();
        }

        if(onTreeExecuted != null)
        {
            onTreeExecuted();
        }
    }

    private NodeStates CriticalHealthCheck()
    {
        if(ownData.HasLowHealth)
        {
            return NodeStates.SUCCESS;
        }
        else
        {
            return NodeStates.FAILURE;
        }
    }

    private NodeStates CheckPlayerHealth()
    {
        if(playerData.HasLowHealth)
        {
            return NodeStates.SUCCESS;
        }
        else
        {
            return NodeStates.FAILURE;
        }
    }

    private NodeStates BuffCheck()
    {
        if(!ownData.IsBuffed)
        {
            return NodeStates.SUCCESS;
        }
        else
        {
            return NodeStates.FAILURE;
        }
    }

    public void SetPlayerData(Player human, Player ai)
    {
        playerData = human;
        ownData = ai;
    }

    public void Evaluate()
    {
        rootNode.Evaluate();
        StartCoroutine(Execute());
    }
}
