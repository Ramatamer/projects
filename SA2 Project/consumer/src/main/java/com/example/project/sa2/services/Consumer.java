package com.example.project.sa2.services;

import com.example.project.sa2.model.Property;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Service;

@Service
public class Consumer {

    private static final Logger LOGGER = LoggerFactory.getLogger(Consumer.class);

    @Autowired
    private ConsumerDbServices consumerDbServices;

    private final String topic = "${topic.name}";

    public Consumer(ConsumerDbServices consumerDbServices) {
        this.consumerDbServices = consumerDbServices;
    }


    @KafkaListener(topics = topic, groupId = "propertygroup")
    public void consume(Property property){
        if(property.isDeleteFlag()){
            consumerDbServices.deleteOneProperty(property.getId());
        }else {
            consumerDbServices.insert(property);
        }
    }
}
