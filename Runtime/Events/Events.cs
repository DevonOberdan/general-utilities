using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public static PickupEvent onPickup = new PickupEvent();
    public static InventoryChangedEvent onInventoryChanged = new();
    public static SettingsChangedEvent onSettingsChanged = new();
    public static AudioSettingsChangedEvent onAudioSettingsChanged = new();
}


public class PickupEvent : GameSystemEvent
{ 
   // public ItemPickup item;
}

public class InventoryChangedEvent : GameSystemEvent
{
    public int itemId, itemCount;
}

public class SettingsChangedEvent : GameSystemEvent
{
   // public Settings changedSettings;
}

public class AudioSettingsChangedEvent : GameSystemEvent
{
    public SettingsAudio changedSettings;
}