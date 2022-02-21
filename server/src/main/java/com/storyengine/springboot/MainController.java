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

  @PostMapping(path="/actor") 
  public @ResponseBody Actor AddNewActor (@RequestBody Actor newActor) {
    return actorRepository.save(newActor);
  }

  @PostMapping(path="/status") 
  public @ResponseBody Status AddNewStatus (@RequestBody Status newStatus) {
    return statusRepository.save(newStatus);
  }

  @PostMapping(path="/performance") 
  public @ResponseBody Performance AddNewPerformance (@RequestBody Performance newPerformance) {
    return performanceRepository.save(newPerformance);
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