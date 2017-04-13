# Developer Guide - ARCoM

> This Developer Guide is best viewed via GitHub (https://github.com/daredevil999/ARCoM)

* [Setting Up](#setting-up)
* [Design](#design)

## Setting Up

#### Pre-requisites
1. Download and install the latest version of **Unity** to edit or develop ARCoM.
2. Download and install the latest version of **Blender** to create 3D virtual animations.

#### Importing the project into **Unity**
1. Download and unzip the latest ZIP folder from the `releases` tab in the GitHub link given above.
2. Unzip the files to a directory of your choice.
3. Click `FYP` > `Assets` > `ARCoM.unity` file.
4. The **Unity** application should initialise and you may be able to edit the program code from there

#### Editing ARCoM Scripts
1. The scripts behind the application use C# and can be edited and compiled using **Unity** native editor and compiler.
2. You may find the scripts under `FYP\Assets\MyScripts` and edit them with any text editor of your choice.

#### Running and compiling in **Unity**
1. There are many helpful resources to aid you in this aspect online. Do look through them.

#### Creating Animations with **Blender**
1. 3D animations can be created with **Blender** with an existing CAD model.
2. You may refer to online resources on creating animations via **Blender**.
3. Upon completion of animation, export the file to `.fbx` format.
4. In your **Unity** window, import the `animation.fbx` file.
5. Unity will extract the package and store it in the `Assets` folder.
6. You may then make use of the Animation Controller in **Unity** to control the animation sequence and steps.
> <img src="/Graphics/AnimationController.JPG">

#### Exporting to Android APK
1. Upon completion, you may export directly to an Android APK for easy installation in Android mobile devices.
2. Do ensure that you have downloaded the latest Android SDK in your computer.
3. Also, ensure that you have downloaded the Unity extension for Android applciation development which can be found in the Unity developer website.
4. In **Unity**, click `File` > `Build Settings`.
5. Then, select the Android platform and a prompt will appear requesting for Android SDK file path link.
6. Lastly, you may build the application into an Android SDK.
