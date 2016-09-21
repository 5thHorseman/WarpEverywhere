using System;
using UnityEngine;
// using System.Reflection;
// using System.Collections;
// using System.Collections.Generic;
// using KSP.UI.Dialogs;

namespace WarpEverywhere 
{

    [KSPAddon(KSPAddon.Startup.AllGameScenes, false)] //The "false" at the end means this will run every time you change scenes. KSP seems to reset time warp on every scene change.
    public class WarpEverywhere : MonoBehaviour
    {
        public void Start()
        {
            // Create a reference to the time warp object.
            TimeWarp timeWarp = (TimeWarp)FindObjectOfType(typeof(TimeWarp));

            if (timeWarp != null) //Only do the rest if there is a timewarp object to modify
            {
                //Resize it, increasing it by 2.
                Array.Resize(ref timeWarp.warpRates, 10);

                //Make the last 2 entries 10x each previous entry. The highest one is 100x faster than the current fastest warp.
                timeWarp.warpRates[8] = timeWarp.warpRates[7] * 10;
                timeWarp.warpRates[9] = timeWarp.warpRates[8] * 10;

                //Resize each world's array and set all of its altitude limits to 0. This makes it so you can warp as fast as you want, as low as you want, everywhere.
                //Well, everywhere you can warp. If you can only physics warp, then you're stuck with that. :)
                foreach (CelestialBody iCelestialBody in FlightGlobals.Bodies)
                {
                    Array.Resize(ref iCelestialBody.timeWarpAltitudeLimits, timeWarp.warpRates.Length);
                    for (int i = 0; i < iCelestialBody.timeWarpAltitudeLimits.Length; i++)
                    {
                        iCelestialBody.timeWarpAltitudeLimits[i] = 0;
                    }
                }
            }
        }
    }
}