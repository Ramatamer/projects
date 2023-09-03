package com.example.project.sa2.model;


import lombok.Data;
import lombok.Getter;
import lombok.Setter;

@Data
@Getter
@Setter
public class Property {
    private Long id;

    private String size;

    private Double price;

    private String bookingDate;

    private boolean  deleteFlag;


    @Override
    public String toString() {
        return "Property{" +
                "size='" + size + '\'' +
                ", price=" + price +
                ", bookingDate='" + bookingDate + '\'' +
                '}';
    }
}
