# MC_SVReticleScaling
  
Backup your save before using any mods.  
  
Uninstall any mods and attempt to replicate issues before reporting any suspected base game bugs on official channels.  

Function
====
Scales targetting reticle provided by battle computers.  
  
Install
=======
1. Install BepInEx - https://docs.bepinex.dev/articles/user_guide/installation/index.html Stable version 5.4.21 x86.  
2. Run the game at least once to initialise BepInEx and quit.  
3. Download latest mod release.  
4. Place MC_SVReticleScale.dll in .\SteamLibrary\steamapps\common\Star Valor\BepInEx\plugins\  
  
Use / Configuration
=====
After first run, a new file will be created in .\Star Valor\BepInEx\config\ called mc.starvalor.scalingreticle.cfg to change key bindings and operating mode.  

- Fixed Mode: Enabled => fixed values are used.  Disabled => ship class x by class value is used.  
- Skip Shuttle: Enabled => skips shuttle class when fixed mode is disabled (by class scaling starts at yachts).  Disabled => scaling starts at shuttles.  No effect when fixed mode is enabled.
- By class: Ammount to scale per ship class when fixed mode is disabled.
- Shuttle, Yacht, Corvette...Dread: Ammount to scale for named ship class when fixed mode is enabled.
  
