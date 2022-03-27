package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class Actor {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private String name;

    private Integer lastAte;

    private String environment;

    public Actor(){};

    public Actor(String name, Integer lastAte, String environment) {
        this.name = name;
        this.lastAte = lastAte;
        this.environment = environment;
    }
    
    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getLastAte() {
        return this.lastAte;
    }

    public void setLastAte(Integer lastAte) {
        this.lastAte = lastAte;
    }

    public String getEnvironment() {
        return this.environment;
    }

    public void setEnvironment(String environment) {
        this.environment = environment;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Actor)) {
            return false;
        }
        Actor character = (Actor) o;
        return character.id == id;
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            ", id:\"" + getId() + "\"" +
            " name:\"" + getName() + "\"" +
            ", lastAte:\"" + getLastAte() + "\"" +
            ", environment:\"" + getEnvironment() + "\"" +
            "}";
    }
}