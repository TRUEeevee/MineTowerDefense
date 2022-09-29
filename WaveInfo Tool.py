import os
from subprocess import STARTF_USESHOWWINDOW
import sys
import json
import math
import tkinter as tk
from tkinter import ttk
from tkinter import messagebox


ENEMY_NAME_TO_HEX = {
    'None': 0x00,
    'Creeper': 0x01,
    'Zombie' : 0x02,
    'Skeleton': 0x03
}

ENEMY_HEX_TO_NAME = {value: key for key, value in ENEMY_NAME_TO_HEX.items()}


#------------------------------------------------------------#
# How to write to JSON file
#------------------------------------------------------------#

# ironically the top of the hierarchy is an array i.e. an array of dictionaries
# MasterDictionary = []

# Entry = {
#     "EnemyType": "Creeper",
#     "Count": 22,
#     "SpacingDelay": 0.1,
#     "ProcessNext": 0.5
# }

# MasterDictionary.append(Entry)

# Entry = {
#     "EnemyType": "Skeleton",
#     "Count": 2,
#     "SpacingDelay": 0.1,
#     "ProcessNext": 0.55
# }

# MasterDictionary.append(Entry)

# json_obj = json.dumps(MasterDictionary, indent=4)

# with open("test_file.json", "w") as outfile:
#     outfile.write(json_obj)

#------------------------------------------------------------
# How to read from JSON file
#------------------------------------------------------------

# infile = open('test_file.json')

# data = json.load(infile)

# read json file and turn it into hex txt file
# NOTE: JSON_file arg can be either full path or just Round#.json
def encode(JSON_file):
    # change directory to JSON folder so if only the file name is sent it can open the file appropriately
    os.chdir(os.getcwd() + '\Rounds\JSON')

    # Read data from JSON file and convert it into hex equivalent
    out_str = ''
    with open(JSON_file, 'r') as infile:
        data = json.load(infile)
        for wave_info in data:
            temp_str = ''
            temp_str += format(ENEMY_NAME_TO_HEX[wave_info["EnemyType"]], '02X')
            temp_str += format(int(wave_info["Count"], 16), '02X')
            temp_str += format(round(float(wave_info["SpacingDelay"]) / 0.1 - 1), '01X')
            temp_str += format(round(float(wave_info["ProcessDelay"]) / 0.125), '01X')
            out_str += temp_str
    
    # remove .json from end of file name
    file_name = str(JSON_file)[:-5]

    # if full path is sent, use partition to grab file name, or if no partition is found just use full string aka file name
    file_name = file_name.partition('JSON\\')[2] or file_name.partition('JSON\\')[0]

    # Find Hex folder path
    outfile_path = os.getcwd().partition('\JSON')[0] + '\Hex\\'

    # If Hex folder doesn't exist, make the Hex folder
    if not os.path.exists(outfile_path):
        os.mkdir(outfile_path)

    # add .txt to file name
    outfile_name = file_name + '.txt'

    # path will now include exact path of where file should be made
    outfile_path += outfile_name

    # write to file
    with open(outfile_path, 'w') as outfile:
        outfile.write(out_str)

    # display confirmation that file has been encoded
    messagebox.showinfo(title="JSON Encoder", message="%s was successfully encoded from %s" % (outfile_name, file_name + '.json'))

    # close files
    infile.close()
    outfile.close()

    # change directory back to where it was before so iteration in folder works
    os.chdir(os.getcwd().partition('Rounds\JSON')[0])

def decode(txt_document):
    os.chdir(os.getcwd() + '\Rounds\Hex')
    Master_Dictionary = []
    with open(txt_document, 'r') as infile:
        read_str = infile.read(6)
        while read_str != "":
            temp_dict = {}
            temp_dict["EnemyType"] = ENEMY_HEX_TO_NAME[int(read_str[0:2], 16)]
            temp_dict["Count"] = str(int(read_str[2:4], 16))
            temp_dict["SpacingDelay"] = str(round((int(read_str[4], 16) + 1 ) * 0.1, 1))
            temp_dict["ProcessDelay"] = str(round(int(read_str[5], 16) * 0.125, 3))
            Master_Dictionary.append(temp_dict)
            read_str = infile.read(6)

    json_obj = json.dumps(Master_Dictionary, indent=4)

    file_name = str(txt_document)[:-4]
    file_name = file_name.partition('Hex\\')[2] or file_name.partition('Hex\\')[0]
    outfile_path = os.getcwd().partition('\Hex')[0] + '\JSON\\'
    if not os.path.exists(outfile_path):
        os.mkdir(outfile_path)
    outfile_name = file_name + '.json'
    outfile_path += outfile_name

    with open(outfile_path, 'w') as outfile:
        outfile.write(json_obj)
    messagebox.showinfo(title="Hex Decoder", message="%s was successfully decoded from %s" % (outfile_name, file_name + '.txt'))
    infile.close()
    outfile.close()
    os.chdir(os.getcwd().partition('Rounds\Hex')[0])

# if script is opened via a file, determine if it's JSON-> txt or txt->JSON
if (len(sys.argv) > 1):
    # if file is json, immediately encode it into a hex txt file and close program
    for arg in sys.argv:
        # print(arg)
        match arg[-3:]:
            case 'txt':
                decode(arg)
            case 'son':
                encode(arg)
            case 'SON':
                for file_ in os.listdir(arg):
                    encode(file_)
            case 'Hex':
                for file_ in os.listdir(arg):
                    decode(file_)
            case _:
                continue
    exit()


Dictionary_Wrapper = []

def add_info():
    if enemy_type_combo.get() == "":
        messagebox.showwarning(title="Invalid Entry", message="Pick an enemy type retard")
    else:
        d = {}
        d["EnemyType"] = enemy_type_combo.get()
        d["Count"] = enemy_count_spin.get()
        d["SpacingDelay"] = spacing_delay_spin.get()
        d["ProcessDelay"] = process_delay_spin.get()
        Dictionary_Wrapper.append(d)

def generate_files():
    if not os.path.exists("Rounds"):
        os.mkdir("Rounds")
        os.mkdir("Rounds/Hex")
        os.mkdir("Rounds/JSON")

    json_obj = json.dumps(Dictionary_Wrapper, indent=4)
    round_num = int(round_number_spin.get())

    with open("Rounds/JSON/Round%d.json"%round_num, "w") as outfile:
        outfile.write(json_obj)
    
    messagebox.showinfo(title="JSON Creator", message="File %s successfully created"%outfile.name.partition("JSON/")[2])
    outfile.close()

    Dictionary_Wrapper.clear()


window = tk.Tk()
window.title("WaveInfo Auxiliary Tool")

frame = tk.Frame(window)
frame.pack()

enemy_type_count = tk.LabelFrame(frame, text="Enemy Info")
enemy_type_count.grid(row=0, column=0, padx=20, pady=10)

enemy_type = tk.Label(enemy_type_count, text="Enemy Type")
enemy_type.grid(row=0, column=0)

enemy_count = tk.Label(enemy_type_count, text="Number of Enemies")
enemy_count.grid(row=0, column=1)

enemy_type_combo = ttk.Combobox(enemy_type_count, values=["None", "Creeper", "Skeleton", "Zombie"])
enemy_count_spin = tk.Spinbox(enemy_type_count, from_=0, to=255)

enemy_type_combo.grid(row=1, column=0, padx=10)
enemy_count_spin.grid(row=1, column=1, padx=10)

for widget in enemy_type_count.winfo_children():
    widget.grid_configure(padx=10, pady=5)


delay_group = tk.LabelFrame(frame, text="Delay Times")
delay_group.grid(row=1, column=0, sticky="news", padx=20, pady=50)

spacing_delay = tk.Label(delay_group, text="Spacing Delay")
spacing_delay.grid(row=0, column=0)

process_delay = tk.Label(delay_group, text="Process Delay")
process_delay.grid(row=0, column=1)

spacing_delay_spin = tk.Spinbox(delay_group, from_=0, to=1.6, values=tuple(round(x*0.1, 2) for x in range(1, 17)))
process_delay_spin = tk.Spinbox(delay_group, from_=0, to=1.6, values=tuple(round(x*0.125, 3) for x in range(0, 16)))

spacing_delay_spin.grid(row=1, column=0)
process_delay_spin.grid(row=1, column=1)

for widget in delay_group.winfo_children():
    widget.grid_configure(padx=10, pady=5)

button_group = tk.LabelFrame(frame)
button_group.grid(row=2, column=0, sticky="news", padx=20, pady=10)

enter_button = tk.Button(button_group, text="Enter Info", command=add_info)
enter_button.grid(row=0, column=0, sticky="news")

round_number_spin = tk.Spinbox(button_group, from_=1, to=100, width=10)
round_number_spin.grid(row=0, column=1, sticky="news")

generate_file_button = tk.Button(button_group, text="Generate files", command=generate_files)
generate_file_button.grid(row=0, column=2, sticky="news")

button_group.columnconfigure(0, weight=1)
button_group.columnconfigure(1, weight=1)


for widget in button_group.winfo_children():
    widget.grid_configure(padx=10, pady=5)

window.mainloop()