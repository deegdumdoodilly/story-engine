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
@RequestMapping(path = "/api") // This means URL's start with /api (after Application path)
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
  private FlagRepository flagRepository;
  @Autowired
  private ActorRepository actorRepository;
  @Autowired
  private OutcomeRepository outcomeRepository;
  @Autowired
  private RequirementRepository requirementRepository;

  // Get Mappings

  @GetMapping(path = "/health")
  public @ResponseBody String GetHealth() {
    return "{\"status\":\"UP\"}";
  }

  @GetMapping(path = "/time")
  public @ResponseBody Integer GetTime() {
    return timeRepository.findById(1).get().getTime();
  }

  // Post Mappings

  @PostMapping(path = "/time/advance")
  public @ResponseBody String advanceTime() {
    Time t = timeRepository.findById(1).get();
    t.setTime(t.getTime() + 1);
    timeRepository.save(t);
    return "Time advanced";
  }

  @PostMapping(path = "/time/set")
  public @ResponseBody String setTime(@RequestBody Integer time) {
    Time t = timeRepository.findById(1).get();
    t.setTime(time);
    timeRepository.save(t);
    return "Time advanced";
  }

  @PostMapping(path = "/time/reset")
  public @ResponseBody String resetTime() {
    Time t = timeRepository.findById(1).get();
    t.setTime(0);
    timeRepository.save(t);
    return "Time reset";
  }

  @PostMapping(path = "/simulation/reset")
  public @ResponseBody String ResetSimulation() {
    for (Scene s : sceneRepository.findAll()) {
      s.setOccurrences(0);
      sceneRepository.save(s);
    }
    for (User u : userRepository.findAll()) {
      u.setVotingChances(0);
      u.setPositiveVotes(0);
      u.setNeutralVotes(0);
      u.setNegativeVotes(0);
      userRepository.save(u);
    }
    for (Actor a : actorRepository.findAll()) {
      a.setEnvironment("Woods");
      actorRepository.save(a);
    }
    Time t = timeRepository.findById(1).get();
    t.setTime(0);
    timeRepository.save(t);

    performanceRepository.deleteAll();
    voteRepository.deleteAll();
    statusRepository.deleteAll();
    flagRepository.deleteAll();

    System.out.println("Simulation reset\n\n");
    return "Simulation reset";
  }

  @PostMapping(path = "/simulation/assign-performances")
  public @ResponseBody Iterable<Performance> AssignPerformances(@RequestParam(required = false) Integer seed,
      @RequestParam(required = false) Integer votesPerPerformance) {
    Random randomNumberGenerator;
    if (seed == null) {
      randomNumberGenerator = new Random();
    } else {
      randomNumberGenerator = new Random(seed);
    }

    if (votesPerPerformance == null)
      votesPerPerformance = 1;

    // Will store the final set of new performances
    ArrayList<Performance> performanceStack = new ArrayList<Performance>();

    // Get a list of all actors and shuffle
    System.out.println("Assembling auditioners...\n\n");
    ArrayList<Auditioner> auditioners = new ArrayList<Auditioner>();
    for (Actor a : actorRepository.findAll()) {
      Auditioner newAuditioner = new Auditioner(a);
      auditioners.add(newAuditioner);
    }
    Collections.shuffle(auditioners, randomNumberGenerator);

    // Get a list of all requirements for later
    ArrayList<Requirement> requirementList = new ArrayList<Requirement>();
    for (Requirement req : requirementRepository.findAll()) {
      requirementList.add(req);
    }

    // For each auditioner:
    // - For each scene:
    // - If the scene is singleplayer and we meet the requirements, add it as a
    // possible Role.
    // - If the scene is multiplayer, check every permutation of auditioners in
    // other roles. If any meet the requirements, add them as possible Roles to all
    // involved auditioners.
    // - Filter out all but the highest priority roles

    // For each auditioner (in order of who has the highest priority roles):
    // Filter out all higher frequency events
    // Filter out all less complex events
    // - if it's multiplayer, make sure all associated Auditioners get signed off on
    // that event too
    System.out.println("Assembling all possible roles...\n\n");
    Iterable<Status> statusList = statusRepository.findAll();
    Iterable<Flag> flagList = flagRepository.findAll();
    for (Auditioner auditioner : auditioners) {
      for (Scene scene : sceneRepository.findAll()) {
        if (scene.IsSceneSingleplayer()) {
          if (scene.AllRequirementsSatisfied(auditioner, requirementList, statusList, flagList, GetTime())) {
            auditioner.roles.add(new Role(auditioner, scene, 1));
          }
        } else {
          auditioner.AddRolePermutations(scene, auditioners, requirementList, statusList, flagList, GetTime());
        }
      }
    }

    // for(Auditioner auditioner : auditioners)
    // System.out.println(auditioner.toString());

    System.out.println("Filtering by priority...\n\n");
    // Filter by priority
    for (Auditioner auditioner : auditioners) {
      for (Role role : auditioner.roles) {
        auditioner.highestPriority = Integer.max(auditioner.highestPriority, role.scene.getPriority());
      }

      for (int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex++) {
        Role role = auditioner.roles.get(roleIndex);
        if (role.scene.getPriority() < auditioner.highestPriority) {
          roleIndex -= role.Delete();
        }
      }
    }

    Collections.sort(auditioners);

    // for(Auditioner auditioner : auditioners)
    // System.out.println(auditioner.toString());

    System.out.println("Filtering by frequency...\n\n");
    // Filter out all higher frequency events
    for (Auditioner auditioner : auditioners) {
      for (Role role : auditioner.roles) {
        auditioner.lowestFrequency = Integer.min(auditioner.lowestFrequency, role.scene.getOccurrences());
      }

      for (int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex++) {
        Role role = auditioner.roles.get(roleIndex);
        if (role.scene.getOccurrences() > auditioner.lowestFrequency) {
          roleIndex -= role.Delete();
        }
      }
    }

    // for(Auditioner auditioner : auditioners)
    // System.out.println(auditioner.toString());
    System.out.println("Filtering parent scenes...\n\n");
    // Filter out all parent scenes
    for (Auditioner auditioner : auditioners) {
      auditioner.parentSceneIds = new ArrayList<Integer>();
      for (Role role : auditioner.roles) {
        Integer parentSceneId = role.scene.getParentSceneId();
        if (parentSceneId != null) {
          auditioner.parentSceneIds.add(parentSceneId);
        }
      }

      for (int roleIndex = 0; roleIndex < auditioner.roles.size(); roleIndex++) {
        Role role = auditioner.roles.get(roleIndex);
        if (auditioner.parentSceneIds.contains(role.scene.getId())) {
          roleIndex -= role.DeleteLineage(0, false);
        }
      }
    }

    Collections.sort(auditioners);

    // for(Auditioner auditioner : auditioners)
    // System.out.println(auditioner.toString());
    System.out.println("Selecting roles...\n\n");
    // Assign random roles to performances
    if (seed == null) {
      randomNumberGenerator = new Random();
    } else {
      randomNumberGenerator = new Random(seed);
    }
    ArrayList<Auditioner> unscheduledAuditioners = (ArrayList<Auditioner>) auditioners.clone();
    for (int auditionerIndex = 0; auditionerIndex < unscheduledAuditioners.size(); auditionerIndex++) {
      System.out.println("(" + (auditioners.size() - unscheduledAuditioners.size()) + "/" + auditioners.size() + ")\n");
      Auditioner auditioner = unscheduledAuditioners.get(auditionerIndex);
      ArrayList<ArrayList<Role>> uniqueRoles = new ArrayList<ArrayList<Role>>();
      for (Role role : auditioner.roles) {
        boolean foundMatch = false;
        for (ArrayList<Role> existingRoleArray : uniqueRoles) {
          if (existingRoleArray.get(0).scene.getId().intValue() == role.scene.getId().intValue()) {
            existingRoleArray.add(role);
            foundMatch = true;
            continue;
          }
        }
        if (!foundMatch) {
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
      if (chosenRole.associatedRoles != null) {
        for (int associatedRoleIndex = 0; associatedRoleIndex < chosenRole.associatedRoles
            .size(); associatedRoleIndex++) {
          Role associatedRole = chosenRole.associatedRoles.get(associatedRoleIndex);

          participantIds[associatedRole.roleNumber - 1] = associatedRole.auditioner.actor.getId();

          if (associatedRole.auditioner != auditioner) {
            if (unscheduledAuditioners.indexOf(associatedRole.auditioner) < auditionerIndex) {
              auditionerIndex--;
            }
            unscheduledAuditioners.remove(associatedRole.auditioner);
          }
        }
        chosenRole.Delete();

        finalizedAuditioners = chosenRole.associatedRoles;
      } else {
        participantIds[0] = auditioner.actor.getId();
        finalizedAuditioners = new ArrayList<Role>();
        finalizedAuditioners.add(chosenRole);
      }

      // Delete occurences of this auditioner from other roles
      for(Role finalizedRole : finalizedAuditioners){
        // Go through all the auditioner's roles except this one
        Auditioner hiredAuditioner = finalizedRole.auditioner;
        for(int i = 0; i < hiredAuditioner.roles.size(); i++){
          Role turnedDownRole = hiredAuditioner.roles.get(i);
          if(turnedDownRole != finalizedRole){
            i -= turnedDownRole.Delete();
          }
        }
      }

      Performance newPerformance = new Performance(chosenRole.scene.getId(), GetTime());
      newPerformance.setParticipants("" + participantIds[0]);

      for (int i = 1; i < participantIds.length; i++) {
        newPerformance.addParticipant("" + participantIds[i]);
      }
      newPerformance = performanceRepository.save(newPerformance);
      performanceStack.add(newPerformance);
    }

    System.out.println("Distributing votes...\n\n");

    ArrayList<User> users = new ArrayList<User>();
    for (User user : userRepository.findAll()) {
      users.add(user);
    }

    Collections.shuffle(users, randomNumberGenerator);

    for (Performance performance : performanceStack) {
      for (int i = 0; i < votesPerPerformance; i++) {
        User nextVoter = users.get(0);
        for (User user : users) {
          if (user.getVotingChances() < nextVoter.getVotingChances()) {
            nextVoter = user;
          }
        }
        // public Vote(Integer performanceId, Integer voterId, Integer chosenOutcomeId,
        // boolean inProgress, boolean hasChosenOutcome) {
        Vote newVote = new Vote(performance.getId(), nextVoter.getId(), GetTime(), -1, true, false);
        nextVoter.incrementVotingChances();

        voteRepository.save(newVote);
        userRepository.save(nextVoter);
      }
    }

    // for(Auditioner auditioner : auditioners)
    // System.out.println(auditioner.toString());
    System.out.println("Done\n\n");
    return performanceStack;
  }

  @PostMapping(path = "/simulation/execute")
  public @ResponseBody Iterable<Outcome> ExecutePerformances(@RequestParam(required = false) Integer performanceId,
      @RequestParam(required = false) Integer seed) {
    Random randomNumberGenerator;
    if (seed == null) {
      randomNumberGenerator = new Random();
    } else {
      randomNumberGenerator = new Random(seed);
    }
    ArrayList<Outcome> outcomes = new ArrayList<Outcome>();
    if (performanceId == null) {
      for (Performance p : performanceRepository.findAll()) {
        if (p.isInProgress()) {
          outcomes.add(ResolvePerformance(p, randomNumberGenerator));
        }
      }
      return outcomes;
    }

    if (!performanceRepository.existsById(performanceId)) {
      System.out.println("Performance not found: " + performanceId);
      return null;
    }

    Performance performance = performanceRepository.findById(performanceId).get();
    outcomes.add(ResolvePerformance(performance, randomNumberGenerator));

    return outcomes;
  }

  private Outcome ResolvePerformance(Performance performance, Random randomNumberGenerator) {
    // Check for existing votes, create one if insufficient exist
    // Update votes
    // Pick winning vote
    // Apply effects
    // Update voting history
    // Update performance
    // Save modified performance
    ArrayList<Outcome> outcomes = new ArrayList<Outcome>();
    for (Outcome o : outcomeRepository.findAll()) {
      if (o.getSceneId().intValue() == performance.getSceneId().intValue())
        outcomes.add(o);
    }

    ArrayList<Vote> completeVotes = new ArrayList<Vote>();
    ArrayList<Vote> incompleteVotes = new ArrayList<Vote>();
    for (Vote v : voteRepository.findAll()) {
      if (v.getPerformanceId().intValue() == performance.getId().intValue()) {
        v.setInProgress(false);
        voteRepository.save(v);

        if (userRepository.existsById(v.getVoterId())) {
          User user = userRepository.findById(v.getVoterId()).get();
          if(v.isHasChosenOutcome()){
            completeVotes.add(v);
            Outcome o = outcomeRepository.findById(v.getChosenOutcomeId()).get();
            switch (o.getType()) {
              case -1:
                user.setNegativeVotes(user.getNegativeVotes() + 1);
                break;
              case 0:
                user.setNeutralVotes(user.getNeutralVotes() + 1);
                break;
              case 1:
                user.setPositiveVotes(user.getPositiveVotes() + 1);
                break;
            }
            userRepository.save(user);
          }else{
            incompleteVotes.add(v);
          }
        }
      }
    }

    String[] castString = performance.getParticipants().split(",");
    Actor[] cast = new Actor[castString.length];
    for (Actor a : actorRepository.findAll()) {
      for (int i = 0; i < castString.length; i++) {
        if (castString[i].equals("" + a.getId())) {
          cast[i] = a;
        }
      }
    }

    if (completeVotes.size() == 0) {
      System.out.println("Insufficient votes");
      ArrayList<Outcome> neutralOutcomes = new ArrayList<Outcome>();
      for(Outcome o : outcomes){
        if(o.getType().intValue() == 0)
          neutralOutcomes.add(o);
      }
      System.out.println("Neutral options: " + neutralOutcomes.size());

      int randomOutcomeIndex = randomNumberGenerator.nextInt(neutralOutcomes.size());

      Outcome randomOutcome = neutralOutcomes.get(randomOutcomeIndex);
      System.out.println("Chosen outcome: " + randomOutcome.getId());
      
      Vote newVote = new Vote(performance.getId(), -1, GetTime(), randomOutcome.getId(), false, true);
      
      completeVotes.add(newVote);
    }
    System.out.println("Complete votes: " + completeVotes.size());

    int randomWinnerIndex = randomNumberGenerator.nextInt(completeVotes.size());

    System.out.println("Random index: " + randomWinnerIndex);

    Vote winningVote = completeVotes.get(randomWinnerIndex);
    
    winningVote.setIsWinningVote(true);
    voteRepository.save(winningVote);

    Outcome winningOutcome = null;
    for (Outcome o : outcomes) {
      if (o.getId().intValue() == winningVote.getChosenOutcomeId().intValue()) {
        winningOutcome = o;
        break;
      }
    }

    ArrayList<Status> deleteStatuses = new ArrayList<Status>();

    for (String effect : winningOutcome.getEffect().split(",")) {
      // Expecting a format like: 1 add status "status"
      //                      or: 1 set flag "this" "that"
      char[] charArray = effect.toCharArray();

      // Character reading logic
      String buffer = "";
      boolean inQuote = false;
      boolean escapeNextChar = false;

      // Token reading logic
      ArrayList<String> terms = new ArrayList<String>();
      // System.out.println(requirementText);
      for (int i = 0; i <= charArray.length; i++) {
        char character;
        if (i < charArray.length) {
          character = charArray[i];
        } else {
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
          terms.add(buffer);
          buffer = "";
        }
      }

      int characterNumber = Integer.parseInt(terms.get(0)) - 1;
      String function = terms.get(1) + " " + terms.get(2);
      String value = terms.get(3);

      Actor actor = cast[characterNumber];

      System.out.println("actorId " + actor.getId());

      boolean foundFlag;
      switch (function) {
        case "add status":
          Status newStatus = new Status(actor.getId(), value);
          boolean foundExistingStatus = false;
          for(Status s : statusRepository.findAll()){
            if(s.getActorId().intValue() == newStatus.getActorId().intValue() && s.getStatus().equals(newStatus.getStatus())){
              foundExistingStatus = true;
              break;
            }
          } 
          if(!foundExistingStatus){
            statusRepository.save(newStatus);
          }
          break;
        case "remove status":
          for (Status status : statusRepository.findAll()) {
            if (status.getActorId().intValue() == actor.getId() && status.getStatus().equals(value)) {
              deleteStatuses.add(status);
            }
          }
          break;
        case "set environment":
          actor.setEnvironment(value);
          actorRepository.save(actor);
          break;
        case "set flag":
          foundFlag = false;
          for (Flag flag : flagRepository.findAll()){
            if(flag.getActorId().intValue() == actor.getId().intValue() && flag.getKey().equals(value)){
              flag.setValue(terms.get(4));
              flagRepository.save(flag);
              foundFlag = true;
              break;
            }
          }
          // Flag was not found, make a new one
          if(!foundFlag){
            Flag newFlag = new Flag(-1, actor.getId(), value, terms.get(4));
            flagRepository.save(newFlag);
          }
          break;
        case "increase flag":
          foundFlag = false;
          for (Flag flag : flagRepository.findAll()){
            if(flag.getActorId().intValue() == actor.getId().intValue() && flag.getKey().equals(value)){
              int sum;
              try{
                sum = Integer.parseInt(flag.getValue()) + Integer.parseInt(terms.get(4));
              }catch(NumberFormatException e){
                throw new ResponseStatusException(HttpStatus.NOT_ACCEPTABLE, "Flag value/increment is not an integer");
              }
              flag.setValue("" + sum);
              flagRepository.save(flag);
              foundFlag = true;
              break;
            }
          }
          if(!foundFlag){
          // Flag was not found, throw error
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find flag with flagkey " + value);
          }
          break;
        case "decrease flag":
          foundFlag = false;
          for (Flag flag : flagRepository.findAll()){
            if(flag.getActorId().intValue() == actor.getId().intValue() && flag.getKey().equals(value)){
              int sum;
              try{
                sum = Integer.parseInt(flag.getValue()) - Integer.parseInt(terms.get(4));
              }catch(NumberFormatException e){
                throw new ResponseStatusException(HttpStatus.NOT_ACCEPTABLE, "Flag value/increment is not an integer");
              }
              flag.setValue("" + sum);
              flagRepository.save(flag);
              foundFlag = true;
              break;
            }
          }
          if(!foundFlag){
          // Flag was not found, throw error
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find flag with flagkey " + value);
          }
          break;
        case "remove flag":
          foundFlag = false;
          for (Flag flag : flagRepository.findAll()){
            if(flag.getActorId().intValue() == actor.getId().intValue() && flag.getKey().equals(value)){
              flagRepository.delete(flag);
              foundFlag = true;
              break;
            }
          }
          if(!foundFlag){
            // Flag was not found, throw error
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find flag with flagkey " + value);
          }
          break;
      }
    }

    statusRepository.deleteAll(deleteStatuses);

    performance.setInProgress(false);
    performance.setWinningVote(winningVote.getId());
    performanceRepository.save(performance);
    return winningOutcome;
  }

  @PostMapping(path = "/simulation/test-requirement")
  public @ResponseBody Boolean TestRequirement(@RequestParam Integer actorId, @RequestParam Integer requirementId) {
    Requirement r = requirementRepository.findById(requirementId).get();
    ArrayList<Auditioner> cast = new ArrayList<Auditioner>();
    cast.add(new Auditioner(actorRepository.findById(actorId).get()));
    return Scene.RequirementSatisfied(r, cast, statusRepository.findAll(), flagRepository.findAll(), (int) GetTime());
  }
}