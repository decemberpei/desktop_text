# desktop_text
show text on windows desktop
1. "TextOnWallpaper" vs project generates text content (currently host and ip, refresh upon network change) and write to “TextShow.txt”, 
    you may want to change the src to generate some other content you want;
2. "Bginfo64.exe" reads “TextShow.txt” content and put it onto desktop;
3. This utility auto starts when user login;
