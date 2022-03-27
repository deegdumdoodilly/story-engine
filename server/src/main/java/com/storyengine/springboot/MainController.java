package com.storyengine.springboot;

import com.storyengine.springboot.User;

import org.apache.catalina.core.ApplicationContext;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.PathVariable;

import org.springframework.web.server.ResponseStatusException;

import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Random;

@RestController 
@RequestMapping(path="/api") // This means URL's start with /api (after Application path)
public class MainController {
  @Autowired
  private TimeRepository timeRepository;
  @Autowired
  private SceneRepository sceneRepository;
  @Autowired
  private UserRepository userRepository;
  @Autowired
  private PerformanceRepository performanceRepository;
  @Autowired
  private VoteRepository voteRepository;
  @Autowired
  private StatusRepository statusRepository;
  @Autowired
  private ActorRepository actorRepository;
  @Autowired
  private OutcomeRepository outcomeRepository;
  @Autowired
  private RequirementRepository requirementRepository;


  // Get Mappings

  @GetMapping(path="/time")
  public @ResponseBody Integer GetTime() {
    return timeRepository.findById(1).get().getTime();
  }

  @GetMapping(path="/health")
  public @ResponseBody String GetHealth() {
    return "{\"status\":\"UP\"}";
  }


  // Post Mappings

  @PostMapping(path="/time/advance")
  public @ResponseBody String advanceTime() {
    Time t = timeRepository.findById(1).get();
    t.setTime(t.getTime() + 1);
    timeRepository.save(t);
    return "Time advanced";
  }

  @PostMapping(path="/time/reset")
  public @ResponseBody String resetTime() {
    Time t = timeRepository.findById(1).get();
    t.setTime(0);
    timeRepository.save(t);
    return "Time reset";
  }

  @PostMapping(path="/simulation/reset")
  public @ResponseBody String ResetSimulation(){
    for(Scene s : sceneRepository.findAll()){
      s.setOccurrences(0);
      sceneRepository.save(s);
    }
    for(User u : userRepository.findAll()){
      u.setVotingChances(0);
      u.setPositiveVotes(0);
      u.setNeutralVotes(0);
      u.setNegativeVotes(0);
      userRepository.save(u);
    }
    for(Actor a : actorRepository.findAll()){
      a.setEnvironment("Woods");
      actorRepository.save(a);
    }
    Time t = timeRepository.findById(1).get();
    t.setTime(0);
    timeRepository.save(t);
    
    performanceRepository.deleteAll();
    voteRepository.deleteAll();
    statusRepository.deleteAll();

    
    System.out.println("Simulation reset\n\n");
    return "Simulation reset";
  }

  @PostMapping(path="/simulation/assign-performances")
  public @ResponseBody Iterable<Performance> AssignPerformances(@RequestParam(required = false) Integer seed, @RequestParam(required = false) Integer votesPerPerformance){
    Random randomNumberGenerator;
    if(seed == null){
      randomNumberGenerator = new Random();
    }else{
      randomNumberGenerator = new Random(seed);
    }

    if(votesPerPerformance == null)
      votesPerPerformance = 1;
    

    // Will store the final set of new performances
    ArrayList<Performance> performanceStack = new ArrayList<Performance>();

    // Get a list of all actors and shuffle
    System.out.println("Assembling auditioners...\n\n");
    ArrayList<Auditioner> auditioners = new ArrayList<Auditioner>(); 
    for(Actor a : actorRepository.findAll()){
      Auditioner newAuditioner = new Auditioner(a);
      auditioners.add(newAuditioner);
    }
    Collections.shuffle(auditioners, randomNumberGenerator);

    // Get a list of all requirements for later
    ArrayList<Requirement> requirementList = new ArrayList<Requirement>();
    for(Requirement req : requirementRepository.findAll()){
      requirementList.add(req);
    }

    // For each auditioner:
    // - For each scene:
    //   - If the scene is singleplayer and we meet the requirements, add it as a possible Role.
    //   - If the scene is multiplayer, check every permutation of auditioners in other roles. If any meet the requirements, add them as possible Roles to all involved auditioners.
    // - Filter out all but the highest priority roles

    // For each auditioner (in order of who has the highest priority roles):
    // Filter out all higher frequency events
    // Filter out all less complex events
    // - if it's multiplayer, make sure all associated Auditioners get signed off on that event too
    System.out.println("Assembling all possible roles...\n\n");
    Iterable<Status> statusList = statusRepository.findAll();
    for(Auditioner auditioner : auditioners){
      for(Scene scene : sceneRepository.findAll()){
        if(scene.IsSceneSingleplayer()){
          if(scene.AllRequirementsSatisfied(auditioner, requirementList, statusList, GetTime())){
            auditioner.roles.add(new Role(auditioner, scene, 1));
          }
        }else{
          auditioner.AddRolePermutations(scene, auditioners, requirementList, statusList, GetTime());
        }
      }
    }

    // for(Auditioner auditioner : auditioners)
    //   System.out.println(auditioner.toString());

    System.out.println("Filtering by priority...\n\n");
    // Filter by priority
    for(Auditioner auditioner : auditioners){
      for(Role role : auditioner.roles){
        auditioner.highestPriority = Integer.max(auditioner.highestPriority, role.scene.getPriority());
      }

      for(int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex ++){
        Role role = auditioner.roles.get(roleIndex);
        if(role.scene.getPriority() < auditioner.highestPriority){
          roleIndex -= role.Delete();
        }
      }
    }

    Collections.sort(auditioners);

    
    // for(Auditioner auditioner : auditioners)
    //   System.out.println(auditioner.toString());
    
    System.out.println("Filtering by frequency...\n\n");
    // Filter out all higher frequency events
    for(Auditioner auditioner : auditioners){
      for(Role role : auditioner.roles){
        auditioner.lowestFrequency = Integer.min(auditioner.lowestFrequency, role.scene.getOccurrences());
      }

      for(int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex ++){
        Role role = auditioner.roles.get(roleIndex);
        if(role.scene.getOccurrences() > auditioner.lowestFrequency){
          roleIndex -= role.Delete();
        }
      }
    }

    
    // for(Auditioner auditioner : auditioners)
    //   System.out.println(auditioner.toString());
    System.out.println("Filtering parent scenes...\n\n");
    // Filter out all parent scenes
    for(Auditioner auditioner : auditioners){
      auditioner.parentSceneIds = new ArrayList<Integer>();
      for(Role role : auditioner.roles){
        Integer parentSceneId = role.scene.getParentSceneId();
        if(parentSceneId != null){
          auditioner.parentSceneIds.add(parentSceneId);
        }
      }

      for(int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex ++){
        Role role = auditioner.roles.get(roleIndex);
        if(auditioner.parentSceneIds.contains(role.scene.getId())){
          roleIndex -= role.DeleteLineage(0, false);
        }
      }
    }

    Collections.sort(auditioners);

    // for(Auditioner auditioner : auditioners)
    //   System.out.println(auditioner.toString());
    System.out.println("Selecting roles...\n\n");
    // Assign random roles to performances
    randomNumberGenerator = new Random(seed);
    ArrayList<Auditioner> unscheduledAuditioners = (ArrayList<Auditioner>) auditioners.clone();
    for(int auditionerIndex = 0; auditionerIndex < unscheduledAuditioners.size(); auditionerIndex++){
      Auditioner auditioner = unscheduledAuditioners.get(auditionerIndex);
      ArrayList<ArrayList<Role>> uniqueRoles = new ArrayList<ArrayList<Role>>();
      for(Role role : auditioner.roles){
        boolean foundMatch = false;
        for(ArrayList<Role> existingRoleArray : uniqueRoles){
          if(existingRoleArray.get(0).scene.getId().intValue() == role.scene.getId().intValue()){
            existingRoleArray.add(role);
            foundMatch = true;
            continue;
          }
        }
        if(!foundMatch){
          ArrayList<Role> newList = new ArrayList<Role>();
          newList.add(role);
          uniqueRoles.add(newList);
        }
      }
      int index = randomNumberGenerator.nextInt(uniqueRoles.size());
      ArrayList<Role> chosenRoleList = uniqueRoles.get(index);
      index = randomNumberGenerator.nextInt(chosenRoleList.size());
      Role chosenRole = chosenRoleList.get(index);
      int[] participantIds = new int[chosenRole.scene.getNumParticipants()];

      chosenRole.scene.incrementOccurrences();
      sceneRepository.save(chosenRole.scene);

      ArrayList<Role> finalizedAuditioners;
      if(chosenRole.associatedRoles != null){
        for(int associatedRoleIndex = 0; associatedRoleIndex < chosenRole.associatedRoles.size(); associatedRoleIndex ++){
          Role associatedRole = chosenRole.associatedRoles.get(associatedRoleIndex);

          participantIds[associatedRole.roleNumber - 1] = associatedRole.auditioner.actor.getId();

          if(associatedRole.auditioner != auditioner){
            if(unscheduledAuditioners.indexOf(associatedRole.auditioner) < auditionerIndex){
              auditionerIndex--;
            }
            unscheduledAuditioners.remove(associatedRole.auditioner);
          }
        }
        chosenRole.Delete();

        finalizedAuditioners = chosenRole.associatedRoles;
      }else{
        participantIds[0] = auditioner.actor.getId();
        finalizedAuditioners = new ArrayList<Role>();
        finalizedAuditioners.add(chosenRole);
      }

      // Delete occurences of this auditioner from other roles
      for(Auditioner otherAuditioner : auditioners){
        for(int roleIndex = 0; roleIndex < otherAuditioner.roles.size(); roleIndex++){
          Role role = otherAuditioner.roles.get(roleIndex);
          if(role.associatedRoles != null){
            for(Role associatedRole : role.associatedRoles){
              for(Role occupiedAuditioner : finalizedAuditioners){
                if(associatedRole.auditioner == occupiedAuditioner.auditioner){
                  roleIndex -= role.Delete();
                }
              }
            }
          }
        }
      }

      Performance newPerformance = new Performance(chosenRole.scene.getId(), GetTime());
      newPerformance.setParticipants("" + participantIds[0]);

      for(int i = 1; i < participantIds.length; i++){
        newPerformance.addParticipant("" + participantIds[i]);
      }
      newPerformance = performanceRepository.save(newPerformance);
      performanceStack.add(newPerformance);
    }


    System.out.println("Distributing votes...\n\n");

    ArrayList<User> users = new ArrayList<User>();
    for(User user : userRepository.findAll()){
      users.add(user);
    }

    Collections.shuffle(users, randomNumberGenerator);

    for(Performance performance : performanceStack){
      for(int i = 0; i < votesPerPerformance; i++){
        User nextVoter = users.get(0);
        for(User user : users){
          if(user.getVotingChances() < nextVoter.getVotingChances()){
            nextVoter = user;
          }
        }
        //public Vote(Integer performanceId, Integer voterId, Integer chosenOutcomeId, boolean inProgress, boolean hasChosenOutcome) {
        Vote newVote = new Vote(performance.getId(), nextVoter.getId(), -1, true, false);
        nextVoter.incrementVotingChances();

        voteRepository.save(newVote);
        userRepository.save(nextVoter);
      }
    }

    // for(Auditioner auditioner : auditioners)
    //   System.out.println(auditioner.toString());
    System.out.println("Done\n\n");
    return performanceStack;
  }

  @PostMapping(path="/simulation/test-requirement")
  public @ResponseBody Boolean TestRequirement(@RequestParam Integer actorId, @RequestParam Integer requirementId){
    Requirement r = requirementRepository.findById(requirementId).get();
    ArrayList<Auditioner> cast = new ArrayList<Auditioner>();
    cast.add(new Auditioner(actorRepository.findById(actorId).get()));
    return Scene.RequirementSatisfied(r, cast, statusRepository.findAll(), (int) GetTime());
  }
}