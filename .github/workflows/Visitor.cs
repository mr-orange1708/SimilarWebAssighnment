using System;
using System.Collections.Generic;
using System.Text;

namespace SimilarWebAssighnment
{    class Visitor
    {                
        string id;
        public Dictionary<string, SessionMgr> SessionsInVisitedSites { get; private set; }

        public Visitor(string iId)
        {
            id = iId;
            SessionsInVisitedSites = new Dictionary<string, SessionMgr>();
        }

        //return true in case of a new session for existing site. false otherwise.
        public void AddPageView(string siteUrl, long timeStamp)
        {
            if (!SessionsInVisitedSites.ContainsKey(siteUrl))
            {
                SessionsInVisitedSites[siteUrl] = new SessionMgr();
            }

            SessionsInVisitedSites[siteUrl].ProcessTimeStamp(timeStamp);
        }

        public int GetNumUniqueVisitedSites()
        {
            return SessionsInVisitedSites.Count;
        }
    }
}
