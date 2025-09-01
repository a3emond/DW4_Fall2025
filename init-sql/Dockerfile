FROM mcr.microsoft.com/mssql/server:2022-latest

# Copy init scripts and entrypoint
COPY . /init-sql/

WORKDIR /init-sql

RUN chmod +x entrypoint.sh

ENTRYPOINT ["/init-sql/entrypoint.sh"]
