FROM openjdk:11-jre-slim

EXPOSE 8080/tcp

COPY target/*.jar app.jar
ENTRYPOINT ["java", "-jar", "/app.jar"]