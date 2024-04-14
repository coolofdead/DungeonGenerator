using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Data", menuName = "Dungeon/Dungeon Data", order = 1)]
public class DungeonData : ScriptableObject
{
    [Header("Map")]
    // Name, Icon, Avg LV
    public DungeonSizeType SizeType;
    public int TotalFloor;

    [field: Header("Room")]
    [field: SerializeField] public List<DataByFloor<DungeonRoomData>> RoomDataByFloor { get; protected set; }
    public DungeonRoomData DungeonRoomData(int floor) => RoomDataByFloor.First(dataByFloor => dataByFloor.floorRange.InRange(floor)).data;

    [field:Header("Corridor")]
    [field: SerializeField] public List<DataByFloor<DungeonCorridorData>> CorridorDataByFloor { get; protected set; }
    public DungeonCorridorData DungeonCorridorData(int floor) => CorridorDataByFloor.First(dataByFloor => dataByFloor.floorRange.InRange(floor)).data;

    [field:Header("Items")]
    [field: SerializeField] public List<DataByFloor<DungeonItemData>> ItemDataByFloor { get; protected set; }
    public DungeonItemData DungeonItemData(int floor) => ItemDataByFloor.First(dataByFloor => dataByFloor.floorRange.InRange(floor)).data;

    [Header("Mobs")]

    [Header("Floors")]

    [Header("Tiles")]
    public int lol;

    [Serializable]
    public struct DataByFloor<T>
    {
        public Range floorRange;
        public T data;
    }
}
