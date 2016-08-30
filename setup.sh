if [ ! -d ".nuget" ]; then  
    echo "Creating .nuget directory"
    mkdir .nuget
else
    echo "Nuget directory found"
fi

if [ ! -f ".nuget/nuget.exe" ]; then
    echo "Fetching latest nuget executable"
    cd .nuget
    if test "$OS" = "Windows_NT"
    then
        echo "Note: On windows you need to have curl installed and in your path for this fetch to work, or just download the nuget.exe from"
        echo "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
        echo "And place under the .nuget folder"        
    fi
    curl -O https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
    cd ..
else
    echo "Nuget executable found"
fi

if [ ! -f "packages/FAKE/tools/FAKE.exe" ]; then
    echo "Installing FAKE package"
    
    if test "$OS" = "Windows_NT"
    then
        .nuget/nuget.exe install "FAKE" -OutputDirectory packages/ -ExcludeVersion
    else 
        mono .nuget/nuget.exe install "FAKE" -OutputDirectory packages/ -ExcludeVersion
    fi    
else
    echo "Fake Package found"
fi

