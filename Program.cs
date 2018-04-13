using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using AWSBucketDownloader;
using System;
using System.IO;
using System.Threading;

namespace BucketDownloaderAWS
{
    internal class Program
    {
        private static Configuration configuration;

        private static void Main(string[] args)
        {
            // Initialize a new configuration class
            configuration = new Configuration();

            // Download the object.
            DownloadAllObjects();

            Console.WriteLine(new String('=', Console.BufferWidth - 1));
            Console.WriteLine("\nClosing gracefully in 10 seconds.");
            Thread.Sleep(10000);
            Environment.Exit(0);
        }

        public static void DownloadAllObjects()
        {
            using (IAmazonS3 AmazonNetworkClient = new AmazonS3Client(AWSHelper.BuildAWSCredentialHandler(configuration), AWSHelper.BuildAWSConfiguration(configuration)))
            {
                // Compile a list of objects.
                ListObjectsRequest request = new ListObjectsRequest
                {
                    BucketName = configuration.GetAWSS3BucketName()
                };
                ListObjectsResponse response = AmazonNetworkClient.ListObjects(request);

                // Make sure the save location exists
                if (Directory.Exists(configuration.GetSaveLocation())) Directory.CreateDirectory(configuration.GetSaveLocation());
                if (Directory.Exists(configuration.GetSaveLocationWithBucket())) Directory.CreateDirectory(configuration.GetSaveLocationWithBucket());

                // Add a new line for readability in the console.
                Console.WriteLine(new String('=', Console.BufferWidth - 1));

                // Iterate over each object.
                foreach (S3Object currentObject in response.S3Objects)
                {
                    // Get the base name of the file
                    string objectBaseName;
                    if (currentObject.Key.Contains("/"))
                    {
                        string[] split = currentObject.Key.Split('/');
                        objectBaseName = split[split.Length - 1];
                    } else
                    {
                        objectBaseName = currentObject.Key;
                    }
                    string combinedPath = Path.Combine(configuration.GetSaveLocationWithBucket(), currentObject.Key);

                    // Check to see if it's a directory.
                    if (!currentObject.Key.EndsWith("/"))
                    {
                        Console.WriteLine("Downloading object: " + objectBaseName);
                        Console.WriteLine("\tBucket Path: /" + currentObject.Key);
                        Console.WriteLine("\tFile Size: " + currentObject.Size / Math.Pow(1024, 1));
                        Console.WriteLine("\tSave Location: " + combinedPath);
                        // Get information about the object
                        GetObjectResponse objectResponse = AmazonNetworkClient.GetObject(
                            new GetObjectRequest
                            {
                                BucketName = configuration.GetAWSS3BucketName(),
                                Key = currentObject.Key
                            }
                        );

                        // Write to location
                        objectResponse.WriteResponseStreamToFile(combinedPath);
                        Console.WriteLine(String.Format("{0} has been downloaded successfully.\n", currentObject.Key));
                    }
                }
            }
        }
    }
}