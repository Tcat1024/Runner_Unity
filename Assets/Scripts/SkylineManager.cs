using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SkylineManager : MonoBehaviour {

    public Transform Perfab;
    public float RecycleRange;
    public int CubeCount;

    public Vector3 StartPosition;
    public Vector3 MinSize, MaxSize;
    private Vector3 NextPosition;


    private Queue<Transform> m_queueCubes;

    void PushQueue(Transform tran)
    {
        Vector3 localScale = new Vector3(
            Random.Range(MinSize.x, MaxSize.x),
            Random.Range(MinSize.y, MaxSize.y),
            Random.Range(MinSize.z, MaxSize.z)
            );
        Vector3 localPos = NextPosition;
        localPos.x += localScale.x * 0.5f;
        localPos.y += localScale.y * 0.5f;
        tran.localPosition = localPos;
        tran.localScale = localScale;
        NextPosition.x += localScale.x;
        m_queueCubes.Enqueue(tran);
    }

    void Recycle()
    {
        Transform tran = m_queueCubes.Dequeue();
        PushQueue(tran);
    }

	// Use this for initialization
	void Start ()
    {
        GameEventManager.GameStartEvent += GameStart;
        GameEventManager.GameOverEvent += GameOver;
        NextPosition = StartPosition;
        NextPosition.z = -100;
        m_queueCubes = new Queue<Transform>();
        for(int i = 0; i < CubeCount; ++i)
        {
            Transform tran = Instantiate(Perfab);
            PushQueue(tran);
        }
        Perfab.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_queueCubes.Peek().localPosition.x + RecycleRange < Runner.distanceTraveled)
            Recycle();
	}
    void GameStart()
    {
        NextPosition = StartPosition;
        for (int i = 0; i < CubeCount; ++i)
        {
            Recycle();
        }
        enabled = true;
    }

    void GameOver()
    {
        enabled = false;
    }
}
