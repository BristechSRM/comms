if test "$OS" = "Windows_NT"
then
    build/output/Comms.exe
else 
    mono build/output/Comms.exe
fi

