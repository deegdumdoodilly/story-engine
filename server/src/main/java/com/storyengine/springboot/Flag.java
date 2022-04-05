package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity
public class Flag {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private Integer actorId;

    private String flagkey;

    private String value;
    

    public Flag() {
    }

    public Flag(Integer id, Integer actorId, String flagkey, String value) {
        this.id = id;
        this.actorId = actorId;
        this.flagkey = flagkey;
        this.value = value;
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

    public String getKey() {
        return this.flagkey;
    }

    public void setKey(String flagkey) {
        this.flagkey = flagkey;
    }

    public String getValue() {
        return this.value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public Flag id(Integer id) {
        setId(id);
        return this;
    }

    public Flag actorId(Integer actorId) {
        setActorId(actorId);
        return this;
    }

    public Flag flagkey(String flagkey) {
        setKey(flagkey);
        return this;
    }

    public Flag value(String value) {
        setValue(value);
        return this;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Flag)) {
            return false;
        }
        Flag flag = (Flag) o;
        return id == flag.id;
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            "\"id\":\"" + getId() + "\"" +
            ",\"actorId\":\"" + getActorId() + "\"" +
            ",\"flagkey\":\"" + getKey() + "\"" +
            ",\"value\":\"" + getValue() + "\"" +
            "}";
    }

}
