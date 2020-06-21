using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Linq;

namespace SimilarWebAssighnment
{
    class Sessionizing
    {
        Dictionary<string, Visitor> visitorsDict;
        Dictionary<string, Site> sitesDict;

        public Sessionizing()
        {
            visitorsDict = new Dictionary<string, Visitor>();
            sitesDict = new Dictionary<string, Site>();
        }

        public void LoadInputFilesByDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException();
            }

            var inputFiles = Directory.EnumerateFiles(directory, "*.csv");

            if (inputFiles == null || !inputFiles.Any())
            {
                throw new FileNotFoundException();
            }

            foreach (var currentFilePath in inputFiles)
            {
                ReadFile(currentFilePath);
            }
        }

        public void LoadInputFileByFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            ReadFile(filePath);

        }

        //add all visitors sessions to the site dictionary.
        //we should do it now after we finished loading all the files for session length calculation
        public void ManageSitesAfterInputReading()
        {
            foreach (var visitor in visitorsDict.Values)
            {
                foreach (var siteUrl in visitor.SessionsInVisitedSites.Keys)
                {
                    if (!sitesDict.ContainsKey(siteUrl))
                    {
                        sitesDict[siteUrl] = new Site(siteUrl);
                    }

                    foreach (var session in visitor.SessionsInVisitedSites[siteUrl].SessionsList)
                    {
                        sitesDict[siteUrl].AddSession(session.SessionLength());
                    }
                }
            }

            foreach (var site in sitesDict.Values)
            {
                site.SortSessionsListForMedianCalc();
            }
        }

        //return -1 in case of missing input URL.
        public int NumSession(string siteUrl)
        {
            if (sitesDict.ContainsKey(siteUrl))
            {
                return sitesDict[siteUrl].GetNumSession();
            }

            return -1;
        }

        //return -1 in case of missing input URL.
        public double MedianSessionLength(string siteUrl)
        {
            if (sitesDict.ContainsKey(siteUrl))
            {
                return sitesDict[siteUrl].GetMedianSessionLength();
            }

            return -1;
        }

        //return -1 in case of missing input visitir id.
        public int NumUniqueVisitedSites(string visitorId)
        {
            if (visitorsDict.ContainsKey(visitorId))
            {
                return visitorsDict[visitorId].GetNumUniqueVisitedSites();
            }

            return -1;
        }

        void ReadFile(string filePath)
        {
            var lines = File.ReadLines(filePath);
            string visitorId;
            string siteUrl;
            string pageViewUrl;
            long timeStamp;

            foreach (var line in lines)
            {
                var currentRecordInfo = line.Split(',');
                visitorId = currentRecordInfo[0];
                siteUrl = currentRecordInfo[1];
                pageViewUrl = currentRecordInfo[2];
                timeStamp = long.Parse(currentRecordInfo[3]);

                if (!visitorsDict.ContainsKey(visitorId))
                {
                    visitorsDict[visitorId] = new Visitor(visitorId);
                }

                visitorsDict[visitorId].AddPageView(siteUrl, timeStamp);
            }
        }
    }
}
