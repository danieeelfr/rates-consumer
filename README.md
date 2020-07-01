# Tech Challenge - API 2 (consumer-api)

## Solution overview

![overview](Resources/Img/api2.png)

## DevOps flow
![devops](Resources/Img/devops.png)

### Continuous integration pipeline
![devops](Resources/Img/ci.gif)

### Continuous delivery pipeline
![devops](Resources/Img/delivery.gif)

## Quality assurance
### Coverage report

![devcoverageops](Resources/Img/coverage.png)

## Running tests

### Unit and/or Integrated tests
```
dotnet test
```
### With coverage
```
dotnet test \                    
  --configuration Development \
  /p:CollectCoverage=true \
  /p:CoverletOutputFormat=cobertura \
  /p:CoverletOutput=./TestResults/Coverage/
```

```
dotnet tool run reportgenerator \
  -reports:./TestResults/Coverage/coverage.cobertura.xml \
  -targetdir:./CodeCoverage \
  -reporttypes:HtmlInline_AzurePipelines
```
## Dockering
### Build an image
```
docker build --pull --rm -f "Dockerfile" -t consumer-services:latest "."
```
### Running the image in a container
```
docker run --rm -d  -p 5000:5000/tcp -p 5001:5001/tcp consumer-services:latest
```
```
http://localhost:5000/index.html
```
## Deployment

```
just commit the changes
```
![devcoverageops](Resources/Img/release.png)

## Built With

* .NET Core 3.1
* .NET Tools
* C#
* GitHub
* Jwt
* Microsoft Azure
* Swagger
* Ubuntu 18.3
* Visual Studio Code

## What I've learning?

* Azure - Apps for containers
* Azure DevOps - Continuous integration (CI)
* Azure DevOps - Continuous delivery (CD)
* Azure DevOps - Quality analisys & coverage reports
* Developing .NET applications using Ubuntu and vscode.
* Docker - dockering .NET applications
* Docker - Azure Container Registry
* GitHub actions (not applied)
* Integration tests
* Pact tests (not applied)
* Unit tests
