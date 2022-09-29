# MineTowerDefense
A project where 2 pro gamers make a tower defense



## Prorocky's Contribution

### WaveInfo Tool

#### What it is

I wrote a python script to allow for easy creation of waves via a GUI. Once finished, json files containing data on the rounds will be generated. You can then open the json files with the script by dragging/dropping onto the script and it will generate encoded txt files containing the same data. These txt files of encoded hex will be used by the Round Manager script in our game to control the spawning of enemies. 

- The **Enemy Type** and **Count** fields are self-explanatory

- The **Spacing Delay** field is how many seconds apart each enemy will spawn within their clusters. 

- The **Process Delay** field indicated how long until the next cluster will be processed; this will allow for multiple clusters to be processed within the same frame.

#### Setup

Download the python script. It is recommended to move the tool onto your Desktop before using, though it does not *have* to be on the Desktop. 

#### How to Use

- Double Click on the script to run it and open the round creation application. 
- Enter all the information, remebmber to select round number as well. 
- Pressing the **Add Info** button will add the information to the round data 
  - Change the values and add new waves/clusters of enemies again by pressing **Add Info** again
- Once finished, press Generate Files
  - Note that changing the **Round Number** only has an effect on **Generate Files**
  - If a Rounds folder did not exist previously, one will be made and a json file will be generated containing all the info entered
- After reading the json to be sure there are no errors, potentially editing issues, drag and drop the json file onto the script
- a txt document will be created in the Hex/ folder which is ready to be used by the program.
  - Copy the folder or the contents into the Hex folder in the repo (One directory above the Assets/ folder)
  - If the txt needs to be edited for balancing you can either edit it directly (good luck) or you can turn it back into a json by dragging it back on the script. The json file that is now in the JSON/ folder will have the contents of the updated txt file 

