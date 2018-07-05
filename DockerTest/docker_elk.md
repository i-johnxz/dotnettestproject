# elasticsearch
docker run -p 9200:9200 -p 9301:9300 --name elasticsearch_6.2.4 -e "discovery.type=single-node" -e "http.cors.allow-origin=*" -e "http.cors.enabled=true" -e "http.cors.allow-methods=OPTIONS,HEAD,GET,POST,PUT,DELETE" -e "http.cors.allow-headers=X-Requested-With,X-Auth-Token,Content-Type,Content-Length,Authorization" -e "http.cors.allow-credentials=true" -e "ES_JAVA_OPTS=-Xms512m -Xmx512m" docker.elastic.co/elasticsearch/elasticsearch:6.2.4

# kibana
docker run -p 9400:5601 --name kibana_6.2.4 -e "ELASTICSEARCH_URL=http://[ip]:9200/" -e "server.name=kibana_6.2.4" docker.elastic.co/kibana/kibana:6.2.4

# logstash
docker run -it -v /usr/local/logstash_6.2.4/config:/usr/share/logstash/config -v /usr/local/logstash_6.2.4/pipeline:/usr/share/logstash/pipeline -v /usr/local/logstash_6.2.4/geoip:/usr/share/logstash/geoip -p 9600:9601 --name logstash_6.2.4 -e "ES_JAVA_OPTS=-Xms64m -Xmx128m" docker.elastic.co/logstash/logstash:6.2.4

# docker cp
docker cp [container_name]:[path] [path]
例如 docker cp elasticsearch_6.2.4:/usr/share/elasticsearch/config /usr/local/elasticsearch_6.2.4/config

# docker status
docker stats --no-stream --format "table {{.Name}}\t{{.Container}}\t{{.CPUPerc}}\t{{.MemUsage}}\t{{.MemPerc}}"
