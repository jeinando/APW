#!/bin/bash
echo "Esperando SQL Server..."
until /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" -C &>/dev/null; do
  sleep 2
done

echo "Creando bases de datos..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ProductDB')
    CREATE DATABASE [ProductDB];
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Foodbank')
    CREATE DATABASE [Foodbank];
"

/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -i /docker-entrypoint-initdb.d/productdb.sql
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -i /docker-entrypoint-initdb.d/foodbank.sql

echo "Listo."

