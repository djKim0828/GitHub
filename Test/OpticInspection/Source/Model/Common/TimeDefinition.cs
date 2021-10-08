using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.App.OpticInspection
{
    public class TimeDefinition
    {
        public TimeDefinition()
        {
            _startTime = string.Empty;
            _endtime = string.Empty;
        }
        ~TimeDefinition()
        {
        }
        public string _endtime { get; set; }
        public string _startTime { get; set; }

        public string GetInterval()
        {
            string result = string.Empty;
            if (_startTime == string.Empty)
            {
                return "Please, mark the start time";
            } // else

            if (_endtime == string.Empty)
            {
                return "Please, mark the end time";
            } // else

            //starttime이 endtime보다 늦는 경우 선택 불가
            if (IsValidTime(_startTime, _endtime) == false)
            {
                return "Please, check the end time";
            } //else

            TimeSpan ts = Convert.ToDateTime(_endtime) - Convert.ToDateTime(_startTime);

            return string.Format("{0}.{1}", ts.Seconds, ts.Milliseconds);
        }

        private bool IsValidTime(string startTime, string endTime)
        {
            if (string.IsNullOrEmpty(startTime) == false && string.IsNullOrEmpty(endTime) == false)
            {
                if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                {
                    return false;
                } //else
            } //else
            return true;
        }
    }
}
