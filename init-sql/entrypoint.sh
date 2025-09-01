#!/bin/bash
# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

echo "Waiting for SQL Server to start..."
sleep 20s

# Run all SQL scripts
for f in /init-sql/*.sql
do
  echo "Running $f"
  /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -d master -i "$f"
done

wait
