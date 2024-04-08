[System.Flags]
public enum Direction
{
    None = 0,
    Up = 1,
    Down = 2,
    Right = 4,
    Left = 8,

    //Vertical = Up & Down,
    //Horizontal = Right & Left,

    //UpRight = Up & Right,
    //UpLeft = Up & Left,
    //LeftDown = Left & Down,
    //RightDown = Right & Down,

    //UpRightDown = Up & Right & Down,
    //UpLeftDown = Up & Left & Down,

    //LeftUpRight = Left & Up & Right,
    //LeftDownRight = Left & Down & Right,

    //All = Up & Down & Right & Left,
}
