using UnityEngine;
using System.Collections;

public class ParticleSystemManager : MonoBehaviour {
    public ParticleSystem[] ParticleSystems;

	// Use this for initialization
	void Start () {
        GameEventManager.GameStartEvent += GameStart;
        GameEventManager.GameOverEvent += GameOver;
        GameOver();
    }
	
    void GameStart()
    {
        foreach (ParticleSystem sys in ParticleSystems)
        {
            sys.Clear();
            sys.enableEmission = true;
        }
    }

    void GameOver()
    {
        foreach (ParticleSystem sys in ParticleSystems)
        {
            sys.enableEmission = false;
        }
    }
}
