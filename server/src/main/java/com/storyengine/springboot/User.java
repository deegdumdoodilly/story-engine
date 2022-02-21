package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

@Entity // This tells Hibernate to make a table out of this class
public class User {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Integer id;
    
    private String username;

    private String passhash;

    private Integer votingChances;

    private Integer positiveVotes;

    private Integer neutralVotes;

    private Integer negativeVotes;

    private Boolean validVoter;

    public User(){};

    public User(String username, String passhash, Integer votingChances, Integer positiveVotes, Integer neutralVotes, Integer negativeVotes, Boolean validVoter) {
        this.username = username;
        this.passhash = passhash;
        this.votingChances = votingChances;
        this.positiveVotes = positiveVotes;
        this.neutralVotes = neutralVotes;
        this.negativeVotes = negativeVotes;
        this.validVoter = validVoter;
    }

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getUsername() {
        return this.username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPasshash() {
        return this.passhash;
    }

    public void setPasshash(String passhash) {
        this.passhash = passhash;
    }

    public Integer getVotingChances() {
        return this.votingChances;
    }

    public void setVotingChances(Integer votingChances) {
        this.votingChances = votingChances;
    }

    public Integer getPositiveVotes() {
        return this.positiveVotes;
    }

    public void setPositiveVotes(Integer positiveVotes) {
        this.positiveVotes = positiveVotes;
    }

    public Integer getNeutralVotes() {
        return this.neutralVotes;
    }

    public void setNeutralVotes(Integer neutralVotes) {
        this.neutralVotes = neutralVotes;
    }

    public Integer getNegativeVotes() {
        return this.negativeVotes;
    }

    public void setNegativeVotes(Integer negativeVotes) {
        this.negativeVotes = negativeVotes;
    }

    public Boolean isValidVoter() {
        return this.validVoter;
    }

    public void setValidVoter(Boolean validVoter) {
        this.validVoter = validVoter;
    }

    @Override
    public boolean equals(Object o) {
        if (o == this)
            return true;
        if (!(o instanceof User)) {
            return false;
        }
        User user = (User) o;
        return (id == user.id) && (username == user.username) && (passhash == user.passhash) && (votingChances == user.votingChances) && (positiveVotes == user.positiveVotes) && (neutralVotes == user.neutralVotes) && (negativeVotes == user.negativeVotes) && (validVoter == user.validVoter);
    }

    @Override
    public int hashCode() {
        return id;
    }

    @Override
    public String toString() {
        return "{" +
            " id:\"" + getId() + "\"" +
            ", username:\"" + getUsername() + "\"" +
            ", passhash:\"" + getPasshash() + "\"" +
            ", votingChances:\"" + getVotingChances() + "\"" +
            ", positiveVotes:\"" + getPositiveVotes() + "\"" +
            ", neutralVotes:\"" + getNeutralVotes() + "\"" +
            ", negativeVotes:\"" + getNegativeVotes() + "\"" +
            ", validVoter:\"" + isValidVoter() + "\"" +
            "}";
    }
}