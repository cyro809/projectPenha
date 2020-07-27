using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] objects;
	public float spawnTime = 6f;            // How long between each spawn.
	private Vector3 spawnPosition;
	public List<GameObject> plane;
	GameState gameState;
	
	// Use this for initialization
	void Start () 
	{
		gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		if(gameState.gameStart) {
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
			int planeBeforeIndex = (int) indexes["planeBeforeIndex"];
			int playerOnIndex = (int) indexes["playerOnIndex"];
			float scale = 0.5f;
			int planeIndex = Random.Range(planeBeforeIndex, playerOnIndex);
			float moveAreaX = plane[planeIndex].GetComponent<Renderer>().bounds.size.x / 2;
			float moveAreaZ = plane[planeIndex].GetComponent<Renderer>().bounds.size.z / 2;
			Vector3 center = plane[planeIndex].GetComponent<Renderer>().bounds.center;
			spawnPosition.x = center.x + Random.Range(-moveAreaX*scale, moveAreaX*scale);
			spawnPosition.y = transform.position.y;
			spawnPosition.z = center.z + Random.Range(-moveAreaZ*scale, moveAreaZ*scale);

			Instantiate(objects[UnityEngine.Random.Range(0, objects.Length - 1)], spawnPosition, Quaternion.identity);
		}
		if(gameState.gameOver) {
			CancelInvoke ();
			Destroy(gameObject);
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
			playerOnIndex = 1;
		} else if (playerOnIndex == plane.Count - 1) {
			planeBeforeIndex = 3;
		} else {
			playerOnIndex++;
			planeBeforeIndex = playerOnIndex - 1;
		}
		indexes.Add("playerOnIndex", playerOnIndex);
		indexes.Add("planeBeforeIndex", planeBeforeIndex);
		return indexes;
	}
}
