using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using static PlayerMovement;

public class CloneMovement : MonoBehaviour
{
    //public PlayerMovement PlayerMovement;
    public List<Vector2> Way;
    //public PlayerMoves olderMoves;

    //private Queue<Movements> movements;
    //void Awake()
    //{        
    //    movements = olderMoves?.moves;
    //    playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;
    //}
    public void LoadWay()//выгрузка сохраненного пути
    {
        string Data = PlayerPrefs.GetString("Positions", "");
        if (!string.IsNullOrEmpty(Data))
        {
            Way.Clear();
            string[] PositionsData = Data.Split(';');
            for (int i = 0; i < PositionsData.Length; i++)
            {
                if (!string.IsNullOrEmpty(PositionsData[i]))
                {
                    string[] PositionValues = PositionsData[i].Split(',');
                    if (PositionValues.Length == 2)
                    {
                        Way.Add(new Vector2(float.Parse(PositionValues[0]), float.Parse(PositionValues[1])));
                    }
                }
            }
        }
    }
    //private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    //{
    //    Movements result;
    //    if (movements != null && movements.TryDequeue(out result))
    //    {
    //        switch (result)
    //        {
    //            case Movements.Wait: break;
    //            case Movements.Up: transform.position += Vector3.forward; break;
    //            case Movements.Down: transform.position += Vector3.back; break;
    //            case Movements.Left: transform.position += Vector3.left; break;
    //            case Movements.Right: transform.position += Vector3.right; break;
    //        }
    //    }
    //}
}
