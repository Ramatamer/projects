package com.example.project.sa2.repository;

import com.example.project.sa2.entity.PropertyDb;
import org.hibernate.metamodel.model.convert.spi.JpaAttributeConverter;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PropertyRepository extends JpaRepository<PropertyDb, Long> {
}
