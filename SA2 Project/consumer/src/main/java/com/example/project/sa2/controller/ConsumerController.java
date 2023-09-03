package com.example.project.sa2.controller;

import com.example.project.sa2.entity.PropertyDb;
import com.example.project.sa2.services.ConsumerDbServices;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/client")
public class ConsumerController {

    private static final Logger LOGGER = LoggerFactory.getLogger(ConsumerController.class);

    @Autowired
    private ConsumerDbServices consumerDbServices;

    public ConsumerController(ConsumerDbServices consumerDbServices) {
        this.consumerDbServices = consumerDbServices;
    }

    @GetMapping("/PropertyList")
    public List<PropertyDb> readAllProperties(){
        LOGGER.info(String.format("Request of All Properties is performed Successfully"));
        return consumerDbServices.selectAllProperty();
    }

    @GetMapping("/oneProperty/{id}")
    public PropertyDb readOneProperty(@PathVariable("id") Long id){
        LOGGER.info(String.format("Request of One Property is performed Successfully"));
        return consumerDbServices.selectOneProperty(id);
    }
    @DeleteMapping("/delete/{id}")
    public ResponseEntity<String> removeOneProperty(@PathVariable("id") Long id){
        LOGGER.info(String.format("Request of delete One Property is performed Successfully"));
        consumerDbServices.deleteOneProperty(id);
        return ResponseEntity.ok("Property is deleted Successfully with id : " + id);
    }

    @PutMapping("/update/{id}")
    public ResponseEntity<String> updateProperty(@RequestBody PropertyDb newData , @PathVariable("id") Long id){
        LOGGER.info(String.format("Request of update One Property is performed Successfully"));
        return ResponseEntity.ok("Property is updated Successfully : " + consumerDbServices.updateOneProperty(newData, id).toString());
    }

}

//   http://localhost:8081/client/PropertyList
//   http://localhost:8081/client/oneProperty/:id
//   http://localhost:8081/client/delete/:id
//   http://localhost:8081/client/update/:id