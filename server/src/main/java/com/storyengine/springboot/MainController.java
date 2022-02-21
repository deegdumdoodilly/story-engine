package com.storyengine.springboot;

import com.storyengine.springboot.User;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.PathVariable;

import org.springframework.web.bind.annotation.RestController;

@RestController 
@RequestMapping(path="/api") // This means URL's start with /api (after Application path)
public class MainController {
  @Autowired 
  private ActorRepository actorRepository;
  @Autowired
  private OutcomeRepository outcomeRepository;
  @Autowired
  private PerformanceRepository performanceRepository;
  @Autowired
  private RequirementRepository requirementRepository;
  @Autowired
  private SceneRepository sceneRepository;
  @Autowired
  private StatusRepository statusRepository;
  @Autowired 
  private UserRepository userRepository;
  @Autowired
  private VoteRepository voteRepository;

  @PostMapping(path="/user/add") // Map ONLY POST Requests
  public @ResponseBody String addNewUser (@RequestParam(required = false) String username
      , @RequestParam(required = false) String passhash
      , @RequestParam(required = false) Integer votingChances
      , @RequestParam(required = false) Integer positiveVotes
      , @RequestParam(required = false) Integer neutralVotes
      , @RequestParam(required = false) Integer negativeVotes
      , @RequestParam(required = false) Boolean validVoter) {
    User n = new User(username,passhash,votingChances, positiveVotes, neutralVotes, negativeVotes, validVoter);
    userRepository.save(n);
    return "Saved";
  }

  // Post Mappings

  @PostMapping(path="/vote") 
  public @ResponseBody Vote AddNewVote (@RequestBody Vote newVote) {
    return voteRepository.save(newVote);
  }

  @PostMapping(path="/vote/clear") 
  public @ResponseBody String ClearVotes () {
    voteRepository.deleteAll();
    return "Votes cleared.";
  }

  @PostMapping(path="/vote/in-progress") 
  public @ResponseBody Vote SetVoteInProgress (@RequestParam(required = false) Integer id, @RequestParam Boolean inProgress) {
    if(id == null){
      for(Vote vote : voteRepository.findAll()){
        vote.setInProgress(inProgress);
      }
      return null;
    }else{
      if(voteRepository.existsById(id)){
        voteRepository.findById(id).get().setInProgress(inProgress);
        return voteRepository.findById(id).get();
      }else{
        return null;
      }
    }
  }

  @PostMapping(path="/actor") 
  public @ResponseBody Actor AddNewActor (@RequestBody Actor newActor) {
    return actorRepository.save(newActor);
  }

  @PostMapping(path="/actor/{id}/set-environment") 
  public @ResponseBody Actor SetEnvironment (@PathVariable("id") Integer id, @RequestParam String environment) {
    if(actorRepository.existsById(id)){
      Actor actor = actorRepository.findById(id).get();
      actor.setEnvironment(environment);
      return actor;
    }else{
      return null;
    }
  }

  @PostMapping(path="/status") 
  public @ResponseBody Status AddNewStatus (@RequestBody Status newStatus) {
    return statusRepository.save(newStatus);
  }

  @PostMapping(path="/performance") 
  public @ResponseBody Performance AddNewPerformance (@RequestBody Performance newPerformance) {
    System.out.println(newPerformance.toString());
    return performanceRepository.save(newPerformance);
  }

  @PostMapping(path="/performance/clear") 
  public @ResponseBody String ClearPerformances () {
    performanceRepository.deleteAll();
    return "Performances cleared.";
  }

  @PostMapping(path="/performance/{id}/set-winning-vote") 
  public @ResponseBody Performance SetWinningVote (@PathVariable("id") Integer id, @RequestParam Integer voteId) {
    if(performanceRepository.existsById(id)){
      Performance performance = performanceRepository.findById(id).get();
      performance.setWinningVote(voteId);
      return performance;
    }else{
      return null;
    }
  }

  @PostMapping(path="/performance/in-progress") 
  public @ResponseBody Performance SetPerformanceInProgress (@RequestParam(required = false) Integer id, @RequestParam Boolean inProgress) {
    if(id == null){
      for(Performance performance : performanceRepository.findAll()){
        performance.setInProgress(inProgress);
      }
      return null;
    }else{
      if(performanceRepository.existsById(id)){
        performanceRepository.findById(id).get().setInProgress(inProgress);
        return performanceRepository.findById(id).get();
      }else{
        return null;
      }
    }
  }

  @PostMapping(path="/scene/reset") 
  public @ResponseBody String ResetSceneOccurrences () {
    for(Scene s : sceneRepository.findAll()){
      s.setOccurrences(0);
    }
    return "Occurrence count reset.";
  }

  @PostMapping(path="/scene") 
  public @ResponseBody Scene AddNewScene (@RequestBody Scene newScene) {
    if(sceneRepository.existsById(newScene.getId())){
      sceneRepository.findById(newScene.getId()).get().CopyFrom(newScene);
      return newScene;
    }else{
      return sceneRepository.save(newScene);
    }
  }

  @PostMapping(path="/scene/{id}/set-occurrences") 
  public @ResponseBody Scene AddNewScene (@PathVariable("id") Integer id, @RequestParam Integer occurrences) {
    if(sceneRepository.existsById(id)){
      Scene scene = sceneRepository.findById(id).get();
      scene.setOccurrences(occurrences);
      return scene;
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/set-voting-chances") 
  public @ResponseBody User SetVotingChances (@PathVariable("id") Integer id, @RequestParam Integer votingChances) {
    if(userRepository.existsById(id)){
      userRepository.findById(id).get().setVotingChances(votingChances);
      return userRepository.findById(id).get();
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/set-positive-votes") 
  public @ResponseBody User SetPositiveVotes (@PathVariable("id") Integer id, @RequestParam Integer numVotes) {
    if(userRepository.existsById(id)){
      userRepository.findById(id).get().setPositiveVotes(numVotes);
      return userRepository.findById(id).get();
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/set-negative-votes") 
  public @ResponseBody User SetNegativeVotes (@PathVariable("id") Integer id, @RequestParam Integer numVotes) {
    if(userRepository.existsById(id)){
      userRepository.findById(id).get().setNegativeVotes(numVotes);
      return userRepository.findById(id).get();
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/set-neutral-votes") 
  public @ResponseBody User SetNeutralVotes (@PathVariable("id") Integer id, @RequestParam Integer numVotes) {
    if(userRepository.existsById(id)){
      userRepository.findById(id).get().setNeutralVotes(numVotes);
      return userRepository.findById(id).get();
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/modify-positive-votes") 
  public @ResponseBody User ModifyPositiveVotes (@PathVariable("id") Integer id, @RequestParam Integer modification) {
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setPositiveVotes(user.getPositiveVotes() + modification);
      return user;
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/modify-negative-votes") 
  public @ResponseBody User ModifyNegativeVotes (@PathVariable("id") Integer id, @RequestParam Integer modification) {
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setNegativeVotes(user.getNegativeVotes() + modification);
      return user;
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/{id}/modify-neutral-votes") 
  public @ResponseBody User ModifyNeutralVotes (@PathVariable("id") Integer id, @RequestParam Integer modification) {
    if(userRepository.existsById(id)){
      User user = userRepository.findById(id).get();
      user.setNeutralVotes(user.getNeutralVotes() + modification);
      return user;
    }else{
      return null;
    }
  }

  @PostMapping(path="/user/reset") 
  public @ResponseBody String ResetUserVotes () {
    for(User u : userRepository.findAll()){
      u.setVotingChances(0);
      u.setPositiveVotes(0);
      u.setNeutralVotes(0);
      u.setNegativeVotes(0);
    }
    return "Occurrence count reset.";
  }

  // Get Mappings

  @GetMapping(path="/actor")
  public @ResponseBody Iterable<Actor> getAllActors() {
    return actorRepository.findAll();
  }

  @GetMapping(path="/outcome")
  public @ResponseBody Iterable<Outcome> getAllOutcomes() {
    return outcomeRepository.findAll();
  }

  @GetMapping(path="/performance")
  public @ResponseBody Iterable<Performance> getAllPerformances() {
    return performanceRepository.findAll();
  }

  @GetMapping(path="/requirement")
  public @ResponseBody Iterable<Requirement> getAllRequirements() {
    return requirementRepository.findAll();
  }

  @GetMapping(path="/scene")
  public @ResponseBody Iterable<Scene> getAllScenes() {
    return sceneRepository.findAll();
  }

  @GetMapping(path="/status")
  public @ResponseBody Iterable<Status> getAllStatuses() {
    return statusRepository.findAll();
  }

  @GetMapping(path="/user")
  public @ResponseBody Iterable<User> getAllUsers() {
    return userRepository.findAll();
  }

  @RequestMapping(path="/user/{id}")
  public @ResponseBody User getUser(@PathVariable("id") Integer id) {
    return userRepository.findById(id).get();
  }

  @GetMapping(path="/vote")
  public @ResponseBody Iterable<Vote> getAllVotes() {
    return voteRepository.findAll();
  }
}