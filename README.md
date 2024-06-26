# HexagonalCalculator

Este es un proyecto de calculadora desarrollado en .NET que permite realizar operaciones aritméticas básicas y ver el historial de operaciones realizadas.

## Características

- Realizar operaciones.
- Ver el historial de operaciones realizadas.

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

## Instalación

1. Clona el repositorio:


2. Restaura los paquetes de NuGet:

    ```bash
    dotnet restore
    ```

3. Construye el proyecto:

    ```bash
    dotnet build
    ```

## Uso

Configura la base de datos MySql usando Database *develop* y creando la tabla *operation*


```bash
    CREATE TABLE operation (id CHAR(36) NOT NULL PRIMARY KEY,operand1 INT NOT NULL,operand2 INT NOT NULL,total INT NOT NULL,operation_type VARCHAR(255) NOT NULL,creation_date DATETIME NOT NULL,UNIQUE KEY(id));
```

Para ejecutar la aplicación localmente:

```bash
dotnet run
```
## Contenerización

1. Navega al directorio del proyecto donde se encuentra el Dockerfile : _HexagonalCalculator\CalculatorHexagonal.Infrastructure.Entrypoint_


2. Construye la imagen Docker:

    ```bash
    docker build -t image/hexagonal-calculator:1.0 .
    ```

3. Ejecuta el docker-compose

    ```bash
    docker-compose up
    ```
4. Comprueba que los contenedores fueron creados correctamente y configura la base de datos *MySql* para asegurarse que la database es __develop__


5. Crea la tabla __operation__
 ```bash
    CREATE TABLE operation (id CHAR(36) NOT NULL PRIMARY KEY,operand1 CHAR(11) NOT NULL,operand2 CHAR(11) NOT NULL,total CHAR(11) NOT NULL,operation_type VARCHAR(255) NOT NULL,creation_date DATETIME NOT NULL,UNIQUE KEY(id));
```

6. Correr el contenedor de hexagonal-calculator


7. Observar el flujo de salida e interactuar con la consola:
 ```bash
    docker attach <container_id>
```