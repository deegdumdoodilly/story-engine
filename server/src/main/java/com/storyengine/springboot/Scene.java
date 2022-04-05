package com.storyengine.springboot;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import com.fasterxml.jackson.databind.JsonSerializable.Base;

import java.util.ArrayList;

@Entity // This tells Hibernate to make a table out of this class
public class Scene {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)

    private Integer id;

    private Integer parentSceneId;

    private String sceneName;

    private Integer numRequirements;

    private Integer numOutcomes;

    private Integer occurrences;

    private Integer numParticipants;

    private String description;

    private String briefDescription;

    private Integer priority;

    public Scene() {
    };

    public Scene(String sceneName, Integer numRequirements, Integer numOutcomes, Integer occurrences,
            Integer numParticipants,
            String description, String briefDescription, Integer priority, Integer parentSceneId) {
        this.sceneName = sceneName;
        this.numRequirements = numRequirements;
        this.numOutcomes = numOutcomes;
        this.occurrences = occurrences;
        this.numParticipants = numParticipants;
        this.description = description;
        this.briefDescription = briefDescription;
        this.priority = priority;
        this.parentSceneId = parentSceneId;
    }

    public void CopyFrom(Scene otherScene) {
        this.id = otherScene.id;
        this.sceneName = otherScene.sceneName;
        this.numRequirements = otherScene.numRequirements;
        this.numOutcomes = otherScene.numOutcomes;
        this.occurrences = otherScene.occurrences;
        this.numParticipants = otherScene.numParticipants;
        this.description = otherScene.description;
        this.briefDescription = otherScene.briefDescription;
        this.priority = otherScene.priority;
        this.parentSceneId = otherScene.parentSceneId;
    }

    public boolean IsSceneSingleplayer() {
        return getNumParticipants().intValue() == 1;
    }

    public boolean AllRequirementsSatisfied(ArrayList<Auditioner> cast, ArrayList<Requirement> requirementList,
            Iterable<Status> statusList, Iterable<Flag> flagList, int currentTime) {
        for (Requirement req : requirementList) {
            if (req.getSceneId().intValue() == getId().intValue() && !RequirementSatisfied(req, cast, statusList, flagList, currentTime)) {
                return false;
            }
        }
        return true;
    }

    public boolean AllRequirementsSatisfied(Auditioner auditioner, ArrayList<Requirement> requirementList,
            Iterable<Status> statusList, Iterable<Flag> flagList, int currentTime) {
        ArrayList<Auditioner> cast = new ArrayList<Auditioner>();
        cast.add(auditioner);
        return AllRequirementsSatisfied(cast, requirementList, statusList, flagList, currentTime);
    }

    public static boolean RequirementSatisfied(Requirement requirement, ArrayList<Auditioner> cast,
            Iterable<Status> statusList, Iterable<Flag> flagList, int currentTime) {

        // Tokenize by space

        // Setup
        String requirementText = requirement.getRequirement();
        char[] charArray = requirementText.trim().toCharArray();

        // Character reading logic
        String buffer = "";
        boolean inQuote = false;
        boolean escapeNextChar = false;

        // Token reading logic
        ArrayList<String> firstExpression = new ArrayList<String>();
        ArrayList<String> secondExpression = new ArrayList<String>();
        ArrayList<String> currentExpression = firstExpression;
        String comparator = "";
        boolean expectingCharacterNumber = false;
        int characterNumber = -1;
        boolean expectingCharacterField = false;
        boolean expectingFlagKey = false;
        //System.out.println(requirementText);
        for (int i = 0; i <= charArray.length; i++) {
            char character;
            if(i < charArray.length){
                character = charArray[i];
            }else{
                character = ' ';
            }
            if (escapeNextChar) {
                buffer += character;
                escapeNextChar = false;
            } else if (character == '\\') {
                escapeNextChar = true;
            } else if (character == '\"') {
                inQuote = !inQuote;
            } else if (inQuote || character != ' ') {
                buffer += character;
            } else if (buffer.length() > 0) {
                // Character was a non-quoted space
                // System.out.println("Processing " + buffer);
                if (expectingCharacterNumber) {
                    // System.out.println("result: char num");
                    // Buffer should contain a character number
                    try {
                        characterNumber = Integer.parseInt(buffer);
                    } catch (NumberFormatException ex) {
                        //System.out.println("Could not parse int from " + buffer + ", " + requirementText);
                        return false;
                    }
                    if (characterNumber > cast.size()) {
                        //System.out.println("No character " + characterNumber + " in this scene, " + requirementText);
                        return false;
                    }
                    expectingCharacterField = true;
                    expectingCharacterNumber = false;
                } else if (expectingFlagKey) {
                    Actor actor = cast.get(characterNumber - 1).actor;
                    for(Flag flag : flagList){
                        if(flag.getActorId().intValue() == actor.getId().intValue() && flag.getKey().equals(buffer)){
                            currentExpression.add(flag.getValue());
                        }
                    }
                    expectingFlagKey = false;
                } else if (expectingCharacterField) {
                    // System.out.println("result: char field");
                    Actor actor = cast.get(characterNumber - 1).actor;
                    // Buffer should contain a character field
                    if (buffer.equals("status")) {
                        for (Status status : statusList) {
                            //System.out.println("checking status " + status.getStatus());
                            //System.out.println("status id " + status.getActorId());
                            //System.out.println("actor id " + actor.getId());
                            if (status.getActorId().intValue() == actor.getId().intValue()) {
                                //System.out.println("adding");
                                currentExpression.add(status.getStatus());
                                //System.out.println(firstExpression.size());
                            }
                        }
                    } else if (buffer.equals("environment")) {
                        currentExpression.add(actor.getEnvironment());
                    } else if (buffer.equals("name")) {
                        currentExpression.add(actor.getName());
                    } else if (buffer.equals("flag")) {
                        expectingFlagKey = true;
                    } else {
                        //System.out.println("Did not recognize character field \'" + buffer + "\', " + requirementText);
                    }
                    expectingCharacterField = false;
                } else if(charArray[i-1] == '\"'){
                    // Term was in quotes, add it without processing
                    // System.out.println("result: literal");
                    currentExpression.add(buffer);
                } else if (buffer.equals("is") || buffer.equals("not") || buffer.equals("contains")
                        || buffer.equals("contain") || buffer.equals("greater") || buffer.equals("less")
                        || buffer.equals("than")) {
                    // System.out.println("result: operand");
                    // Buffer represents an operand
                    if (secondExpression.size() > 0) {
                        System.out.println("Error, only one comparator expression permitted: " + requirementText);
                        return false;
                    }
                    comparator += buffer;
                    currentExpression = secondExpression;
                } else if (buffer.equals("character")) {
                    // System.out.println("result: character");
                    // Buffer indicates the start of a character field
                    expectingCharacterNumber = true;
                } else if (buffer.equals("time")) {
                    // System.out.println("result: time");
                    // Buffer indicates the start of a character field
                    currentExpression.add("" + currentTime);
                } else if (buffer.equals("morning")){
                    for(int t = 0; t <= currentTime; t+=4){
                        currentExpression.add(t + "");
                    }
                } else if (buffer.equals("afternoon")){
                    for(int t = 1; t <= currentTime; t+=4){
                        currentExpression.add(t + "");
                    }
                } else if (buffer.equals("evening")){
                    for(int t = 2; t <= currentTime; t+=4){
                        currentExpression.add(t + "");
                    }
                } else if (buffer.equals("night")){
                    for(int t = 3; t <= currentTime; t+=4){
                        currentExpression.add(t + "");
                    }
                } else if (!buffer.equals("or") && !buffer.equals("does")) {
                    // Buffer contains a literal value. We ignore 'or's and discard them
                    currentExpression.add(buffer);
                }
                buffer = "";
            }
        }
        if (buffer.length() > 0) {
            currentExpression.add(buffer);
            buffer = "";
        }

        // Evaluate expressions
        for(String a : firstExpression)
            System.out.println("f: " + a);
            
        for(String a : secondExpression)
            System.out.println("s: " + a);

        System.out.println("comparator " + comparator);

        boolean truthValue = true;
        if (comparator.contains("not")) {
            truthValue = false;
        }

        if (comparator.contains("contain")) {
            for (String a : firstExpression) {
                for (String b : secondExpression) {
                    if (a.contains(b)) {
                        return truthValue;
                    }
                }
            }
            return !truthValue;
        } else if (comparator.contains("than") || comparator.contains("equals")) {
            int sumA = 0, sumB = 0;
            for (String a : firstExpression) {
                try {
                    sumA += Integer.parseInt(a);
                } catch (NumberFormatException ex) {
                    System.out.println(
                            "In a numerical comparison (greater than or less than), all values must be numbers. "
                                    + requirementText);
                }
            }
            for (String b : secondExpression) {
                try {
                    sumB += Integer.parseInt(b);
                } catch (NumberFormatException ex) {
                    System.out.println(
                            "In a numerical comparison (greater than or less than), all values must be numbers. "
                                    + requirementText);
                }
            }
            if (comparator.contains("greater")) {
                return (sumA > sumB) == truthValue;
            } else if (comparator.contains("less")) {
                return (sumA < sumB) == truthValue;
            } else if (comparator.contains("equal")) {
                return (sumA == sumB) == truthValue;
            } else {
                System.out.println("No proper comparator expression dectected");
                return false;
            }
        } else if (comparator.contains("is")) {
            //System.out.println("Counts: " + firstExpression.size() + ", " + secondExpression.size());
            for (String a : firstExpression) {
                for (String b : secondExpression) {
                    //System.out.println("Comparing " + a + " and " + b);
                    if (a.equals(b)) {
                        return truthValue;
                    }
                }
            }
            return !truthValue;
        } else {
            System.out.println("No proper comparator expression dectected");
            return false;
        }
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

    public void incrementOccurrences() {
        this.occurrences += 1;
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

    public String getBriefDescription() {
        return this.briefDescription;
    }

    public void setBriefDescription(String briefDescription) {
        this.briefDescription = briefDescription;
    }

    public Integer getPriority() {
        return this.priority;
    }

    public void setPriority(Integer priority) {
        this.priority = priority;
    }

    public Integer getParentSceneId() {
        return this.parentSceneId;
    }

    public void setParentSceneId(Integer parentSceneId) {
        this.parentSceneId = parentSceneId;
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
                ", occurrences:" + getOccurrences() +
                ", numParticipants:" + getNumParticipants() +
                ", description:'" + getDescription() + "'" +
                ", briefDescription:" + getBriefDescription() +
                ", priority:'" + getPriority() + "'" +
                "}";
    }

}