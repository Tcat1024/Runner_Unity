using UnityEngine;
using System.Collections;

public static class GameEventManager {

    public delegate void GameEvent();
    public static event GameEvent GameStartEvent;
    public static event GameEvent GameOverEvent;

    public static void TriggerGameStartEvent()
    {
        if(GameStartEvent != null)
        {
            GameStartEvent();
        }
    }

    public static void TriggerGameOverEvent()
    {
        if (GameOverEvent != null)
        {
            GameOverEvent();
        }
    }

}
