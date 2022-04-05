package com.storyengine.springboot;
import java.util.ArrayList;

// A Role here represents the performance and role number of an auditioner

// To remove a role, set everything in associatedRoles to have discontinued=true
public class Role{
    public Scene scene;
    public ArrayList<Role> associatedRoles;
    public int roleNumber;
    public Auditioner auditioner;

    public Role(Auditioner auditioner, Scene scene, int roleNumber){
      this.auditioner = auditioner;
      this.scene = scene;
      this.roleNumber = roleNumber;
    }

    public Role(Auditioner auditioner, Scene scene, int roleNumber, ArrayList<Role> associatedRoles){
      this.auditioner = auditioner;
      this.scene = scene;
      this.roleNumber = roleNumber;
      this.associatedRoles = associatedRoles;
    }

    // Deletes this roll from all associated auditioners
    // Returns the number of roles removed from this auditioner
    public int Delete(){
      if(associatedRoles != null){
        for(Role role : associatedRoles){
          role.auditioner.roles.remove(role);
        }
      }
      auditioner.roles.remove(this);
      return 1;
    }

    // Deletes this role and all ancestors from all associated auditioners
    // Returns the number of roles removed from this auditioner
    public int DeleteLineage(int deletions, boolean ignoreAssociations){
      if(scene.getId().intValue() == scene.getParentSceneId()){
        System.out.print("Error, recursive scene parent loop");
        return 0;
      }

      if(associatedRoles != null && !ignoreAssociations){
        for(Role role : associatedRoles){
          if(role != this){
            role.DeleteLineage(0, true);
          }
        }
      }

      auditioner.roles.remove(this);
      deletions += 1;

      for(int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex++){
        Role possibleAncestor = auditioner.roles.get(roleIndex);
        if(scene.getParentSceneId().intValue() == possibleAncestor.scene.getId().intValue()){
          deletions += possibleAncestor.DeleteLineage(deletions, false);
          roleIndex -= 1;
        }
      }
      return deletions;
    }

    @Override
    public String toString(){
      String result = "    Scene: " + scene.getSceneName() + " (id: " + scene.getId() + ")";
      if(associatedRoles == null){
        result += "\n    Solo act";
      }else{
        result += "\n    Cast: " + associatedRoles.get(0).auditioner.actor.getId();
        for(int i = 1; i < associatedRoles.size(); i ++)
          result += ", " + associatedRoles.get(i).auditioner.actor.getId();
        result += "\n    Position: " + roleNumber;
      }
      return result;
    }
  }