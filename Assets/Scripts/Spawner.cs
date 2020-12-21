using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] objects;
	public float spawnTime;            // How long between each spawn.
	private Vector3 spawnPosition;
	public List<GameObject> plane;
	GameState gameState;
	public bool fixedTime;
	public bool fixedOrder;
	public bool oneTimeSpawn;
	public bool fixedPlaneOrder;
	public bool spawnInCenter;
	public bool spawnTriggered;
	bool alreadyTriggered = false;

	int spawnIndex = 0;
	int planeIndex = 0;


	// Use this for initialization
	void Start ()
	{
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.

	}

	void Update() {
		if(spawnTriggered && !alreadyTriggered) {
			Spawn();
			StartCoroutine(CountdownEnum(spawnTime));
			if (!fixedTime) {
				StartCoroutine(SpawnerCountdownEnum(10));
			}
			alreadyTriggered = true;
		}
	}

	void Spawn ()
	{
		if(gameState.gameStart && spawnTriggered) {
			if(GameObject.Find ("Body") != null) {
				GameObject player = GameObject.Find ("Body");
				if(player.GetComponent<Body>() != null) {
					Body playerBody = player.GetComponent<Body>();
					if (!playerBody.alive) {
						CancelInvoke ();
						Destroy(gameObject);
					}
				}
			} else {
				CancelInvoke();
			}
			IDictionary<string, int> indexes = getIndexes();
			if (!fixedPlaneOrder) {
				int planeBeforeIndex = (int) indexes["planeBeforeIndex"];
				int playerOnIndex = (int) indexes["playerOnIndex"];

				planeIndex = Random.Range(planeBeforeIndex, playerOnIndex);

			}
			float moveAreaX = plane[planeIndex].GetComponent<Renderer>().bounds.size.x / 2;
			float moveAreaZ = plane[planeIndex].GetComponent<Renderer>().bounds.size.z / 2;

			Vector3 center = plane[planeIndex].GetComponent<Renderer>().bounds.center;

			if (fixedPlaneOrder) {
				planeIndex++;
				if (planeIndex > plane.Count - 1) {
					planeIndex = 0;
				}
			}

			SetSpawnPosition(center, moveAreaX, moveAreaZ);

			if (!fixedOrder) {
				Instantiate(objects[UnityEngine.Random.Range(0, objects.Length)], spawnPosition, Quaternion.identity);
			} else {
				Instantiate(objects[spawnIndex], spawnPosition, Quaternion.identity);
			}
			spawnIndex++;

			if(spawnIndex > objects.Length - 1) {
				if(oneTimeSpawn) {
					CancelInvoke();
					Destroy(gameObject);
				} else {
					spawnIndex = 0;
				}

			}
		}
		if(gameState.gameOver || gameState.gameWin) {
			CancelInvoke ();
			Destroy(gameObject);
		}

	}

	public void TriggerSpawn() {
		spawnTriggered = true;
	}

	void SetSpawnPosition(Vector3 center, float moveAreaX, float moveAreaZ) {
		float scale = 0.5f;
		spawnPosition.x = center.x;
		spawnPosition.y = transform.position.y;
		spawnPosition.z = center.z;
		if(!spawnInCenter) {
			spawnPosition.x += Random.Range(-moveAreaX*scale, moveAreaX*scale);
			spawnPosition.z += Random.Range(-moveAreaZ*scale, moveAreaZ*scale);
		}
	}

	IDictionary<string, int> getIndexes() {
		if (plane.Count == 1) {
			return new Dictionary<string, int>() {{"playerOnIndex", 0}, {"planeBeforeIndex", 0}};
		}
		IDictionary<string, int> indexes = new Dictionary<string, int>();
		int planeBeforeIndex = 0;
		int playerOnIndex = plane.FindIndex(p => p.GetComponent<Ground>().isPlayerOn());
		if (playerOnIndex == -1 || playerOnIndex == 0) {
			playerOnIndex = 2; // Random.Range excludes max number
		} else if (playerOnIndex == plane.Count - 1) {
			planeBeforeIndex = plane.Count - 1;
		} else {
			playerOnIndex += 2; // Random.Range excludes max number
			planeBeforeIndex = playerOnIndex - 1;
		}
		indexes.Add("playerOnIndex", playerOnIndex);
		indexes.Add("planeBeforeIndex", planeBeforeIndex);
		return indexes;
	}

	IEnumerator CountdownEnum(float seconds)
    {
        float count = seconds;
        while (count > 0) {
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        Spawn();
		StartCoroutine(CountdownEnum(spawnTime));


    }
	IEnumerator SpawnerCountdownEnum(float seconds)
    {
        float count = seconds;
        while (count > 0) {
            // display something...
            yield return new WaitForSeconds(1);
            count --;
        }

        // count down is finished...
        if(spawnTime > 1) {
			spawnTime--;
		}
		StartCoroutine(SpawnerCountdownEnum(10));

    }
}
