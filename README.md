# RC Camera Car Android Application

This is the repository for the Android client application of the RC Camera Car
system. This repository contains the C# code and XAML markup code for the client
application that will communicate with the central server and car of the system.
The application allows users to register an account, login, get their list of
cars, register a new car, view their cars video stream, and control the movement
of the car with any controller connected over Bluetooth or a USB cable.

## Build Setup
### Dependencies:
* Visual Studio
  * Xamarin.Forms

### Steps:
1. Install Visual Studio  
  a) Download the installer from
     [here](https://visualstudio.microsoft.com/downloads/)  
  b) Follow the instructions
     [here](https://docs.microsoft.com/en-us/xamarin/get-started/installation/?pivots=windows)
     to install Xamarin with Visual Studio.  
2. Once Visual Studio is running, use it to open the `CarCamApp.sln` file
   located in the top-level directory of this repository.  
3. Build the application.
  a) Either connect an Android device (with **Develop options** and
  **USB debugging** enabled) to the computer running Visual Studio with a USB
  cable or run an emulator.
  b) Click the green "run" button to build the app and launch it on the
  connected Android device.

## Code Structure
* `CarCamApp.sln`: solution file for loading the project into Visual Studio
* `CarCamApp.Android/MainActivity.cs`: contains the code for the main Activity
of the app which initializes the graphical user interface (GUI) and also handles
controller input
* `CarCampApp/`:
  * `Views/`: contains the code (`*.cs`) and markup (`*.xaml`) for all the custom
  Views that make up the GUI
  * `Models/`: contains the code that represents the different components of the
  systems (`Car.cs`, `User.cs`, etc.), as well as utility code (`Constants.cs`,
  `SocketClient.cs`, etc.)
  * `Messages/`: contains the code that represents the relevant message types for
  the app from the system's communication protocol