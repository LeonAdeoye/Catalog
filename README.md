To create a new project solution run the command: dotnet new webapi -name catalog

To check health use https://localhost:7170/health/
To check health use https://localhost:7170/health/live
To check health use https://localhost:7170/health/ready


For Postman or Chrome use 
https://localhost:7170/items
http://localhost:5000/items/86d3d360-cead-471d-92e6-19c45e492980


Create DockerFile  and remember that Docker is case-sensitive.
To build docker image: docker build -t catalog:v1 .

To run the container you can use Docker Desktop or run on the command line: 
docker run -it --rm -p 7170:80 --name catalog_container catalog:v1


To see docker images: docker images

To check what comntainers are running: docker ps

To delete a docker image: docker rmi catalog:v1

