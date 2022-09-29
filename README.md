# MineTowerDefense
A project where 2 pro gamers make a tower defense



## Prorocky's Contribution

### WaveInfo Tool

#### What it is

I wrote a python script to allow for easy creation of waves via a GUI. Once finished, json files containing data on the rounds will be generated. You can then open the json files with the script by dragging/dropping onto the script and it will generate encoded txt files containing the same data. These txt files of encoded hex will be used by the Round Manager script in our game to control the spawning of enemies. The **Enemy Type** and **Count** fields are self-explanatory, the **Spacing Delay** field is how many seconds apart each enemy will spawn within their clusters. The **Process Delay** field indicated how long until the next cluster will be processed; this will allow for multiple clusters to be processed within the same frame.

#### Setup

Download the python script. It is recommended to move the tool onto your Desktop before using, though it does not *have* to be on the Desktop. Double Click on the script to run it and open the round creation application. From here, enter all the information, remebmber to select round number as well. Pressing the **Add Info** button will add the information that has been entered to the round data where you can then change the values and add new waves/clusters of enemies.
