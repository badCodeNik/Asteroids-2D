using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace Analytics
{
    public class FirebaseInit : IInitializable
    {
        public void Initialize()
        {
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log($"Firebase init failed {task.Exception}");
                    return;
                }

                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                Debug.Log("Firebase init success");
            });
        }
    }
}