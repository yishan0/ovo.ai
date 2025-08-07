using UnityEngine;
using System;
using System.Collections.Generic;

public enum GameState {
    Booting,
    Login,
    AssistantGreeting,
    DesktopIdle,
    AssistantCreeping,
    ControlLoss,
    LockedDown,
    End
}
public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem Instance;

    public GameState CurrentState { get; private set; } = GameState.Booting;

    // Events that other scripts can listen to
    public event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetGameState(GameState newState)
    {
        if (newState == CurrentState) return;

        CurrentState = newState;
        Debug.Log($"Game state changed to: {newState}");

        // Notify listeners
        OnGameStateChanged?.Invoke(newState);
    }
    public GameState GetGameState()
    {
        return CurrentState;
    }
}