using R3;

namespace GFT
{
    public class GFCounter
    {
        public Observable<int> Value => _value;
        
        private readonly ReactiveProperty<int> _value = new(0);

        public GFCounter(int value = 0)
        {
            _value.Value = value;
        }

        public void Increase(int value)
        {
            _value.Value += value;
        }
    }
}