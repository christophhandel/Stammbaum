# Stammbaum-MongoDB-DBI-5ahif

### [Production System](https://stammbaum.naichinger.com).

## Start neo4j container lulz
```bash
docker volume create neo4j
docker run --name neo4j -p7474:7474 -p7687:7687 -d -v neo4j:/data --env NEO4J_AUTH=neo4j/password neo4j:latest
```
