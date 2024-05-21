using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersToday_TestTask
{

    public class Key
    {
        readonly DateTime firstKey;
        readonly DateTime seconfKey;
        readonly byte ThirdKey;
        public Key(DateTime firstKey, DateTime seconfKey, byte ThirdKey)
        {
            this.firstKey = firstKey;
            this.seconfKey = seconfKey;
            this.ThirdKey = ThirdKey;
        }
        public override bool Equals(object obj)
        {
            if ((object)this == obj)
                return true;

            var other = obj as Key;

            if ((object)other == null)
                return false;

            return EqualsHelper(this, other);
        }

        protected static bool EqualsHelper(Key thisKey, Key other)
        {
            return
                thisKey.firstKey == other.firstKey &&
                thisKey.seconfKey == other.seconfKey &&
                thisKey.ThirdKey == other.ThirdKey;

        }
    }
    public class DefaulModel
    {

        public DateTime tpep_pickup_datetime;
        public DateTime tpep_dropoff_datetime;
        public byte passenger_count; //tinyint in sql
        public float trip_distance;
        public string store_and_fwd_flag = string.Empty;
        public int PULocationID;
        public int DOLocationID;
        public float fare_amount;
        public float tip_amount;



    }



}

