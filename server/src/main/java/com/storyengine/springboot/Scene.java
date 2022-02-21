package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class Scene {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private String sceneName;

    private Integer numRequirements;

    private Integer numOutcomes;

    private Integer occurrences;

    private Integer numParticipants;

    private String description;

    public Scene(){};

    public Scene(String sceneName, Integer numRequirements, Integer numOutcomes, Integer occurrences, Integer numParticipants, String description) {
        this.sceneName = sceneName;
        this.numRequirements = numRequirements;
        this.numOutcomes = numOutcomes;
        this.occurrences = occurrences;
        this.numParticipants = numParticipants;
        this.description = description;
    }

    public void CopyFrom(Scene otherScene){
        this.id = otherScene.id;
        this.sceneName = otherScene.sceneName;
        this.numRequirements = otherScene.numRequirements;
        this.numOutcomes = otherScene.numOutcomes;
        this.occurrences = otherScene.occurrences;
        this.numParticipants = otherScene.numParticipants;
        this.description = otherScene.description;
    }
    

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getSceneName() {
        return this.sceneName;
    }

    public void setSceneName(String sceneName) {
        this.sceneName = sceneName;
    }

    public Integer getNumRequirements() {
        return this.numRequirements;
    }

    public void setNumRequirements(Integer numRequirements) {
        this.numRequirements = numRequirements;
    }

    public Integer getNumOutcomes() {
        return this.numOutcomes;
    }

    public void setNumOutcomes(Integer numOutcomes) {
        this.numOutcomes = numOutcomes;
    }

    public Integer getOccurrences() {
        return this.occurrences;
    }

    public void setOccurrences(Integer occurrences) {
        this.occurrences = occurrences;
    }

    public Integer getNumParticipants() {
        return this.numParticipants;
    }

    public void setNumParticipants(Integer numParticipants) {
        this.numParticipants = numParticipants;
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
        if (!(o instanceof Scene)) {
            return false;
        }
        Scene scene = (Scene) o;
        return id == scene.id;
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            "id:" + getId() +
            ", sceneName:'" + getSceneName() + "'" +
            ", numRequirements:" + getNumRequirements() +
            ", numOutcomes:" + getNumOutcomes() +
            ", occurrences:" + getOccurrences()  +
            ", numParticipants:" + getNumParticipants()  +
            ", description:'" + getDescription() + "'" +
            "}";
    }

}