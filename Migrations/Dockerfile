FROM flyway/flyway
WORKDIR /src
COPY Migrations/sql .
COPY Migrations/wait-for-it.sh ./wait-for-it.sh
USER root
RUN chmod +x ./wait-for-it.sh
USER flyway