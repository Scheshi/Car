using System.Collections.Generic;


namespace Assets.Scripts.Interfaces
{
    public interface IAnalytic
    {
        void SendMessage(string alias, IDictionary<string, object> eventData);
    }
}