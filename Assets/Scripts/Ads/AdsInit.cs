using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(MyRewardedAds))]
public class AdsInit : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;
    private MyRewardedAds _myRewarded;

    private static AdsInit _myAds;
    
    void Awake()
    {
        if (_myAds != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _myAds = this;
            DontDestroyOnLoad(this);
        }

        _myRewarded = GetComponent<MyRewardedAds>();
        InitializeAds();
    }

    private void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
    }

    public void OnInitializationComplete()
    {
        _myRewarded.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    
}
