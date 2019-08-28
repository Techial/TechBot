@echo off
COLOR 02

call:TWITCH
echo                        1. Compile for Linux (Ubuntu, CentOS, Fedora and etc)
echo                        2.                Compile for Windows 10
echo                        3.         Compile for MacOS (10.12 and above)
echo.
SET /P M=Type 1, 2 or 3 then press ENTER:
IF %M%==1 GOTO LINUX
IF %M%==2 GOTO WIN10
IF %M%==3 GOTO MACOS

:TWITCH
cls
echo.
echo                                                         `:::::::::::::::                           
echo                                                         /h/-----------+y                           
echo                                                         sh:   `.  `.  :y                           
echo                                                         sh:   /y  /y  :y                           
echo                                                         sh:   /s  /y  :y                           
echo                                                         sh:          `oy                           
echo                                                         sh+--. `----+y/                            
echo                                                         shhhhs+hhhhy/                              
echo                                                         /++shhy+++:                                
echo                 `.......                  ............`    -+:`   `.......                         
echo                 -h+///shs:                ss///+h+///s+         `/yh+///ss                         
echo                 -h.   ohhhs+++++++++++++++ys+++oh.   os++/ :++++yhhh-   +y+++/-                    
echo                 -h.   ```:h.```s+```:h.```so```:h.   ```/hyo.`````.h-   `````-ss-                  
echo                 -h.      :h`   s+   -h`   s+   -h.      :y.       `h-          :h.                 
echo                 -h.   /ssyh`   s+   -h`   s+   -h.   +ssyy    ossssh-   /ss/   .h.                 
echo                 -h.   +yyhh`   s+   -y`   s+   -h.   oyyhy    yyyyhh-   +hh+   .h.                 
echo                 -h-      :h`              s+   -h.      :y`       `h-   +hh+   .h.                 
echo                  :y+`    :h`            :yh+   -hy+`    :hy/`     `h-   +hh+   .h.                 
echo                    :syyyyhhyyyyyyyyyyyyyhhhhyyyyhhhhyyyyhhhhhyyyyyyhyyyyhhhhyyyys`                 
echo                      .:ohhhhhhhhhhhhhhhhhhohhhhhyohhhhhhoyhhhhhhhsyhhhy+yhhhhs:`                   
echo                         `-::::..:::::::::. :::::- .:::::` ::::::- :::-  ::::`                      
echo.
echo                                        TwitchBot - LUA by Techial
echo --------------------------------------------------------------------------------------------------------
echo.
exit /B 0

:LINUX
call:TWITCH
echo                                       Starting the Linux compiler...
PING localhost -n 3 >NUL
call:TWITCH
dotnet build --runtime linux-x64
pause
exit

:WIN10
call:TWITCH
echo                                      Starting the Windows compiler...
PING localhost -n 3 >NUL
call:TWITCH
dotnet build --runtime win10-x64
pause
exit

:MACOS
call:TWITCH
echo                                   Starting the MacOS (10.12+) compiler...
PING localhost -n 3 >NUL
call:TWITCH
dotnet build --runtime osx-x64
pause
exit