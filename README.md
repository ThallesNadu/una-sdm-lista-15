# una-sdm-lista-15
## Latência em Sistemas Distribuídos

No endpoint `api/intelligence/carbon-footprint`, foi adicionada uma latência artificial de 3 segundos com `Task.Delay(3000)` para simular a resposta lenta de sensores IoT em campo usando protocolos como MQTT ou HTTP.

Em uma frota com 10.000 veículos reportando falhas simultaneamente, essa latência poderia causar sobrecarga na API, aumento no tempo de resposta, filas de requisições, consumo excessivo de memória e possível indisponibilidade do sistema.

Para mitigar esse problema, a melhor estratégia seria usar mensageria com RabbitMQ ou Kafka. Assim, os veículos não dependeriam de resposta imediata da API principal. Cada falha seria enviada para uma fila, processada de forma assíncrona e distribuída entre vários consumidores.

Também poderia ser usado Redis para cache de consultas agregadas, como o cálculo de pegada de carbono por cidade. Dessa forma, dados consultados com frequência não precisariam ser recalculados a todo momento, reduzindo carga no banco e melhorando a escalabilidade.
