#!/bin/bash

# HOW TO USE THIS FILE:
#
# copy all your sql scripts to /docker-entrypoint-initdb.d/ directory and you are good to go.

# Function to check if SQL Server is ready
wait_for_sql_server() {
        local rows_affected=0

        while [[ rows_affected -lt 4 ]]
        do
                echo "Waiting for sys.databases query to return 4 or more rows"
                sleep 1
                set +o pipefail # Turn off termination for sqlcmd failures, see earlier comments.
                rows_affected=$(/opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U sa -P "${SA_PASSWORD}" -Q 'SELECT name FROM sys.databases' 2>/dev/null | sed -n 's/(//; s/ rows affected.*//p;')
                set -o pipefail
        done
}

# Start SQL Server
/opt/mssql/bin/sqlservr &
pid=$!

# Wait for SQL Server to start
wait_for_sql_server

# Execute each SQL script in the directory

for file in /docker-entrypoint-initdb.d/*.sql
do
        echo "Executing $file"
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P ${SA_PASSWORD} -i $file
done

# Kill the SQL Server process
kill $pid
wait $pid

/opt/mssql/bin/sqlservr
