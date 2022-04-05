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

    private Integer timeOfSubmission;

    private Integer chosenOutcomeId;

    private boolean inProgress;

    private boolean hasChosenOutcome;

    private boolean isWinningVote;

    public Vote(){}

    public Vote(Integer performanceId, Integer voterId, Integer timeOfSubmission, Integer chosenOutcomeId, boolean inProgress, boolean hasChosenOutcome) {
        this.performanceId = performanceId;
        this.voterId = voterId;
        this.timeOfSubmission = timeOfSubmission;
        this.chosenOutcomeId = chosenOutcomeId;
        this.inProgress = inProgress;
        this.hasChosenOutcome = hasChosenOutcome;
        this.isWinningVote = false;
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


    public boolean isWinningVote() {
        return this.isWinningVote;
    }

    public boolean getIsWinningVote() {
        return this.isWinningVote;
    }

    public void setIsWinningVote(boolean isWinningVote) {
        this.isWinningVote = isWinningVote;
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