FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

RUN apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    rm -rf /var/lib/apt/lists/*

ENV PATH="$PATH:/opt/mssql-tools/bin"

USER mssql

COPY . /init-sql/
WORKDIR /init-sql

COPY --chmod=0755 entrypoint.sh /init-sql/entrypoint.sh

ENTRYPOINT ["/init-sql/entrypoint.sh"]
