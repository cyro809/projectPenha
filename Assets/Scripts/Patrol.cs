using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour {

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    Rigidbody rB;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        rB = gameObject.GetComponent<Rigidbody>();
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        int previousPoint = destPoint;
        System.Random rand = new System.Random();
        while (previousPoint == destPoint) {
            destPoint = rand.Next(0, points.Length);
        }
    }


    void Update () {
        // Choose the next destination point when the agent g ets
        // close to the current one.

        Vector3 direction = GetLookAtDirection(agent.destination);
        if (!agent.pathPending && agent.remainingDistance < 0.5f) {

            Debug.Log(direction);
            GotoNextPoint();

        } else if (agent.remainingDistance >= 0.5f){
            // rB.AddForce (points[destPoint].position * 2);
        }
        rB.AddForce (direction * 20);

    }

    Vector3 GetLookAtDirection(Vector3 nextPoint) {
		/*
		Code reference from: https://answers.unity.com/questions/239614/roll-a-ball-towards-the-player.html
		*/

		Vector3 myPosition = transform.position;

		// work out direction and distance
		return (nextPoint - myPosition).normalized;
	}

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Bullet")) {

        }
    }
}