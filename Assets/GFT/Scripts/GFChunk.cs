using System.Collections.Generic;
using System.Linq;
using R3;

namespace GFT
{
    public class GFChunk
    {
        private readonly IEnumerable<IGFTrigger> _triggers;
        private readonly IEnumerable<IGFAction> _actions;

        public Observable<Unit> Trigger => _triggers.Select(t => t.Progress).Last().Select(t => Unit.Default);

        public GFChunk(IEnumerable<IGFTrigger> triggers, IEnumerable<IGFAction> actions)
        {
            _triggers = triggers;
            _actions = actions;
        }

        public Observable<Unit> ExecuteActions()
        {
            return Observable.Concat(_actions.Select(a => a.Execute()));
        }
    }
}
