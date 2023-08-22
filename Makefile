ImageName = innoloft
ContainerName = innoloft


redis:
	docker run -d --name redis-server -p 6379:6379 -v redis-data:/data --restart unless-stopped redis:latest redis-server --requirepass pass

mysql:
	docker run -d --name mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=innoloft_db -e MYSQL_USER=innoloft -e MYSQL_PASSWORD=innoloftpass mysql:latest

migrate:
	cd EventModuleApi && \
	dotnet ef database update

server:
	cd EventModuleApi && \
	dotnet watch run 

build:
	dotnet build

restore:
	dotnet restore

test:
	dotnet test

build:
	dotnet build 


	
build-docker:
	docker build -t $(ImageName):latest .

run-docker: 
	docker run -d --name $(ContainerName) -p 5177:5177 $(ImageName):latest
	
up:
	docker-compse up -d 

.PHONY: redis mysql network server build restore test build migrate build-docker run-docker up
