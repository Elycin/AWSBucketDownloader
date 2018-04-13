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