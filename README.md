# ChatApp
ChatApp is a simple WPF-based chat application built using C# and .NET. The application consists of two parts: a Chat Server and a Chat Client. The Chat Server listens for incoming client connections and facilitates message broadcasting among connected clients. The Chat Client connects to the server, sends messages, and receives broadcasted messages from other connected clients.

## Features
- **Server:** Handles multiple client connections, broadcasts messages, and manages client connections.
- **Client:** Connects to the server, sends messages to the server, and receives messages from other connected clients.
- **Simple UI:** Easy-to-use interface for both the server and client, built using WPF.

## Getting Started

### Prerequisites
- **Visual Studio**: Ensure you have Visual Studio installed on your system.
- **.NET Framework**: This application targets the .NET Framework. Make sure the .NET Framework is installed on your machine.

### Setting Up the Server
1. Clone the repository and navigate to the ChatServer directory.
2. Open the ChatServer.sln solution file in Visual Studio.
3. Build and run the solution. The server window will open and start listening for client connections on localhost:5000.

### Setting Up the Client
1. Navigate to the ChatClient directory.
2. Open the ChatClient.sln solution file in Visual Studio.
3. Build and run the solution. The client window will open and automatically connect to the server.
4. Enter a message in the message box and click "Send" to send the message to the server.

### Usage
- Run the server first, followed by one or more clients.
- Each client can send messages to the server, which will be broadcasted to all other connected clients.
- Messages are displayed in the chat window, along with a prefix indicating whether the message was sent by "You" or a "Friend".

## Contributions
Contributions are welcome! If you find a bug or have a feature request, please open an issue. Feel free to fork the repository and submit a pull request.
