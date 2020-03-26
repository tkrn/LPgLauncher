# LPg Launcher

A down and dirty project which fundamentally is a URI wrapper for executables that do no natively support the URI syntax. This is most common with older computer game executables. This project aims to create a better user experiance while at LAN parties. The idea is to enable web links to launch, for example, a Quake 2 client to a particular server.

## Example
   Say we want to create the following URI/URL to launch a game server: 

    q2://tastyspleen.net:27916 

We'll need the **LPgLauncher.ini** to reflect the **q2://** URI. LPgLauncher is expected to be in the following format:

    [URI]
    q2.exe=C:\Games\Quake2\Quake2.exe
    q2.parm=+connect

Additionally, you can have additional URIs in the same [URI] section in the LPgLauncher.ini file. Example:

    [URI]
    q2.exe=C:\Games\Quake2\Quake2.exe
    q2.parm=+connect
    q3.exe=C:\Games\Quake3\ioquake3.x86.exe
    q3.parm=+connect

Continuing in the Quake 2 example the following will be executed:

    C:\Games\Quake2\Quake2.exe +connect tastyspleen.net:27916 


## Deployment / Inno Setup Scripting

The best way this could be deployed in my opinion is through a setup program such as Inno Setup. There is a variety of tools to help write the .ini and add the right registery keys.

Here is an Inno Setup [Registry] section to define the **q2://** protocol:

    [Registry]
    Root: HKCR; Subkey: "q2"; ValueType: string; ValueData: "URL:Q2 Protocol";
    Root: HKCR; Subkey: "q2"; ValueType: string; ValueName: "URL Protocol"; ValueData: "";
    Root: HKCR; Subkey: "q2\DefaultIcon"; ValueType: string; ValueData: "LPgLauncher.exe,1";
    Root: HKCR; Subkey: "q2\shell\open\command"; ValueType: string; ValueData: "{app}\LPgLauncher.exe %1"
    Root: HKCR; Subkey: "q2"; Flags: uninsdeletekey

Inno Setup [INI] section to write LPgLauncher.ini with the correct paths:

    [INI]
    Filename: "{app}\LPgLauncher.ini"; Section: "URI"; Key: "q2.exe"; String: "{app}\quake2.exe"
    Filename: "{app}\LPgLauncher.ini"; Section: "URI"; Key: "q2.parms"; String: "+connect"

