using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnInfo", menuName = "ScriptableObjects/SpawnInfo")]
public class SpawnInfo : ScriptableObject
{
    public List<InfoElement> SpawnInfoElements = new List<InfoElement>();
}

[Serializable]
public class InfoElement
{
    public float SpawnTime;
    [Tooltip("from 0-14 where 0 is left and 14 is right side")]
    public int SpawnPosition;
}
