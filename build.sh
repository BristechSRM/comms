if test "$OS" = "Windows_NT"
then
    packages/FAKE/tools/FAKE.exe build.fsx
else 
    mono packages/FAKE/tools/FAKE.exe build.fsx
fi
