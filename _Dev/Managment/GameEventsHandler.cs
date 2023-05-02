// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using UnityEngine;

public static class GameEventsHandler
{
    public static readonly PlayerDeathEvent PlayerDeathEvent = new PlayerDeathEvent();
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly PlayerProgressEvent PlayerProgressEvent = new PlayerProgressEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly PlayerVaultEvent PlayerVaultEvent = new PlayerVaultEvent();
    public static readonly PlayerTakeDamageEvent PlayerTakeDamageEvent = new PlayerTakeDamageEvent();
    public static readonly BulletsPickUpEvent BulletsPickUpEvent = new BulletsPickUpEvent();
    public static readonly PlayerTargetChangeEvent PlayerTargetChangeEvent = new PlayerTargetChangeEvent();
    public static readonly BulletsNumberChangeEvent BulletsNumberChangeEvent = new BulletsNumberChangeEvent();
    public static readonly FinishSpawnedEvent FinishSpawnedEvent = new FinishSpawnedEvent();
    public static readonly PlayerFinishPassedEvent PlayerFinishPassedEvent = new PlayerFinishPassedEvent();
    public static readonly PlayerOnEntryPointEvent PlayerOnEntryPointEvent = new PlayerOnEntryPointEvent();
    public static readonly FinisherEndEvent FinisherEndEvent = new FinisherEndEvent();
    public static readonly FinisherEnemyKilledEvent FinisherEnemyKilledEvent = new FinisherEnemyKilledEvent();
    public static readonly BoostPickUpEvent BoostPickUpEvent = new BoostPickUpEvent();
}

public class GameEvent {}

public class GameStartEvent : GameEvent
{
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class PlayerProgressEvent : GameEvent
{
    
}

public class MoneyCollectEvent : GameEvent
{
    
}

public class PlayerVaultEvent : GameEvent
{
}

public class PlayerTakeDamageEvent : GameEvent
{
    public int Damage;
}

public class FinishSpawnedEvent : GameEvent
{
}

public class PlayerTargetChangeEvent : GameEvent
{
    public Transform Target;
}

public class PlayerDeathEvent : GameEvent 
{
    
}

public class BulletsNumberChangeEvent : GameEvent
{
    public int Number;
    public int Max;
}

public class BulletsPickUpEvent : GameEvent
{
    public int Number;
}

public class PlayerFinishPassedEvent : GameEvent
{
    public Vector3 PlayerPos;
}

public class PlayerOnEntryPointEvent : GameEvent
{
    
}

public class FinisherEndEvent : GameEvent
{
    
}

public class FinisherEnemyKilledEvent : GameEvent
{
    
}
public class BoostPickUpEvent : GameEvent{}


