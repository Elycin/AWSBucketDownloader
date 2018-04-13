# AWSBucketDownloader
Tool to download everything in an AWS S3 Bucket

## Purpose
This tool was created to assist in orchestration of downloading files to server in a personal project, in a timed basis.

## How it works and how to use
 - The first initial run will create a configuration file in the current working directory.
 - Edit the `AWSBucketDownloader.json` it creates with your `AWS_ACCESS_KEY`, `AWS_SECRET_KEY`, `AWS_BUCKET_NAME` and `AWS_REGION`.
 - Specify the root directory where it should download the files to
   - Note: it will create a subdirectory with the bucket name.
 - Run it again to download all the files in the bucket.
   - Optinally, you can add it to Task Scheduler to haved scheduled updates
