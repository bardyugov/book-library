# Book-library
<div>Backend stack:</div>
<ul>
  <li>.Net</li>
  <li>C#</li>
  <li>ASP.NET</li>
  <li>Entity Framework Core</li>
  <li>MediatR</li>
  <li>Swagger</li>
</ul>


# Start
    dotnet tool install --global dotnet-ef
    cd ./Docker
    docker-compose up
    cd ..
    cd ./Scripts
    bash Build.sh update
    cd ..
    cd BookLibrary.Core
    mkdir wwwroot
    dotnet run
# Swagger 
    http://localhost:5126/swagger/index.html