# SimpleAwsSpeedtest
A super simple AWS Speedtest ping utility. I wrote it because I needed to find the best AWS region for me.

The tool works by pinging the s3 endpoint for each region and showing the best results based on the app settings.

I probably won't make much, if any, changes or merge any pull requests, however I will try to add new regions and other things I have time too.

Coded in Visual Studio 2019 for .net Core 3.1

To make sure you have the updated regions list always use the one in master branch. The China regions and Osaka-Local region is not in the regions.json file but can be added manually

## Configuration
*appsettings.json* has 2 settings:
1. *pingType* can be *ICMP* for standard ping or *HTTPS* for timing a HTTPS request. (Default: *ICMP*)
2. *resultCount* used to set how many results to view, ordered by lowest ping (Default: *3*)

*regions.json* just contains an array of region subdomains and name
