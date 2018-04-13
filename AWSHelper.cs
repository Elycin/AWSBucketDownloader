using Amazon.Runtime;
using Amazon.S3;
using BucketDownloaderAWS;
using System;

namespace AWSBucketDownloader
{
    internal class AWSHelper
    {
        /// <summary>
        /// Converts string based AWS regions into their proper types.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private static Amazon.RegionEndpoint ParseRegion(string endpoint)
        {
            switch (endpoint)
            {
                case "us-east-1":
                    return Amazon.RegionEndpoint.USEast1;

                case "us-east-2":
                    return Amazon.RegionEndpoint.USEast2;

                case "us-west-1":
                    return Amazon.RegionEndpoint.USWest1;

                case "us-west-2":
                    return Amazon.RegionEndpoint.USWest2;

                case "ap-northeast-1":
                    return Amazon.RegionEndpoint.APNortheast1;

                case "ap-northeast-2":
                    return Amazon.RegionEndpoint.APNortheast2;

                case "ap-south-1":
                    return Amazon.RegionEndpoint.APSouth1;

                case "ap-southeast-1":
                    return Amazon.RegionEndpoint.APSoutheast1;

                case "ap-southeast-2":
                    return Amazon.RegionEndpoint.APSoutheast2;

                case "ca-central-1":
                    return Amazon.RegionEndpoint.CACentral1;

                case "cn-north-1":
                    return Amazon.RegionEndpoint.CNNorth1;

                case "cn-northwest-1":
                    return Amazon.RegionEndpoint.CNNorthWest1;

                case "eu-central-1":
                    return Amazon.RegionEndpoint.EUCentral1;

                case "eu-west-1":
                    return Amazon.RegionEndpoint.EUWest1;

                case "eu-west-2":
                    return Amazon.RegionEndpoint.EUWest2;

                case "eu-west-3":
                    return Amazon.RegionEndpoint.EUWest3;

                case "sa-east-1":
                    return Amazon.RegionEndpoint.SAEast1;

                default:
                    Console.WriteLine("Invalid AWS Region specified.");
                    Console.ReadLine();
                    Environment.Exit(1);
                    return null;
            }
        }

        /// <summary>
        /// Builds a BasicAWSCredentials instance with the proper variables for the bucket.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static BasicAWSCredentials BuildAWSCredentialHandler(Configuration configuration) => new BasicAWSCredentials(configuration.GetAWSAccessKey(), configuration.GetAWSSecretKey());

        /// <summary>
        /// Builds a AmazonS3Config instance with the proper variables for the region.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static AmazonS3Config BuildAWSConfiguration(Configuration configuration) => new AmazonS3Config { RegionEndpoint = ParseRegion(configuration.GetAWSRegion()) };
    }
}