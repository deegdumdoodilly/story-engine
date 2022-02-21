package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class Vote {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;

    private Integer performanceId;

    private Integer voterId;

    private Integer chosenOutcomeId;

    private boolean inProgress;

    private boolean hasChosenOutcome;

    public Vote(){}

    public Vote(Integer performanceId, Integer voterId, Integer chosenOutcomeId, boolean inProgress, boolean hasChosenOutcome) {
        this.performanceId = performanceId;
        this.voterId = voterId;
        this.chosenOutcomeId = chosenOutcomeId;
        this.inProgress = inProgress;
        this.hasChosenOutcome = hasChosenOutcome;
    }


    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getPerformanceId() {
        return this.performanceId;
    }

    public void setPerformanceId(Integer sceneId) {
        this.performanceId = sceneId;
    }

    public Integer getVoterId() {
        return this.voterId;
    }

    public void setVoterId(Integer voterId) {
        this.voterId = voterId;
    }

    public Integer getChosenOutcomeId() {
        return this.chosenOutcomeId;
    }

    public void setChosenOutcomeId(Integer chosenOutcomeId) {
        this.chosenOutcomeId = chosenOutcomeId;
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

    public boolean isHasChosenOutcome() {
        return this.hasChosenOutcome;
    }

    public boolean getHasChosenOutcome() {
        return this.hasChosenOutcome;
    }

    public void setHasChosenOutcome(boolean hasChosenOutcome) {
        this.hasChosenOutcome = hasChosenOutcome;
    }

    public Vote id(Integer id) {
        setId(id);
        return this;
    }

    public Vote sceneId(Integer sceneId) {
        setPerformanceId(sceneId);
        return this;
    }

    public Vote voterId(Integer voterId) {
        setVoterId(voterId);
        return this;
    }

    public Vote chosenOutcomeId(Integer chosenOutcomeId) {
        setChosenOutcomeId(chosenOutcomeId);
        return this;
    }

    public Vote inProgress(boolean inProgress) {
        setInProgress(inProgress);
        return this;
    }

    public Vote hasChosenOutcome(boolean hasChosenOutcome) {
        setHasChosenOutcome(hasChosenOutcome);
        return this;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof Vote)) {
            return false;
        }
        Vote vote = (Vote) o;
        return (id == vote.id);
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            " id='" + getId() + "'" +
            ", sceneId='" + getPerformanceId() + "'" +
            ", voterId='" + getVoterId() + "'" +
            ", chosenOutcomeId='" + getChosenOutcomeId() + "'" +
            ", inProgress='" + isInProgress() + "'" +
            ", hasChosenOutcome='" + isHasChosenOutcome() + "'" +
            "}";
    }
}