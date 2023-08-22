# Innoloft Backend Developer Application - (Olaore Oluwafunmibi Olumuyiwa)

FOLOW THE STEPS

### Api Documentation

The api documentation can be on swagger

```
{{Host}}/swagger/index.html
```

Based on [this repo](https://github.com/fxmbx/Innoloft-BE-Application).

## Usage

### 1. Build projects

From the base directory /Innoloft-BE-Application

```bash
dotnet build
```

### 2. Run test

```bash
dotnet test
```

### 3. Run Server

navigate into the EventModuleApi directory then run :

- Create database

```bash
 docker run -d --name mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=innoloft_db -e MYSQL_USER=innoloft -e MYSQL_PASSWORD=innoloftpass mysql:latest
```

- Create Redis

```bash
 docker run -d --name redis-server -p 6379:6379 -v redis-data:/data --restart unless-stopped redis:latest redis-server --requirepass pass

```

- Run migration

```bash
dotnet ef database update
```

- Start Server

```bash
dotnet watch run
```

# OR using make command

from the base directory /Innoloft-BE-Application

### 1. Build project

```bash
make build
```

- to run test

```bash
make test
```

- to run server

```bash
make mysql
```

```bash
make redis
```

- run migrations

```bash
make migrate
```

- run server

```bash
make server
```

### 2. Prepare docker

To build a dev container run:

```bash
docker-compose build --no-cache
```

To run docker-compose

```bash
docker-compose up -d
```
