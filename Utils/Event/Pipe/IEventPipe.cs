using System.Collections.Generic;

namespace Gullis
{
    public interface IEventPipe
    {
        IEvent PushEvent(IEvent inputEvent);
        
        List<IEventHandler> handlerSequence { get; }
    }
}