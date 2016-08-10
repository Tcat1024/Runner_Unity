using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Runner : MonoBehaviour {
    [HideInInspector]
    static public float distanceTraveled;

    public float Acceleration = 50f;
    public Vector3 JumpVelocity;
    public Vector3 BoostVelocity;
    public float GameOverY = -6;
    [HideInInspector]
    static public int BoostCount { get; private set; }

    private bool m_bTouchingPlatform;
    private Rigidbody m_sRigidbody;
    private Vector3 m_vStartPosition;

    static public void AddBoost()
    {
        UIManager.Instance.SetBoostCount(++BoostCount);
    }
	// Use this for initialization
	void Start ()
    {
        GameEventManager.GameStartEvent += GameStart;
        GameEventManager.GameOverEvent += GameOver;
        distanceTraveled = transform.localPosition.x;
        m_sRigidbody = GetComponent<Rigidbody>();
        m_vStartPosition = transform.localPosition;
        m_sRigidbody.isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (m_bTouchingPlatform)
            {
                m_sRigidbody.AddForce(JumpVelocity, ForceMode.VelocityChange);
                m_bTouchingPlatform = false;
            }
            else if(BoostCount > 0)
            {
                m_sRigidbody.AddForce(BoostVelocity, ForceMode.VelocityChange);
                UIManager.Instance.SetBoostCount(--BoostCount);
            }
        }
        

        distanceTraveled = transform.localPosition.x;
        UIManager.Instance.SetDistence((int)distanceTraveled);

        if (transform.localPosition.y < GameOverY)
        {
            GameEventManager.TriggerGameOverEvent();
        }
    }

    void FixedUpdate()
    {
        if(m_bTouchingPlatform)
        {
            m_sRigidbody.AddForce(Acceleration, 0f, 0f, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter()
    {
        m_bTouchingPlatform = true;
    }

    void OnCollisionExit()
    {
        m_bTouchingPlatform = false;
    }

    void GameStart()
    {
        m_sRigidbody.isKinematic = false;
        transform.localPosition = m_vStartPosition;
        distanceTraveled = transform.localPosition.x;
        BoostCount = 0;
        GetComponent<MeshRenderer>().enabled = true;
        UIManager.Instance.SetBoostCount(0);
        UIManager.Instance.SetDistence(0);
        enabled = true;
    }

    void GameOver()
    {
        m_sRigidbody.isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        enabled = false;
    }
}
