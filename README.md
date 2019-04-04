# SimpleAwsSpeedtest
A super simple AWS Speedtest ping utility. I wrote it because I needed to find the best AWS region for me.

I probably won't make much, if any, changes or merge any pull requests, however I will try to add new regions.

# Configuration
*appsettings.json* has 2 settings:
1. *pingType* can be *ICMP* for standard ping or *HTTPS* for timing a HTTPS request. (Default: *ICMP*)
2. *resultCount* used to set how many results to view (Default: *3*)

*regions.json* just contains an array of region subdomains and name
