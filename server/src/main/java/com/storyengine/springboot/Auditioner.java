package com.storyengine.springboot;
import java.util.ArrayList;

// An Auditioner is an Actor trying out for different performances in different roles
public class Auditioner implements Comparable{
    public Actor actor;
    public ArrayList<Role> roles;
    public int highestPriority;
    public int lowestFrequency;
    public ArrayList<Integer> parentSceneIds;

    public Auditioner(Actor actor){
        this.actor = actor;
        this.roles = new ArrayList<Role>();
        this.highestPriority = -99;
        this.lowestFrequency = 99;
    }

    public void AddRolePermutations(Scene scene, ArrayList<Auditioner> auditioners, ArrayList<Requirement> requirementsList, Iterable<Status> statusList, int time){
        ArrayList<Auditioner> cast = new ArrayList<Auditioner>();
        cast.add(this);
        AddRolePermutationsRecursive(scene, cast, auditioners, requirementsList, statusList, time);
    }

    private void AddRolePermutationsRecursive(Scene scene, ArrayList<Auditioner> cast, ArrayList<Auditioner> auditioners, ArrayList<Requirement> requirementsList, Iterable<Status> statusList, int time){
        if(scene.getNumParticipants() > cast.size()){
        // Need more members, attempt to add one
        for(Auditioner nextAuditioner : auditioners){
            if(!cast.contains(nextAuditioner)){
                cast.add(nextAuditioner);
                AddRolePermutationsRecursive(scene, cast, auditioners, requirementsList, statusList, time);
                cast.remove(nextAuditioner);
            }
        }
        }else{
            if(scene.AllRequirementsSatisfied(cast, requirementsList, statusList, time)){
                ArrayList<Role> associatedRoles = new ArrayList<Role>();
                for(int i = 1; i <= cast.size(); i++){
                Role newRole = new Role(cast.get(i-1), scene, i);
                newRole.associatedRoles = associatedRoles;
                associatedRoles.add(newRole);
                cast.get(i-1).roles.add(newRole);
                }
            }
        }
    }

    @Override
    public int compareTo(Object o) {
        Auditioner other = (Auditioner) o;
        if(this.highestPriority == other.highestPriority){
        return Integer.compare(this.roles.size(), other.roles.size());
        }
        return Integer.compare(this.highestPriority, other.highestPriority);
    }

    @Override
    public String toString(){
        String result = "Auditioner: " + actor.getName();
        result +=     " (id: " + actor.getId() + ")";
        result +=     "\nCurrent roles:\n";
        for(Role r : roles)
        result += r.toString() + "\n";
        return result;
    }
}
