package com.example.project.sa2.controller;

import com.example.project.sa2.model.Property;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.example.project.sa2.services.ProducerServices;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/owner")
public class producerController {

    private static final Logger LOGGER = LoggerFactory.getLogger(producerController.class);

    @Autowired
    private ProducerServices producerServices;

    @PostMapping("/publish")
    public ResponseEntity<String> publish (@RequestBody Property property){
        producerServices.send(property);
        LOGGER.info(String.format("Request of insert property is performed successfully! %s", property.toString()));
        return ResponseEntity.ok("property is sent successfully!" + property.toString());
    }



    @DeleteMapping("/delete")
    public ResponseEntity<String> delete (@RequestBody Property property){
        property.setDeleteFlag(true);
        producerServices.send(property);
        LOGGER.info(String.format("Request of delete ... is performed successfully! %s", property.toString()));
        return ResponseEntity.ok("... is sent successfully!" + property.toString());
    }


}

//       http://localhost:8080/.../publish
//       http://localhost:8080/.../delete