using UnityEngine;
using System.Collections.Generic;
using System;

namespace FailureType {

    public static class FullFeatureList
    {
        public static string ferrousSize = "ferrousSize";
        public static string nonFerrousSize = "nonFerrousSize";
        public static string ferrousCount = "ferrousCount";
        public static string nonFerrousCount = "nonFerrousCount";

        public static string peak = "peakvalue";
        public static string rms = "rmsvalue";
        public static string cf = "cfvalue";
        public static string kurtosis = "kurtosis";
        public static string fm4 = "fm4value";
        public static string na4 = "na4value";
    }

    public abstract class BaseFailure
    {
        public Dictionary<string, int> featureList;
        public Dictionary<string, DateTime> firstOccurenceList;
        public int featureCountMax;
        public int failedFeatureCount;
        public string displayString;
        public int totalOccurence;

        public BaseFailure()
        {
            featureList = new Dictionary<string, int>();
            firstOccurenceList = new Dictionary<string, DateTime>();
            populate();
            failedFeatureCount = 0;
            totalOccurence = 0;
        }

        public abstract void populate();
        public abstract bool checkFailure();
        public abstract string getDisplayString();

        public string getFailurePercentage()
        {
            double percentageValue = 0.0;
            if (totalOccurence > 360)
            {
                percentageValue = 50.0 * ((1.0 * failedFeatureCount) / (1.0 * featureCountMax)) + 50.0;
            }
            else
            {
                Debug.Log("Less than 360");
                percentageValue = 50.0 * ((1.0 * failedFeatureCount) / (1.0 * featureCountMax)) + 50.0 * ((1.0 * totalOccurence) / 360);
            }

            return Math.Round(percentageValue * 0.9 , 2).ToString();
        }
    }

    public class GearWearFailure : BaseFailure
    {
        public override bool checkFailure()
        {
            bool result = false;
            displayString = "";
            if (firstOccurenceList.ContainsKey(FullFeatureList.ferrousSize))
            {
                if (featureList[FullFeatureList.ferrousSize] >= 10)
                {
                    result = true;
                    displayString += ("Ferrous Wear Debris Size Exceeded, " + firstOccurenceList[FullFeatureList.ferrousSize].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.ferrousCount))
            {
                if (featureList[FullFeatureList.ferrousCount] >= 10)
                {
                    result = true;
                    displayString += ("Ferrous Wear Debris Count Exceeded, " + firstOccurenceList[FullFeatureList.ferrousCount].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.nonFerrousSize))
            {
                if (featureList[FullFeatureList.nonFerrousSize] >= 10)
                {
                    result = true;
                    displayString += ("Non-Ferrous Wear Debris Size Exceeded, " + firstOccurenceList[FullFeatureList.nonFerrousSize].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.nonFerrousCount))
            {
                if (featureList[FullFeatureList.nonFerrousCount] >= 10)
                {
                    result = true;
                    displayString += ("Non-Ferrous Wear Debris Count Exceeded, " + firstOccurenceList[FullFeatureList.nonFerrousCount].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.peak))
            {
                if (featureList[FullFeatureList.peak] >= 10)
                {
                    result = true;
                    displayString += ("Peak Value Exceeded, " + firstOccurenceList[FullFeatureList.peak].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.kurtosis))
            {
                if (featureList[FullFeatureList.kurtosis] >= 10)
                {
                    result = true;
                    displayString += ("Kurtosis value Exceeded, " + firstOccurenceList[FullFeatureList.kurtosis].ToShortTimeString() + "\n");
                }
            }
            return result;
        }

        

        public override string getDisplayString()
        {
            return displayString;
        }

        public override void populate()
        {
            featureCountMax = 6;
            featureList[FullFeatureList.ferrousSize] = 0;
            featureList[FullFeatureList.ferrousCount] = 0;
            featureList[FullFeatureList.nonFerrousSize] = 0;
            featureList[FullFeatureList.nonFerrousCount] = 0;
            featureList[FullFeatureList.peak] = 0;
            featureList[FullFeatureList.kurtosis] = 0;
        }
    }

    public class GearFatigueFailure : BaseFailure
    {

        public override void populate()
        {
            featureCountMax = 2;
            featureList[FullFeatureList.peak] = 0;
            featureList[FullFeatureList.kurtosis] = 0;
        }

        public override bool checkFailure()
        {
            bool result = false;
            displayString = "";
            if (firstOccurenceList.ContainsKey(FullFeatureList.peak))
            {
                if (featureList[FullFeatureList.peak] >= 10)
                {
                    result = true;
                    displayString += ("Peak Value Exceeded, " + firstOccurenceList[FullFeatureList.peak].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.kurtosis))
            {
                if (featureList[FullFeatureList.kurtosis] >= 10)
                {
                    result = true;
                    displayString += ("Kurtosis value Exceeded, " + firstOccurenceList[FullFeatureList.kurtosis].ToShortTimeString() + "\n");
                }
            }
            return result;
        }

        public override string getDisplayString()
        {
            return displayString;
        }
    }

    public class GearPittingFailure : BaseFailure
    {

        public override void populate()
        {
            featureCountMax = 3;
            featureList[FullFeatureList.cf] = 0;
            featureList[FullFeatureList.fm4] = 0;
            featureList[FullFeatureList.na4] = 0;
        }

        public override bool checkFailure()
        {
            bool result = false;
            displayString = "";
            if (firstOccurenceList.ContainsKey(FullFeatureList.cf))
            {
                if (featureList[FullFeatureList.cf] >= 10)
                {
                    result = true;
                    displayString += ("Crest Factor Exceeded, " + firstOccurenceList[FullFeatureList.cf].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.fm4))
            {
                if (featureList[FullFeatureList.fm4] >= 10)
                {
                    result = true;
                    displayString += ("FM4 value Exceeded, " + firstOccurenceList[FullFeatureList.fm4].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.na4))
            {
                if (featureList[FullFeatureList.na4] >= 10)
                {
                    result = true;
                    displayString += ("NA4 value Exceeded, " + firstOccurenceList[FullFeatureList.na4].ToShortTimeString() + "\n");
                }
            }
            return result;
        }

        public override string getDisplayString()
        {
            return displayString;
        }

    }

    public class BearingWearFailure : BaseFailure
    {

        public override void populate()
        {
            featureCountMax = 5;
            featureList[FullFeatureList.ferrousSize] = 0;
            featureList[FullFeatureList.ferrousCount] = 0;
            featureList[FullFeatureList.nonFerrousSize] = 0;
            featureList[FullFeatureList.nonFerrousCount] = 0;
            featureList[FullFeatureList.rms] = 0;
        }

        public override bool checkFailure()
        {
            bool result = false;
            displayString = "";
            if (firstOccurenceList.ContainsKey(FullFeatureList.ferrousSize))
            {
                if (featureList[FullFeatureList.ferrousSize] >= 10)
                {
                    result = true;
                    displayString += ("Ferrous Wear Debris Size Exceeded, " + firstOccurenceList[FullFeatureList.ferrousSize].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.ferrousCount))
            {
                if (featureList[FullFeatureList.ferrousCount] >= 10)
                {
                    result = true;
                    displayString += ("Ferrous Wear Debris Count Exceeded, " + firstOccurenceList[FullFeatureList.ferrousCount].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.nonFerrousSize))
            {
                if (featureList[FullFeatureList.nonFerrousSize] >= 10)
                {
                    result = true;
                    displayString += ("Non-Ferrous Wear Debris Size Exceeded, " + firstOccurenceList[FullFeatureList.nonFerrousSize].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.nonFerrousCount))
            {
                if (featureList[FullFeatureList.nonFerrousCount] >= 10)
                {
                    result = true;
                    displayString += ("Non-Ferrous Wear Debris Count Exceeded, " + firstOccurenceList[FullFeatureList.nonFerrousCount].ToShortTimeString() + "\n");
                }
            }
            if (firstOccurenceList.ContainsKey(FullFeatureList.rms))
            {
                if (featureList[FullFeatureList.rms] >= 10)
                {
                    result = true;
                    displayString += ("RMS Value Exceeded, " + firstOccurenceList[FullFeatureList.rms].ToShortTimeString() + "\n");
                }
            }
            return result;
        }

        public override string getDisplayString()
        {
            return displayString;
        }

    }

}
