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

    private Integer characterId;

    private String status;
    
    public Status(){};

    public Status(Integer characterId, String status){
        this.characterId = characterId;
        this.status = status;
    }

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getCharacterId() {
        return this.characterId;
    }

    public void setCharacterId(Integer characterId) {
        this.characterId = characterId;
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

    public Status characterId(Integer characterId) {
        setCharacterId(characterId);
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
            ", characterId='" + getCharacterId() + "'" +
            ", status='" + getStatus() + "'" +
            "}";
    }
}
