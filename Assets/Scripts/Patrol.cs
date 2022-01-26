using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MovingObject {

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    public override void Start () {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<Rigidbody>().isKinematic = true;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        GotoNextPoint();
        base.Start ();

    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        if (agent.enabled)
            agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        int previousPoint = destPoint;
        System.Random rand = new System.Random();
        while (previousPoint == destPoint) {
            destPoint = rand.Next(0, points.Length);
        }
    }

    protected override void OnMove() {
        if(agent.enabled) {
            Vector3 direction = GetLookAtDirection(agent.destination);
            if (!agent.pathPending && agent.remainingDistance < 0.5f) {
                GotoNextPoint();

            } else if (agent.remainingDistance >= 0.5f){
                // rB.AddForce (points[destPoint].position * 2);
            }
            rB.AddForce (direction * 5);
        }
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
            agent.enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Patrol>().enabled = false;
            StartCoroutine(CooldownCount(2));
            getHit(1000, transform.position);
        }
        if (col.gameObject.CompareTag("LimitPlane")) {
            Kill();
        }
    }

    void Kill() {
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    IEnumerator CooldownCount(int seconds)
    {
        int count = seconds;

        while (count > 0) {
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        gameObject.GetComponent<Patrol>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        agent.enabled = true;
    }
}