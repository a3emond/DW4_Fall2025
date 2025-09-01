FROM mcr.microsoft.com/mssql/server:2022-latest

COPY . /init-sql/
WORKDIR /init-sql

# Copy entrypoint with correct permissions
COPY --chmod=0755 entrypoint.sh /init-sql/entrypoint.sh

ENTRYPOINT ["/init-sql/entrypoint.sh"]
