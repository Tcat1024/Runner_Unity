using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

    public Vector3 RoateForce;
    public Vector3 PosOffset;
    public float RecycleDistence;
    public int AvalibleChancePer;
	// Use this for initialization
	void Start () {
        GameEventManager.GameOverEvent += GameOver;

        gameObject.SetActive(false);
	}
	
    public void TrySetPosition(Vector3 position)
    {
        if(gameObject.activeSelf || AvalibleChancePer >= Random.Range(0, 100))
        {
            return;
        }
        transform.position = position + PosOffset;
        gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if(Runner.distanceTraveled - transform.position.x > RecycleDistence)
        {
            gameObject.SetActive(false);
        }
        transform.Rotate(RoateForce * Time.deltaTime);
	}

    void OnTriggerEnter()
    {
        Runner.AddBoost();
        gameObject.SetActive(false);
    }

    void GameOver()
    {
        gameObject.SetActive(false);
    }
}
