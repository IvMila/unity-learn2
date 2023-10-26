using System;

public class GlobalEventManager 
{
    public static Action OnEnemyKilled;
    public static Action ItemCollected;
    public static Action OpeningDoor;

    public static void SendEnemyKilled()
    {
        if (OnEnemyKilled != null) OnEnemyKilled.Invoke();
    }

    public static void SendItemCollected()
    {
        if (ItemCollected != null) ItemCollected.Invoke();
    }

    public static void SendOpeningDoor()
    {
        if (OpeningDoor != null) OpeningDoor.Invoke();
    }
}
