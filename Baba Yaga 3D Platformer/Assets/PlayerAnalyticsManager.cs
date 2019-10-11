using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerAnalyticsManager : MonoBehaviour
{
    int noOfPlayerDeaths;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    public void PlayerController_OnPlayerDied(Vector3 deathPosition)
    {
        noOfPlayerDeaths += 1;
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("Position", deathPosition);
        data.Add("No of Deaths", noOfPlayerDeaths);
        Analytics.CustomEvent("PLAYER_DIED", data);
    }

    private void OnDestroy()
    {
        Analytics.FlushEvents();
    }

}
