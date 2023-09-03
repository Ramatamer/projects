package com.example.project.sa2.services;

import com.example.project.sa2.model.Property;
import com.example.project.sa2.entity.PropertyDb;
import com.example.project.sa2.repository.PropertyRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ConsumerDbServices {

    private static final Logger LOGGER = LoggerFactory.getLogger(ConsumerDbServices.class);

    @Autowired
    private PropertyRepository propertyRepository;

    public ConsumerDbServices(PropertyRepository propertyRepository) {
        this.propertyRepository = propertyRepository;
    }

    public void insert(Property property){

        PropertyDb propertyDb = new PropertyDb();
        propertyDb.setPrice(property.getPrice());
        propertyDb.setSize(property.getSize());
        propertyDb.setBookingDate(property.getBookingDate());

        propertyRepository.save(propertyDb);
        LOGGER.info(String.format("New property is inserted Successfully in DataBase: %s", propertyDb.toString()));
    }

    public List<PropertyDb> selectAllProperty(){
        LOGGER.info(String.format("All property are selected Successfully from DataBase"));
        return propertyRepository.findAll();
    }

    public PropertyDb selectOneProperty(Long id){
        Optional<PropertyDb> propertyDb = propertyRepository.findById(id);
        LOGGER.info(String.format("One property is selected Successfully from DataBase"));
        return propertyDb.orElse(null);
    }

    public void deleteOneProperty(Long id){
        LOGGER.info(String.format("One property is deleted Successfully from DataBase"));
        propertyRepository.deleteById(id);
    }

    public PropertyDb updateOneProperty(PropertyDb newData, Long id){
        Optional<PropertyDb> oldData = propertyRepository.findById(id);

        if (oldData.isPresent()){
            PropertyDb propertyDb = oldData.get();
            propertyDb.setId(newData.getId());
            propertyDb.setPrice(newData.getPrice());
            propertyDb.setSize(newData.getSize());
            propertyDb.setBookingDate(newData.getBookingDate());


            LOGGER.info(String.format("One property is updated Successfully in DataBase"));
            return propertyRepository.save(propertyDb);
        }else{
            return null;
        }
    }
}
