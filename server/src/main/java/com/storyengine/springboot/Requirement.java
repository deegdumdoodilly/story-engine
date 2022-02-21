package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class Requirement {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private Integer sceneId;

    private String requirement;

    private Integer role;


    public Requirement() {}

    public Requirement(Integer sceneId, String requirement, Integer role) {
        this.sceneId = sceneId;
        this.requirement = requirement;
        this.role = role;
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

    public String getRequirement() {
        return this.requirement;
    }

    public void setRequirement(String requirement) {
        this.requirement = requirement;
    }

    public Integer getRole() {
        return this.role;
    }

    public void setRole(Integer role) {
        this.role = role;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Requirement)) {
            return false;
        }
        Requirement requirement = (Requirement) o;
        return id == requirement.id;
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
            ", requirement='" + getRequirement() + "'" +
            ", role='" + getRole() + "'" +
            "}";
    }

}