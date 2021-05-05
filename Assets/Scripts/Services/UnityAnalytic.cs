using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine.Analytics;

namespace Assets.Scripts.Services
{
    public class UnityAnalytic : IAnalytic
    {
        public void SendMessage(string alias, IDictionary<string, object> eventData)
        {
            if (eventData == null)
            {
                eventData = new Dictionary<string, object>();
            }

            Analytics.CustomEvent(alias, eventData);
        }
    }
}