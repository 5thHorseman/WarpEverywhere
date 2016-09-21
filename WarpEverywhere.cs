using System;
using UnityEngine;
// using System.Reflection;
// using System.Collections;
// using System.Collections.Generic;
// using KSP.UI.Dialogs;

namespace WarpEverywhere 
{

    [KSPAddon(KSPAddon.Startup.Flight, true)] //The "true" at the end means this will only run once, the first time you go into flight.
    public class WarpEverywhere : MonoBehaviour
    {
        public void Start()
        {
            // Create a reference to the time warp object.
            TimeWarp timeWarp = (TimeWarp)FindObjectOfType(typeof(TimeWarp));

            //Resize it, increasing it by 2.
            Array.Resize(ref timeWarp.warpRates, timeWarp.warpRates.Length + 2);

            //Make the last 2 entries 10x each previous entry. The highest one is 100x faster than the current fastest warp.
            timeWarp.warpRates[timeWarp.warpRates.Length - 2] = timeWarp.warpRates[timeWarp.warpRates.Length - 3] * 10;
            timeWarp.warpRates[timeWarp.warpRates.Length - 1] = timeWarp.warpRates[timeWarp.warpRates.Length - 2] * 10;

            //Resize each world's array and set all of its altitude limits to 0. This makes it so you can warp as fast as you want, as low as you want, everywhere.
            //Well, everywhere you can warp. If you can only pnysics warp, then you're stuck with that. :)
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