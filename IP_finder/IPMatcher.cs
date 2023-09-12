using System;
using System.Collections.Generic;
using System.IO;

namespace IPMatchingApp
{
    public class IPMatcher : IIPMatcher
    {
        public List<MatchResult> Match(string filePath, string method)
        {
            List<MatchResult> results = new List<MatchResult>();

            // Read IP addresses from .txt file
            List<string> ipAddresses = ReadIPAddressesFromFile(filePath);

            foreach (string ipAddress in ipAddresses)
            {
                string country;

                // Match IP address based on the chosen method
                switch (method)
                {
                    case MatchingMethod.Random:
                        country = MatchRandomly();
                        break;
                    case MatchingMethod.Predefined:
                        country = MatchPredefined(ipAddress);
                        break;
                    default:
                        country = "Unknown";
                        break;
                }

                results.Add(new MatchResult { IPAddress = ipAddress, Country = country });
            }

            return results;
        }

        private List<string> ReadIPAddressesFromFile(string filePath)
        {
        
            List<string> ipAddresses = new List<string>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    ipAddresses.Add(line.Trim());
                }
            }

            return ipAddresses;
        }

        private string MatchRandomly()
        {

            string[] countries = { "USA", "UK", "Angola", "Rwanda", "Italy"};
            Random random = new Random();
            int randomIndex = random.Next(countries.Length);
            return countries[randomIndex];
        }

        private string MatchPredefined(string ipAddress)
        {
            
            string[] parts = ipAddress.Split('.');
            if (parts.Length < 4)
            {
                return "Unknown";
            }

            int firstOctet = int.Parse(parts[0]);

            if (firstOctet >= 0 && firstOctet <= 50)
            {
                return "UK";
            }
            else if (firstOctet >= 51 && firstOctet <= 100)
            {
                return "Italy";
            }
            else if (firstOctet >= 101 && firstOctet <= 150)
            {
                return "France";
            }
            else if (firstOctet >= 151 && firstOctet <= 200)
            {
                return "Germany";
            }
            else if (firstOctet >= 201 && firstOctet <= 254)
            {
                return "USA";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}


