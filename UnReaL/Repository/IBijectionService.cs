namespace UnReaL.Repository
{
    public interface IBijectionService
    {
        public string Encode(int num);
        public int Decode(string str);
    }
}
