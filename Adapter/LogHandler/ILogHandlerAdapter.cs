namespace A320_Cockpit.Adapter.LogHandler
{
    public interface ILogHandlerAdapter
    {
        public string LogPath { get; }

        public void Info(string message);

        public void Info(Exception e);

        public void Warning(string message);

        public void Warning(Exception e);

        public void Error(string message);

        public void Error(Exception e);

    }
}
