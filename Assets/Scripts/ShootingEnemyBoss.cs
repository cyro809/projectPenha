using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyBoss : ShootingEnemy
{
    public Transform[] points;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    Transform headTransform;
    // Start is called before the first frame update
    public override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        headTransform = enemyGun.transform.Find("ShootingEnemyBossHead");
        Debug.Log(headTransform);
        // GetComponent<Rigidbody>().isKinematic = true;
        // GetComponent<Rigidbody>().useGravity = false;
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        GotoNextPoint();
        base.Start ();
    }

    protected override void OnMove()
    {
        if(agent.enabled) {
            Vector3 direction = GetLookAtDirection(agent.destination);
            if (!agent.pathPending && agent.remainingDistance < 0.5f) {
                GotoNextPoint();

            } else if (agent.remainingDistance >= 0.5f){
                // rB.AddForce (points[destPoint].position * 2);
            }
            rB.AddForce (direction * 5);
        }
        else {
            base.OnMove();
        }
        headTransform.position = new Vector3 (transform.position.x, headTransform.position.y, transform.position.z);

        enemyGun.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        enemyGun.transform.LookAt(player.transform.position);


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
            // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            // GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;

            StartCoroutine(CooldownCount(1));
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
        // GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        agent.enabled = true;
    }
}
