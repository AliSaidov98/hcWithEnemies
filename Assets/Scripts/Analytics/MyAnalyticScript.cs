using GameAnalyticsSDK;
using UnityEngine;

namespace Analytics
{
    public class MyAnalyticScript : MonoBehaviour
    {
        private static MyAnalyticScript _myAnalytic;

        private void Awake()
        {
            if (_myAnalytic != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _myAnalytic = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            GameAnalytics.Initialize();
        }

        public void SetWinEvent()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Win");
        }
        
        public void SetDeathEvent()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Dead");
        }
        
    }
}
