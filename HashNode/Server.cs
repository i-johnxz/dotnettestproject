namespace HashNode
{
    public class Server : NodeInfo
    {
        public string IP { get; set; }

        public override string NodeName
        {
            get => IP;
        }
    }
}