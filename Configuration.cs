using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketDownloaderAWS
{
    class Configuration
    {
        /// <summary>
        /// The variable that contains the loaded configuration.
        /// </summary>
        private JObject loadedConfiguration;

        /// <summary>
        /// Constructor that handles the base logic of the configuration initialization and loading.
        /// </summary>
        public Configuration()
        {
            Console.WriteLine("Configuration class initialized");

            if (DoesConfigurationExist())
            {
                // Read the configuration into memory.
                ReadConfiguration();
            } else
            {
                // Create and write the new configuration.
                CreateConfiguration();
                WriteConfiguration();

                // Tell the user what they need to do
                Console.WriteLine();
                Console.WriteLine("Please edit the configuration and restart the application.");
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();

                // Graceful exit.
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Creates a new configuration and loads it into memory.
        /// </summary>
        public void CreateConfiguration()
        {
            // Create a template formt he bucket.
            JObject newBucketSubConfiguration = new JObject
            {
                { "Bucket Name", "YOUR_BUCKET_NAME" },
                { "Access Key", "YOUR_ACCESS_KEY" },
                { "Secret Key", "YOUR_SECRET_KEY" },
                { "Region", "us-east-2" }
            };

            // Create the base configuration that we will write to the filesystem.
            JObject newConfiguration = new JObject
            {
                { "AWS", newBucketSubConfiguration },
                { "Save Path", "C:/Users/Adminsitrator/Desktop/S3" },
            };

            // Assign to the class.
            loadedConfiguration = newConfiguration;

            Console.WriteLine("A new configuration template has been created.");
        }

        public void ReadConfiguration()
        {
            loadedConfiguration = JObject.Parse(File.ReadAllText(GetConfigurationAbsolutePath()));
            Console.WriteLine("Configuration has successfully been loaded from the disk.");
        }

        /// <summary>
        /// Writes the loaded configuration in memory to the disk.
        /// </summary>
        public void WriteConfiguration()
        {

            // Export to a string, but indented.
            String exportedJSON = loadedConfiguration.ToString(Newtonsoft.Json.Formatting.Indented);

            // Write to the filesystem
            File.WriteAllText(GetConfigurationAbsolutePath(), exportedJSON);

            // Write to console where it was saved.
            Console.WriteLine(String.Format("Configuration saved: {0}", GetConfigurationAbsolutePath()));
        }

        /// <summary>
        /// Checks to see if the configuration exists.
        /// </summary>
        /// <returns></returns>
        public bool DoesConfigurationExist() => File.Exists(GetConfigurationAbsolutePath());

        /// <summary>
        /// Get the working directory of the configuration.
        /// </summary>
        /// <returns></returns>
        public string GetConfigurationPath() => Directory.GetCurrentDirectory();

        /// <summary>
        /// Get the absolute path to the configuration.
        /// </summary>
        /// <returns></returns>
        public string GetConfigurationAbsolutePath() => Path.Combine(GetConfigurationPath(), "AWSBucketDownloader.json");

        /// <summary>
        /// Get the bucket name from the configuration.
        /// </summary>
        /// <returns></returns>
        public string GetAWSS3BucketName() => loadedConfiguration["AWS"]["Bucket Name"].ToString();

        /// <summary>
        /// Get the access key from the configuration.
        /// </summary>
        /// <returns></returns>
        public string GetAWSAccessKey() => loadedConfiguration["AWS"]["Access Key"].ToString();

        /// <summary>
        /// Get the secret key from the configuration.
        /// </summary>
        /// <returns></returns>
        public string GetAWSSecretKey() => loadedConfiguration["AWS"]["Secret Key"].ToString();

        /// <summary>
        /// Return the region
        /// </summary>
        /// <returns></returns>
        public string GetAWSRegion() => loadedConfiguration["AWS"]["Region"].ToString();

        /// <summary>
        /// Return the save location.
        /// </summary>
        /// <returns></returns>
        public string GetSaveLocation() => loadedConfiguration["Save Path"].ToString();

        /// <summary>
        /// Return the save location with the bucket name.
        /// </summary>
        /// <returns></returns>
        public string GetSaveLocationWithBucket() => Path.Combine(GetSaveLocation(), GetAWSS3BucketName());
    }
}
