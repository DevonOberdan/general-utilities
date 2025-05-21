using FinishOne.SaveSystem;
using System;
using UnityEngine;

[Serializable]
public class LevelProgressData : ISaveable
{
    [field: SerializeField] public string Id { get; set; }

    public int Index;
    public bool AllLevelsComplete;
}