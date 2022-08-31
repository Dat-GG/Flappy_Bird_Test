using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	[SerializeField] private float waitTime;
	[SerializeField] private GameObject obstaclePrefabs;
	[SerializeField] private float range;
	[SerializeField] private Transform top;
	[SerializeField] private Transform bottom;
	private float tempTime;
	private int easy;
	private int hard;
	private PlayerController player;


	void Start(){
		tempTime = waitTime - Time.deltaTime;
		player = FindObjectOfType<PlayerController>();
	}

	void LateUpdate () {
		if(GameManager.Instance.GameState()){
			tempTime += Time.deltaTime;
			if(tempTime > waitTime){
				// Wait for some time, create an obstacle, then set wait time to 0 and start again
				tempTime = 0;
				if (easy < 3)
                {
					var a = Random.Range(-range, range);
					var pos = new Vector3(transform.position.x, transform.position.y + a, transform.position.z);
					GameObject pipeClone = Instantiate(obstaclePrefabs, pos, transform.rotation);
					easy++;
				}
                else
                {
                    if (player.transform.position.y >= 1)
                    {
						var a = Random.Range(bottom.transform.position.y, transform.position.y - 1.5f);
						var pos = new Vector3(transform.position.x, transform.position.y + a, transform.position.z);
						GameObject pipeClone = Instantiate(obstaclePrefabs, pos, transform.rotation);
					}
                    else
                    {
						var a = Random.Range(top.transform.position.y, transform.position.y + 1.5f);
						var pos = new Vector3(transform.position.x, transform.position.y + a, transform.position.z);
						GameObject pipeClone = Instantiate(obstaclePrefabs, pos, transform.rotation);
					}
					hard++;
                    if (hard > 2)
                    {
						hard = 0;
						easy = 0;
                    }
				}
				
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.transform.parent != null){
			Destroy(col.gameObject.transform.parent.gameObject);
		}else{
			Destroy(col.gameObject);
		}
	}

}
