# How to use
- ## Build the optimized build
	- Inside the `xtramile` folder which contains the `xtramile.csproj` file, run this command.
	```
	dotnet publish -c Release
	```
- ## Run the published file
	- Go to the publish folder where the `xtramile.dll` stored, then run this command.
	```
	dotnet xtramile.dll
	```
	The application will be hosted on `http://localhost:5000`.

# For using the development setting
- ## Run the backend
	- Inside the `xtramile` folder which contains the `xtramile.csproj` file, run this command.
	```
	dotnet run
	```
	The server will run on `http://localhost:5000`
- ## Run the frontend
	- Go to `ClientApp` folder, then run this command
	```
	npm start
	```
	The frontend will be hosted on `http://localhost:3000`

# Swagger
Swagger can be accessed on `http://localhost:5000/swagger`.