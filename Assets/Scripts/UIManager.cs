using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public UILabel GameTitleLabel;
    public UILabel GameOverLabel;
    public UILabel GameStartLabel;
    public UILabel BoostCountLabel;
    public UILabel DistenceLabel;
    public UIAnchor GameUIAnchor;
    public UIAnchor MenuUIAnchor;

    static public UIManager Instance { get; private set; }

    public void SetBoostCount(int count)
    {
        BoostCountLabel.text = count.ToString();
    }

    public void SetDistence(int dist)
    {
        DistenceLabel.text = dist.ToString();
    }

	// Use this for initialization
	void Start () {
        Instance = this;
        GameEventManager.GameStartEvent += GameStart;
        GameEventManager.GameOverEvent += GameOver;
        GameUIAnchor.gameObject.SetActive(false);
        MenuUIAnchor.gameObject.SetActive(true);
        if (GameOverLabel)
            GameOverLabel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Jump"))
        {
            GameEventManager.TriggerGameStartEvent();
        }
	}

    void GameStart()
    {
        GameUIAnchor.gameObject.SetActive(true);
        MenuUIAnchor.gameObject.SetActive(false);
        enabled = false;
    }

    void GameOver()
    {
        MenuUIAnchor.gameObject.SetActive(true);
        if (GameOverLabel)
            GameOverLabel.gameObject.SetActive(true);
        enabled = true;
    }
}
