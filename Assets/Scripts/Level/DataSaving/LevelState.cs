using System;
using System.Collections.Generic;

[Serializable]
public class LevelState
{
    public List<CloneState> cloneStates;
    //public Vector3 playerPosition;
    public SerializableVector3 playerPosition;
    public bool levelFailed;

    public LevelState()
    {
        cloneStates = new List<CloneState>();
    }
}
