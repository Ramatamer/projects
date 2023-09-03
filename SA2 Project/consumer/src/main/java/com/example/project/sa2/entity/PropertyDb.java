package com.example.project.sa2.entity;


import jakarta.persistence.*;
import lombok.Data;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "RealEstate")
public class PropertyDb {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    private String size;

    private Double price;

    private String bookingDate;

    private boolean  deleteFlag;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getSize() {
        return size;
    }

    public void setSize(String size) {
        this.size = size;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public String getBookingDate() {
        return bookingDate;
    }

    public void setBookingDate(String bookingDate) {
        this.bookingDate = bookingDate;
    }

    public boolean isDeleteFlag() {
        return deleteFlag;
    }

    public void setDeleteFlag(boolean deleteFlag) {
        this.deleteFlag = deleteFlag;
    }

    @Override
    public String toString() {
        return "Property{" +
                "size='" + size + '\'' +
                ", price=" + price +
                ", bookingDate='" + bookingDate + '\'' +
                '}';
    }
}
