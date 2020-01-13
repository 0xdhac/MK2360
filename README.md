# What is this
The MK2360 program is a tool that allows anyone running Windows to emulate an Xbox 360 Controller with their Keyboard and Mouse in any video game that supports controllers.

# Download
[Here is the download link to the most recent version.][Download]

# Gaining access
* To gain access to use the program, you need to purchase it. To do that, you should run the program and click **Purchase**. The current payment methods I accept are Bitcoin and Paypal. And the current price is $5 USD.

# How to set up
* Make sure you have a recent version of [.NET Framework].
* Run the SETUP.bat file. This will install the [Interception] and [ScpDriverInterface] drivers needed to make this program function. Restart your PC. If you wish to undo this and not use MK2360. Run the UNINSTALL.bat file.
* If you have a real controller plugged into your PC, either **don't touch it while your game is up**, or **unplug it**. A real controller can cause the program to not work.
* You need to set the joystick deadzone for the game you're on to 0.0 for the aiming to feel smooth. If you're using this on Fortnite, here is a (**DO THIS WHILE YOUR GAME IS CLOSED**) [tutorial][DeadzoneTutorial].

# Binds
* This may seem confusing but it will make sense to you, don't worry. The point of the program itself is to map your desired keyboard keys to send Xbox 360 controller inputs. So say for example you want to **Jump** with your **Spacebar** by sending the Xbox Controller **A** button. In the program itself, you want to bind your **Spacebar** to the **A** controller button, and then afterwards you'll want to make sure your A button is bound to Jump in-game. Here is a demonstration:

![](https://imgur.com/uCzToZO.gif)

* The program comes with a configuration for Fortnite, so you don't need to do any of this stuff unless you really wanted to. If you ever wonder what keyboard button presses what controller button. Just click the controller button inside MK2360 and it will show you on the left:

![](https://imgur.com/Af2WSgB.gif)

* Next up is your in-game settings. There are quite a few settings you need to change here. And I will show you all of them that I personally use.

***

Set your Controller Sensitivity X and Controller Sensitivity Y to a high number like mine. Keep your ADS/Scope sensitivity low, and building/edit mode sensitivity high.

![](https://i.imgur.com/qaLjtbz.png)

***

![](https://imgur.com/l9hvwKD.png)

***

![](https://imgur.com/ihTwxu3.png)

***

![](https://imgur.com/20bk1gT.png)

***

![](https://imgur.com/orWqMsg.png)

***

![](https://imgur.com/TbhW0ux.png)

***

These number should both read 0.0 if you followed the deadzone tutorial from earlier.

![](https://imgur.com/9jTnOrf.png)

# Using it

* Once you've gotten to this point, all you need to do is hit the blue Start button on the MK2360 program to enter **Controller mode**. It does not matter if you do this after or before starting the game, you can hit that button whenever you feel like it. If at any point you feel like you lost control and nothing is working (which shouldn't happen) you can simply press the **Killswitch** bind, which can be found on the left side of the program, to exit controller mode. If you don't know what buttons do what, you simply need to refer to the program and just click the button and it will tell you on the left as demonstrated earlier in the tutorial.

# Macros
Work in progress

[.NET Framework]: https://dotnet.microsoft.com/download/dotnet-framework/net472
[Interception]: https://github.com/oblitum/Interception
[ScpDriverInterface]: https://github.com/mogzol/ScpDriverInterface
[DeadzoneTutorial]: https://youtu.be/fJDWhtRR3t0
[Download]: https://www.dropbox.com/s/pl9sof2tkbi15lj/MK2360.zip?dl=1
