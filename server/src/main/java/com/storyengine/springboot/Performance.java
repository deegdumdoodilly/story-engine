package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class Performance {
    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;

    private Integer sceneId;

    private boolean inProgress;

    private Integer winningVote;

    private String flavor;

    private String participants;

    private Integer time;

    private Integer flavorAuthor;

    public Performance(){};

    public Performance(Integer sceneId, boolean inProgress, Integer winningVote, String flavor, String participants, Integer time) {
        this.sceneId = sceneId;
        this.inProgress = inProgress;
        this.winningVote = winningVote;
        this.flavor = flavor;
        this.participants = participants;
        this.time = time;
        this.flavorAuthor = -1;
    }

    public Performance(Integer sceneId, Integer time){
        this(sceneId, true, -1, "", "", time);
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

    public boolean isInProgress() {
        return this.inProgress;
    }

    public boolean getInProgress() {
        return this.inProgress;
    }

    public void setInProgress(boolean inProgress) {
        this.inProgress = inProgress;
    }

    public Integer getWinningVote() {
        return this.winningVote;
    }

    public void setWinningVote(Integer winningVote) {
        this.winningVote = winningVote;
    }

    public String getFlavor() {
        return this.flavor;
    }

    public void setFlavor(String flavor) {
        this.flavor = flavor;
    }

    public String getParticipants() {
        return this.participants;
    }

    public void setParticipants(String participants) {
        this.participants = participants;
    }

    public void addParticipant(String participant) {
        this.participants += "," + participant;
    }

    public Integer getTime() {
        return this.time;
    }

    public void setTime(Integer time) {
        this.time = time;
    }

    public Performance time(Integer time) {
        setTime(time);
        return this;
    }


    public Integer getFlavorAuthor() {
        return this.flavorAuthor;
    }

    public void setFlavorAuthor(Integer flavorAuthor) {
        this.flavorAuthor = flavorAuthor;
    }



    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Performance)) {
            return false;
        }
        Performance performance = (Performance) o;
        return id == performance.id;
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
            ", inProgress='" + isInProgress() + "'" +
            ", winningVote='" + getWinningVote() + "'" +
            ", flavor='" + getFlavor() + "'" +
            ", participants='" + getParticipants() + "'" +
            ", time='" + getTime() + "'" +
            ", flavorAuthor='" + getFlavorAuthor() + "'" +
            "}";
    }

}