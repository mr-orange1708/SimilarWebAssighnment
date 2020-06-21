using System;
using System.Collections.Generic;
using System.Text;

namespace SimilarWebAssighnment
{
    class SessionMgr
    {
        public List<SessionDuration> SessionsList { get; private set; }

        public SessionMgr()
        {
            SessionsList = new List<SessionDuration>();
        }

        public void ProcessTimeStamp(long timeStamp)
        {
            foreach (var session in SessionsList)
            {
                if (session.IsTheSameSession(timeStamp))
                {
                    return;
                }
            }
            //this is a new session
            SessionsList.Add(new SessionDuration(timeStamp));
        }
    }
}
