using R3;

namespace GFT
{
    public interface IGFTrigger
    {
        public int Id { get; }
        public Observable<int> Progress { get; }
    }
}