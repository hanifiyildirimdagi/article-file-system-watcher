# File Processing with Channels and Queues in .NET

This repository contains a sample code implementation that demonstrates efficient file processing using channels and queues in .NET. The code showcases how to handle multiple files concurrently, process them asynchronously, and manage the flow of data using channels and queues.

[Read The Article](https://medium.com/@hanifi.yildirimdagi/efficient-file-processing-using-filesystemwatcher-in-net-e7f1c994e91d)

## Overview

In scenarios where multiple files arrive simultaneously, it becomes essential to process them efficiently without overwhelming system resources. This code example addresses this issue by leveraging channels and queues to handle file processing in a controlled and scalable manner.

## FileSystemWatcher Properties

The core of the file processing functionality is built upon the FileSystemWatcher class, which provides the ability to monitor a directory for file changes. Here are some key properties used in the code:

- **Path**: Specifies the directory to monitor for file events. It can be set to a specific directory path using the Path property of the FileSystemWatcher instance.
- **Filter**: Specifies the file type(s) to monitor. The code uses the Filter property of the FileSystemWatcher instance to specify the file extension(s) of interest. For example, *.txt will monitor text files only.
- **NotifyFilter**: Specifies the type of file events to monitor. The code sets the NotifyFilter property of the FileSystemWatcher instance to listen for file creation events (Created).

## Class Explanations
The code includes several classes to handle file processing using channels and queues. Here's a brief explanation of each class:

- Program: This is the entry point of the application. It initializes and starts the Listener and Queue instances.
- Listener: This class is responsible for capturing file events using the FileSystemWatcher. It subscribes to the Created event and pushes the event data (file paths) into the channel.
- Queue: This class acts as the consumer, retrieving file paths from the channel and processing them one by one. It performs the required file operations asynchronously, such as reading the file content, applying business logic, and handling any errors that may occur during processing.
- Worker: This class encapsulates the channel and provides the necessary methods for producing (writing) and consuming (reading) file events. It creates an instance of the BoundedChannel class, which allows a maximum of 100 items at a time. If more data is sent, the code will wait until the channel has available space.