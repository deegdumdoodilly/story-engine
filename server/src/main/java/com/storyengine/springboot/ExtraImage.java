package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class ExtraImage {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private Integer sceneId;

    private String url;

    public ExtraImage(){};

    public ExtraImage(Integer sceneId, String url) {
        this.sceneId = sceneId;
        this.url = url;
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

    public String getUrl() {
        return this.url;
    }

    public void setUrl(String url) {
        this.url = url;
    }
    

    @Override
    public String toString() {
        return "{" +
            " id:\"" + getId() + "\"" +
            ", sceneId:\"" + getSceneId() + "\"" +
            ", url:\"" + getUrl() + "\"" +
            "}";
    }
}