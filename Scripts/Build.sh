arg1=$1
arg2=$2
cd ..
if [ "$arg1" = "update" ]; then
  dotnet ef database update --project BookLibrary.Infrastructure --startup-project BookLibrary.Core
elif [ "$arg1" = "migrate" ]; then
  dotnet ef migrations add $arg2 --project BookLibrary.Infrastructure --startup-project BookLibrary.Core
else 
  echo "Not found scripts"
fi