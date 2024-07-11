# fc-rabbitmq

This repository contains a simple RabbitMQ project developed based on the FullCycle RabbitMQ course. The project aims to test the functionality of different types of queues and exchanges, including direct, topic, and fanout exchanges. It includes examples of publishing and subscribing to messages using C# .NET 8 and Docker Compose for setup.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

## Setup and Running the Project

### 1. Clone the Repository

```bash
git clone https://github.com/sabrinahelena/fc-rabbitmq.git
cd fc-rabbitmq
```

### 2. Start RabbitMQ with Docker Compose
To start the RabbitMQ server, run the following command:
```bash
docker-compose up -d
```
This will start RabbitMQ and the management interface will be available at http://localhost:15672.

### 3. Access RabbitMQ Management Interface
Open your browser and go to http://localhost:15672. Log in with the following credentials:
- **Username**: sasa
- **Password**: sorvete123

### 4. Run the Project
To run the project and start publishing or subscribing to messages, use the following steps:
**Publishing messages:** Run the project and select the publish option:

```bash
dotnet run
```

Enter `publish` when prompted. This will send 5 messages to the sasa-queue.

**Verifying messages**: After publishing the messages, go to the RabbitMQ Management Interface and navigate to the Queues tab. Select sasa-queue to see the published messages.

**Consuming messages:** Run the project again and select the subscribe option:
```bash
dotnet run
```
Enter `subscribe` when prompted. This will consume the messages from the `sasa-queue` and display them in the console.

