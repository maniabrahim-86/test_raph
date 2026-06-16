using System.Collections.Generic;
using System.Threading.Tasks;

namespace testRaph.core.services
{
    public interface IMessengerService
    {
        event Action? DataRefreshed;
        void PublishMessage();
    }
    public class MessengerService : IMessengerService
    {
        public event Action? DataRefreshed;

        public void PublishMessage()
        {
            DataRefreshed?.Invoke();
        }
    }
}
