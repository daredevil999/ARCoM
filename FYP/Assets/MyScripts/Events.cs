using System;
using System.Linq;

namespace EventsAndAnalysers
{

    //Events for each parameter
    public class CustomEventArgs : EventArgs
    {
        public DateTime datetime;
        public string message;
    }
    public class FerrousDebrisCountEventArgs : CustomEventArgs
    {
        public FerrousDebrisCountEventArgs (DateTime dt)
        {
            this.datetime = dt;
            this.message = "Ferrous Debris Count exceeded";
        }
    }
    public class FerrousDebrisSizeEventArgs : CustomEventArgs
    {
        public FerrousDebrisSizeEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "Ferrous Debris Size exceeded";
        }
    }
    public class NonFerrousDebrisCountEventArgs : CustomEventArgs
    {
        public NonFerrousDebrisCountEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "Non-Ferrous Debris Count exceeded";
        }
    }
    public class NonFerrousDebrisSizeEventArgs : CustomEventArgs
    {
        public NonFerrousDebrisSizeEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "Non-Ferrous Debris Size exceeded";
        }
    }

    public class PeakValueEventArgs : CustomEventArgs
    {
        public PeakValueEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "Peak Value exceeded";
        }
    }
    public class RMSValueEventArgs : CustomEventArgs
    {
        public RMSValueEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "RMS Value exceeded";
        }
    }
    public class CFValueEventArgs : CustomEventArgs
    {
        public CFValueEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "CF Value exceeded";
        }
    }
    public class KurtosisValueEventArgs : CustomEventArgs
    {
        public KurtosisValueEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "Kurtosis Value exceeded";
        }
    }
    public class FM4ValueEventArgs : CustomEventArgs
    {
        public FM4ValueEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "FM4 Value exceeded";
        }
    }
    public class NA4ValueEventArgs : CustomEventArgs
    {
        public NA4ValueEventArgs(DateTime dt)
        {
            this.datetime = dt;
            this.message = "NA4 Value exceeded";
        }
    }

    //Analysers for oil analysis and vibration analysis
    public class OilAnalyser
    {
        public event EventHandler<CustomEventArgs> RaiseFerrousDebrisCountEvent;
        public event EventHandler<CustomEventArgs> RaiseNonFerrousDebrisCountEvent;
        public event EventHandler<CustomEventArgs> RaiseFerrousDebrisSizeEvent;
        public event EventHandler<CustomEventArgs> RaiseNonFerrousDebrisSizeEvent;

        private static int debrisCountThreshold = 70;
        private static int debrisSizeThreshold = 50;

        public void MonitorOilReading (string ferrousDebrisCount, string ferrousDebrisSize, 
                                       string nonFerrousDebrisCount, string nonFerrousDebrisSize)
        {
            int ferrousDebrisCountInt;
            int ferrousDebrisSizeInt;
            int nonFerrousDebrisCountInt;
            int nonFerrousDebrisSizeInt;

            string ferrousDebrisSizeStr = ferrousDebrisSize.Split('-').Last() ;
            string nonFerrousDebrisSizeStr = nonFerrousDebrisSize.Split('-').Last();

            int.TryParse(ferrousDebrisCount, out ferrousDebrisCountInt);
            int.TryParse(nonFerrousDebrisCount, out nonFerrousDebrisCountInt);
            int.TryParse(ferrousDebrisSizeStr, out ferrousDebrisSizeInt);
            int.TryParse(nonFerrousDebrisSizeStr, out nonFerrousDebrisSizeInt);

            if (ferrousDebrisCountInt > debrisCountThreshold)
            {
                OnRaiseFerrousDebrisCountEvent(new FerrousDebrisCountEventArgs(System.DateTime.Now));
            }

            if (nonFerrousDebrisCountInt > debrisCountThreshold)
            {
                OnRaiseNonFerrousDebrisCountEvent(new NonFerrousDebrisCountEventArgs(System.DateTime.Now));
            }

            if (ferrousDebrisSizeInt > debrisSizeThreshold)
            {
                OnRaiseFerrousDebrisSizeEvent(new FerrousDebrisSizeEventArgs(System.DateTime.Now));
            }

            if (nonFerrousDebrisSizeInt > debrisSizeThreshold)
            {
                OnRaiseNonFerrousDebrisSizeEvent(new NonFerrousDebrisSizeEventArgs(System.DateTime.Now));
            }


        }
        protected virtual void OnRaiseFerrousDebrisCountEvent(CustomEventArgs e)
        {
            if (RaiseFerrousDebrisCountEvent != null)
            {
                RaiseFerrousDebrisCountEvent(this, e);
            }
        }
        protected virtual void OnRaiseNonFerrousDebrisCountEvent(CustomEventArgs e)
        {
            if (RaiseNonFerrousDebrisCountEvent != null)
            {
                RaiseNonFerrousDebrisCountEvent(this, e);
            }
        }
        protected virtual void OnRaiseFerrousDebrisSizeEvent(CustomEventArgs e)
        {
            if (RaiseFerrousDebrisSizeEvent != null)
            {
                RaiseFerrousDebrisSizeEvent(this, e);
            }
        }
        protected virtual void OnRaiseNonFerrousDebrisSizeEvent(CustomEventArgs e)
        {
            if (RaiseNonFerrousDebrisSizeEvent != null)
            {
                RaiseNonFerrousDebrisSizeEvent(this, e);
            }
        }
    }
    
    public class VibrationAnalyser
    {
        public event EventHandler<CustomEventArgs> RaisePeakValueEvent;
        public event EventHandler<CustomEventArgs> RaiseRMSValueEvent;
        public event EventHandler<CustomEventArgs> RaiseCFValueEvent;
        public event EventHandler<CustomEventArgs> RaiseKurtosisValueEvent;
        public event EventHandler<CustomEventArgs> RaiseFM4ValueEvent;
        public event EventHandler<CustomEventArgs> RaiseNA4ValueEvent;

        private static double peakValueThreshold = 5.0;
        private static double rmsValueThreshold = 5.0;
        private static double cfValueUpperThreshold = 6.0;
        private static double cfValueLowerThreshold = 2.0;
        private static double kurtosisValueTarget = 3.0;
        private static double fm4ValueThreshold = 3.0;
        private static double na4ValueThreshold = 3.0;

        public void MonitorVibrationReading (string peakValueStr, string rmsValueStr, string cfValueStr,
                                             string kurtosisValueStr, string fm4ValueStr, string na4ValueStr)
        {
            double peakValue;
            double rmsValue;
            double cfValue;
            double kurtosisValue;
            double fm4Value;
            double na4Value;

            double.TryParse(peakValueStr, out peakValue);
            double.TryParse(rmsValueStr, out rmsValue);
            double.TryParse(cfValueStr, out cfValue);
            double.TryParse(kurtosisValueStr, out kurtosisValue);
            double.TryParse(fm4ValueStr, out fm4Value);
            double.TryParse(na4ValueStr, out na4Value);

            if (peakValue > peakValueThreshold)
            {
                OnRaisePeakValueEvent(new PeakValueEventArgs(System.DateTime.Now));
            }
            if (rmsValue > rmsValueThreshold)
            {
                OnRaiseRMSValueEvent(new RMSValueEventArgs(System.DateTime.Now));
            }
            if (!(cfValue > cfValueLowerThreshold && cfValue < cfValueUpperThreshold))
            {
                OnRaiseCFValueEvent(new CFValueEventArgs(System.DateTime.Now));
            }
            if (Math.Abs(kurtosisValue - kurtosisValueTarget) > 1.0)
            {
                OnRaiseKurtosisValueEvent(new KurtosisValueEventArgs(System.DateTime.Now));
            }
            if (fm4Value > fm4ValueThreshold)
            {
                OnRaiseFM4ValueEvent(new FM4ValueEventArgs(System.DateTime.Now));
            }
            if (na4Value > na4ValueThreshold)
            {
                OnRaiseNA4ValueEvent(new NA4ValueEventArgs(System.DateTime.Now));
            }

        }

        protected virtual void OnRaisePeakValueEvent(CustomEventArgs e)
        {
            if (RaisePeakValueEvent != null)
            {
                RaisePeakValueEvent(this, e);
            }
        }
        protected virtual void OnRaiseRMSValueEvent(CustomEventArgs e)
        {
            if (RaiseRMSValueEvent != null)
            {
                RaiseRMSValueEvent(this, e);
            }
        }
        protected virtual void OnRaiseCFValueEvent(CustomEventArgs e)
        {
            if (RaiseCFValueEvent != null)
            {
                RaiseCFValueEvent(this, e);
            }
        }
        protected virtual void OnRaiseKurtosisValueEvent(CustomEventArgs e)
        {
            if (RaiseKurtosisValueEvent != null)
            {
                RaiseKurtosisValueEvent(this, e);
            }
        }
        protected virtual void OnRaiseFM4ValueEvent(CustomEventArgs e)
        {
            if (RaiseFM4ValueEvent != null)
            {
                RaiseFM4ValueEvent(this, e);
            }
        }
        protected virtual void OnRaiseNA4ValueEvent(CustomEventArgs e)
        {
            if (RaiseNA4ValueEvent != null)
            {
                RaiseNA4ValueEvent(this, e);
            }
        }
    }
    
}

