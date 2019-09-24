# FlightGear-Simulator-Web-App
A web application that is used to display the position of the aircraft in real time.
Implemented using JavaScript

## Installing
like the installing at "FlightGear-Simulator-Interpreter".

## Preparing to lift off
a. First, you may need to adjust the time in the simulator from nighttime to daytime in order to see clearly. Click on the Environment tab in the toolbar shown below, and click on Time Settings. Adjust the time to your liking.
b. Next, in order to help you speed things up and bypass the take-off procedures, click on the Cessna C172P tab, and click on Autostart. This will start the engine and prepare the aircraft to lift off.
## Displays the displacement of the aircraft
Run the simulator and click on the 'Fly!' icon in the bottom left corner. Next, run the application. Your default internet browser will open with the address 'localhost:xxx', where 'xxx' is a number. There are four types of requestes you can perform which we will cover. 

### Single point
The requst will show the current position of the aircraft. To achieve this, type in the following request:
```
localhost:xxx/display/ip/port
```
The outcome will be:

![](map1.JPG)


### Route of point
You can choose to see the airplane progress track. You need type how many point in one secand you want to get.
To achieve this, type in the following request: 
```
localhost:xxx/display/ip/port/dealy
```
The outcome will be:

![](map2.JPG)

### Save the route
The next option is to save the point of the track that the airplane did at a file. you chose how many point in secand, for how long secand to save and the name of the file. If the file does not exist, it will create a new one. Otherwise, it will overwrite what exists and write the data within the file.
To achieve this, type in the following request: 
```
localhost:xxx/save/ip/port/delay/total_time/file_name
```

### display from the file 
after you save the data of the route, you can display it. you need to choose the name of the file and this will show the next point in the route every 'delay' seconds. In the end of all the piont you get a 'alert' massage.
To achieve this, type in the following request: 
```
localhost:xxx/file_name/delay
```


