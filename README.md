RUN:
The .exe file is located in the bin directory. From the project's home directory, just run .\run.cmd or .\bin\Puzzle.exe. Do not run Puzzle.exe from the bin directory itself.
The .exe extension of the executable has been changed to .notexe -- just change it to .exe.

NOTES:
This is an extension of the sliding puzzle project that allows users to add and change the puzzle theme. Click New Game to start the shuffling process. Click to move squares (in the same row or column as the blank tile). A message pops up when the puzzle is solved, and the missing piece is filled in. Note that in this version, a random puzzle piece is always removed, not always the 16th piece as originally intended. This allows for a more challenging (near impossible) experience when uploading custom images.

There are already a couple of themes available. Click Change Theme to select from the current themes located in the .\res\themes directory. Each theme as actually a directory of image fragments named by index. The name of the currently selected directory is saved to the current_theme file in .\res. This is updated each time a theme is selected from this menu.

The user can also add their own themes by clicking Add Theme. This leads to a similar menu that allows selection of 3x3, 4x4, 5x5, and 6x6 puzzles. The user then types the theme name below and clicks OK to be prompted to select an image. This name will determine the name of the folder in which the split images will be located. The images are then split based on the dimensions provided by the user.

DEFECTS:
There are some known issues that I will not have time to fix. Avoid uploading small files or choosing large dimensions like 6x6 to split files if the resulting fragments will be too small. The puzzle gameplay doesn't seem to properly handle pieces of around <50 pixels in either dimension. Also, avoid adding a theme with a name that matches a currently existing theme. The program is supposed to delete the existing theme and replace it with the new one, but an exception is thrown, saying the files to be deleted are currently in use. You can always just delete the theme folder that you want to be replaced from the file system. The program WILL handle the case of the current_theme file containing a theme name that no longer exists. Finally, the layouts themselves are rough around the edges.
