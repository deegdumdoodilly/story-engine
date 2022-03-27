package com.storyengine.springboot;

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

import java.util.ArrayList;

@RestController
@RequestMapping(path="/api/performances")
public class PerformanceController {
  @Autowired
  private PerformanceRepository performanceRepository;
  @Autowired
  private RequirementRepository requirementRepository;
  @Autowired
  private TimeRepository timeRepository;
  @Autowired
  private SceneRepository sceneRepository;
  @Autowired
  private StatusRepository statusRepository;
  @Autowired
  private ActorRepository actorRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Performance> GetPerformances (@RequestParam(required = false) Integer id){
    if(id == null){
      return performanceRepository.findAll();
    }else{
      if(!performanceRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find performance with id " + id);
      }
      ArrayList<Performance> result = new ArrayList<Performance>();
      result.add(performanceRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Performance AddNewPerformance (@RequestBody Performance newPerformance) {
    if(newPerformance.getTime().intValue() == -1){
      newPerformance.setTime(timeRepository.findById(1).get().getTime());
    }
    System.out.println(newPerformance.toString());
    return performanceRepository.save(newPerformance);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemovePerformance (@RequestParam(required = false) Integer id) {
    if(id == null){
      for (Performance p : performanceRepository.findAll()) {
        System.out.println("Deleting performance ID: " + p.getId());
        performanceRepository.delete(p);
      }
      return "Performances removed.";
    }else{
      if(!performanceRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find performance with id " + id);
      }
      performanceRepository.deleteById(id);
      System.out.println("Deleting performance ID: " + id);
      return "Performance removed.";
    }
  }

  // (ArrayList<Auditioner> cast, ArrayList<Requirement> requirementList, Iterable<Status> statusList, int currentTime)
  @PostMapping(path="/validate")
  public @ResponseBody String ValidatePerformance (@RequestBody Performance performance){
    Scene scene = sceneRepository.findById(performance.getSceneId()).get();
    ArrayList<Auditioner> cast = new ArrayList<Auditioner>();
    for(String c : performance.getParticipants().split(",")){
      cast.add(new Auditioner(actorRepository.findById(Integer.parseInt(c)).get()));
    }

    ArrayList<Requirement> requirements = new ArrayList<Requirement>();
    for(Requirement r : requirementRepository.findAll()){
      System.out.println("Scanning requirement " + r.getId());
      if(r.getSceneId().intValue() == scene.getId().intValue()){
        System.out.println("Addint it!");
        requirements.add(r);
      }
    }

    if(scene.AllRequirementsSatisfied(cast, requirements, statusRepository.findAll(), timeRepository.findById(1).get().getTime())){
      return "{\"result\":\"valid\"}";
    }else{
      return "{\"result\":\"invalid\"}";
    }
  }

  @PostMapping(path="/set-winning-vote") 
  public @ResponseBody Performance SetWinningVote (@RequestParam Integer id, @RequestBody String voteId) {
    int vId = -1;
    try{
      vId = Integer.parseInt(voteId);
    }catch (NumberFormatException e){
      System.out.println(e.getClass().getName() + "\n" + e.getStackTrace());
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse integer from '" + voteId + "'");
    }
    if(performanceRepository.existsById(id)){
      Performance performance = performanceRepository.findById(id).get();
      performance.setWinningVote(vId);
      performanceRepository.save(performance);
      return performance;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find performance with id '" + id + "'");
    }
  }

  @PostMapping(path="/set-in-progress") 
  public @ResponseBody Performance SetPerformanceInProgress (@RequestParam(required = false) Integer id, @RequestBody String inProgress) {
    boolean inProg = false;
    if(inProgress.toLowerCase().equals("true")){
      inProg = true;
    }else if(!inProgress.toLowerCase().equals("false")){
      System.out.println("Unable to parse boolean");
      throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Unable to parse boolean from '" + inProgress + "'', must be 'true' or 'false'.");
    }
    if(id == null){
      for(Performance performance : performanceRepository.findAll()){
        performance.setInProgress(inProg);
        performanceRepository.save(performance);
      }
      return null;
    }else{
      if(performanceRepository.existsById(id)){
        Performance performance = performanceRepository.findById(id).get();
        performance.setInProgress(inProg);
        performanceRepository.save(performance);
        return performanceRepository.findById(id).get();
      }else{
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find performance with id " + id);
      }
    }
  }
}
