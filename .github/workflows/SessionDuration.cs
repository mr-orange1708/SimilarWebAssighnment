using System;
using System.Collections.Generic;
using System.Text;

namespace SimilarWebAssighnment
{
    class SessionDuration
    {
        const int MAX_SESSION_TIME_IN_SECONDS = 30 * 60;
        long sessionStartTime;
        long sessionLastVisitTime;

        public SessionDuration(long iSessionStartTime)
        {
            sessionStartTime = sessionLastVisitTime = iSessionStartTime;
        }

        public bool IsTheSameSession(long currentTimeStamp)
        {
            bool retVal = true;
            //check if the current timestamp is within 30 minutes since the last visit in current site 
            if (currentTimeStamp - sessionLastVisitTime <= MAX_SESSION_TIME_IN_SECONDS &&
                currentTimeStamp - sessionLastVisitTime >= 0)
            {
                sessionLastVisitTime = currentTimeStamp;
            }
            //check if the current timestamp is within current session
            else if (currentTimeStamp >= sessionStartTime &&
                     currentTimeStamp <= sessionLastVisitTime)
            {
                //nothing
            }
            //check if the current timestamp is within 30 minutes before current session start time
            else if (sessionStartTime - currentTimeStamp <= MAX_SESSION_TIME_IN_SECONDS &&
                     sessionStartTime - currentTimeStamp >= 0)
            {
                sessionStartTime = currentTimeStamp;
            }
            else
            {
                retVal = false;
            }

            return retVal;
        }

        public long SessionLength()
        {
            return sessionLastVisitTime - sessionStartTime;
        }
    }
}
