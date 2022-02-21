package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class Outcome {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private Integer sceneId;

    private Integer type;

    private String effect;
    
    private String description;

    public Outcome() {}

    public Outcome(Integer sceneId, Integer type, String effect, String description) {
        this.sceneId = sceneId;
        this.type = type;
        this.effect = effect;
        this.description = description;
    }
    

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getSceneId() {
        return this.sceneId;
    }

    public void setSceneId(Integer sceneId) {
        this.sceneId = sceneId;
    }

    public Integer getType() {
        return this.type;
    }

    public void setType(Integer type) {
        this.type = type;
    }

    public String getEffect() {
        return this.effect;
    }

    public void setEffect(String effect) {
        this.effect = effect;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Outcome)) {
            return false;
        }
        Outcome outcome = (Outcome) o;
        return id == outcome.id;
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            " id='" + getId() + "'" +
            ", sceneId='" + getSceneId() + "'" +
            ", type='" + getType() + "'" +
            ", effect='" + getEffect() + "'" +
            ", description='" + getDescription() + "'" +
            "}";
    }

}