FROM mysql:8.0.23

COPY schema.sql /docker-entrypoint-initdb.d

ENV MYSQL_USER=user
ENV MYSQL_PASSWORD=Password1!
ENV MYSQL_ROOT_PASSWORD=Password1!

EXPOSE 3307