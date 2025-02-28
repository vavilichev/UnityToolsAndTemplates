using R3;

namespace GFT
{
    public interface IGFAction
    {
        public Observable<Unit> Execute();
    }
}