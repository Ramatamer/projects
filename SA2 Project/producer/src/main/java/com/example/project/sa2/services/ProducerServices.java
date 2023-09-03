package com.example.project.sa2.services;

import com.example.project.sa2.model.Property;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.kafka.core.KafkaTemplate;
import org.springframework.kafka.support.KafkaHeaders;
import org.springframework.messaging.Message;
import org.springframework.messaging.support.MessageBuilder;
import org.springframework.stereotype.Service;

@Service
public class ProducerServices {

    @Value("${topic.name}")
    private String topic;
    private static final Logger LOGGER = LoggerFactory.getLogger(ProducerServices.class);

    @Autowired
    private KafkaTemplate<String, Property> kafkaTemplate;

    public ProducerServices(KafkaTemplate<String, Property> kafkaTemplate) {
        this.kafkaTemplate = kafkaTemplate;
    }

    public void send(Property property){
        Message<Property> message = MessageBuilder
                .withPayload(property)
                .setHeader(KafkaHeaders.TOPIC, topic)
                .build();
        kafkaTemplate.send(message);
        LOGGER.info(String.format("Property is sent successfully! %s", property.toString()));
    }

}
