param($day)
mkdir $day | cd
dotnet new console
out-file input.txt
cd ..
dotnet sln add $day