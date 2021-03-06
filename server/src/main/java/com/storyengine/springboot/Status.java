package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity
public class Status {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private Integer actorId;

    private String status;
    
    public Status(){};

    public Status(Integer actorId, String status){
        this.actorId = actorId;
        this.status = status;
    }

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getActorId() {
        return this.actorId;
    }

    public void setActorId(Integer actorId) {
        this.actorId = actorId;
    }

    public String getStatus() {
        return this.status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public Status id(Integer id) {
        setId(id);
        return this;
    }

    public Status actorId(Integer actorId) {
        setActorId(actorId);
        return this;
    }

    public Status status(String status) {
        setStatus(status);
        return this;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Status)) {
            return false;
        }
        Status status = (Status) o;
        return (id == status.id);
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            " id='" + getId() + "'" +
            ", actorId='" + getActorId() + "'" +
            ", status='" + getStatus() + "'" +
            "}";
    }
}
