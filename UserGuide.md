# User Guide - ARCoM

> This User Guide is best viewed via GitHub (https://github.com/daredevil999/ARCoM)

* [Quick Start from GitHub](#quick-start-from-GitHub)
* [Quick Start from Folder Download](#quick-start-from-folder-download)
* [Set Up](#set-up)
* [Features](#features)

## Quick Start From Github

0. Ensure that you have Anrdoid Lollipop (Version No. 5.0) or later installed on your Android Smartphone/ Tablet.
	> This app will not work with earleir versions of Android OS
1. Download the latest `ARCoM.apk` from the `releases` tab.
2. Transfer the `ARCoM.apk` to the Android device via USB, email or bluetooth.
3. Run the `ARCoM.apk` installation on your Android device.
4. Upon completion of installation, double click on the logo of ARCoM to initialise the application. Refer to [Set Up](#set-up) and [Features](#features) for a more detailed walkthrough of the app.

## Quick Start From Folder Download

0. Ensure that you have Anrdoid Lollipop (Version No. 5.0) or later installed on your Android Smartphone/ Tablet.
	> This app will not work with earleir versions of Android OS
1. Copy the latest `ARCoM.apk` under the `Build` folder in this file path `Programs\FYP\Build`.
2. Transfer the `ARCoM.apk` to the Android device via USB, email or bluetooth.
3. Run the `ARCoM.apk` installation on your Android device.
4. Upon completion of installation, double click on the logo of ARCoM to initialise the application. Refer to [Set Up](#set-up) and [Features](#features) for a more detailed walkthrough of the app.

## Set Up
Please follow the following set up instructions for the image tracker and the gearbox engine.

#### Image Tracker
- For the Augmented Reality features to accurately overlay onto physical models, an image tracker is used. 
	> <img src="/Graphics/Tracker.jpg" width="250">
- You may print out the image file with dimensions 12cm by 12cm.

#### Retired Gearbox Engine
- The retired gearbox engine used in ARCoM was obtained from the Augmented Reality Laboratory in NUS.
	> <img src="/Graphics/PhysicalGearbox.JPG" width="250">

#### Set Up
- Place the image tracker centred and in front of the physical gearbox as shown in the image below.
	> <img src="/Graphics/GearboxSetup.JPG" width="250">

## Features

#### Homepage 
- Upon detection of the unique image tracker, users will be brought to the Homepage.
- <img src="/Graphics/Homepage.JPG">
- On the left panel, there are 4 buttons in yellow which redirects users to the pages with the respective information: `Vibration Analysis` , `Oil Analysis` , `Operating Conditions` and `Maintenance Guide`.

#### Vibration Analysis Page
- Users can view the Vibration Analysis condition indicators tracked for the gearbox.
- <img src="/Graphics/VibrationAnalysis.JPG">

#### Oil Analysis Page
- Users can view the Oil Analysis condition indicators tracked for the gearbox.
- <img src="/Graphics/OilAnalysis.JPG">

#### Operating Conditions Page
- Users can view the operating conditions of the gearbox.
- <img src="/Graphics/OperatingConditions.JPG">

#### Maintenance Guide
- Users can view the maintenance steps for bearing and gear replacement for the gearbox.
- Screenshots of Gear Replacement Steps
> <img src="/Graphics/GearMaintenanceStep1.JPG">
> <img src="/Graphics/GearMaintenanceStep4.JPG">

#### Failure Demo
- On the right panel of the `Homepage`, there are 4 buttons each simulating sensor input in the event of the failure scenario.
- <img src="/Graphics/Homepage.JPG">
- You may select any of the demo scenarios to view the failure prediction and visualisation capabilities of ARCoM.
- Notice that the predicted failure component is highlighted in red and augmented onto the physical gearbox.
- Screenshots of ARCoM under Gear Wear failure simulation
> <img src="/Graphics/GearWearScenario.JPG">
> <img src="/Graphics/GearWearStatus.JPG">




