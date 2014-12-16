lib\ffmpeg -r 2 -i "E:\Projects2013\WiiUMediaDumpHook\WiiUMediaDumpHook\GUI\bin\Debug\FrameDump\%%03d.jpg" -i "E:\Projects2013\WiiUMediaDumpHook\WiiUMediaDumpHook\GUI\bin\Debug\lib\sample.mp3" -c:v libx264 -c:a aac -strict experimental -b:a 192k -shortest "E:\Projects2013\WiiUMediaDumpHook\WiiUMediaDumpHook\GUI\bin\Debug\lib\sample.mp4"

pause

lib\ffmpeg -r 24 -i FrameDump\%%05d.jpg -i "F:\Wii U Media\Media\Music\Alone Tonight (ronski speed remix) - Above and Beyond (Trance Channel - D I G I T A L L Y - I M P O R T E D - we can't define it!).mp3" -c:v libx264 -c:a aac -strict experimental -b:a 192k -shortest "F:\Wii U Media\Media\Music\Alone Tonight (ronski speed remix) - Above and Beyond (Trance Channel - D I G I T A L L Y - I M P O R T E D - we can't define it!).mp4"

Pause

lib\ffmpeg -r 24 -i FrameDump\%%05d.jpg -s 1920x1080 -c:v libx264 test1.mp4