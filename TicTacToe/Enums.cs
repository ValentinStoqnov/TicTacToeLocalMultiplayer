namespace TicTacToe
{
    public enum OperationFlags
    {
        Null = 0,
        MessageString = 1,
        Request = 2,
        GameAction = 3
    }
    public enum RequestTypes 
    { 
        ServerName = 1,
        StartGame = 2
    }
    public enum GameActions
    {
        TopLeftButtonClicked = 11,
        TopMiddleButtonClicked = 12,
        TopRightButtonClicked = 13,
        CenterLeftButtonClicked= 21,
        CenterMiddleButtonClicked = 22,
        CenterRightButtonClicked = 23,
        BottomLeftButtonClicked = 31,
        BottomMiddleButtonClicked = 32,
        BottomRightButtonClicked = 33
    }

    public enum ClientType
    { 
        Client,
        Server
    }


}
