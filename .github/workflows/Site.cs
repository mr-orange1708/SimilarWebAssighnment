using System;
using System.Collections.Generic;
using System.Text;

namespace SimilarWebAssighnment
{
    class Site
    {
        List<long> sessionsLengths;
        bool sessionListIsSorted;
        public string SiteUrl { get; private set; }
        
        public Site(string siteUrl)
        {
            SiteUrl = siteUrl;
            sessionsLengths = new List<long>();
            sessionListIsSorted = false;
        }

        public void AddSession(long sessionLength)
        {
            sessionsLengths.Add(sessionLength);
            sessionListIsSorted = false;
        }

        public void SortSessionsListForMedianCalc()
        {
            if (!sessionListIsSorted)
            {
                sessionsLengths.Sort();
                sessionListIsSorted = true;
            }
        }

        public double GetMedianSessionLength()
        {
            int mid = sessionsLengths.Count / 2;

            SortSessionsListForMedianCalc();

            return (sessionsLengths.Count % 2 != 0 ? (double)sessionsLengths[mid] : 
                                                     ((double)sessionsLengths[mid] + (double)sessionsLengths[mid - 1]) / 2);
        }

        public int GetNumSession()
        {
            return sessionsLengths.Count;
        }
    }
}
