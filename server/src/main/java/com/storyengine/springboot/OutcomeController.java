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
@RequestMapping(path="/api/outcomes")
public class OutcomeController {
  @Autowired
  private OutcomeRepository outcomeRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Outcome> GetOutcomes (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer sceneId){
    if(id == null){
      if(sceneId == null){
        return outcomeRepository.findAll();
      }else{
        Iterable<Outcome> allOutcomes = outcomeRepository.findAll();
        ArrayList<Outcome> result = new ArrayList<Outcome>();
        for(Outcome o : allOutcomes){
          if(o.getSceneId() == sceneId){
            result.add(o);
          }
        }
        return result;
      }
    }else{
      if(!outcomeRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find Outcome with id " + id);
      }
      ArrayList<Outcome> result = new ArrayList<Outcome>();
      result.add(outcomeRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Outcome AddNewOutcome (@RequestBody Outcome newOutcome) {
    System.out.println(newOutcome.toString());
    return outcomeRepository.save(newOutcome);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveOutcome (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer sceneId) {
    if(id == null){
      if(sceneId == null){
        for (Outcome o : outcomeRepository.findAll()) {
          System.out.println("Deleting Outcome ID: " + o.getId());
          outcomeRepository.delete(o);
        }
        return "Outcomes removed.";
      }else{
        for (Outcome o : outcomeRepository.findAll()) {
          if(o.getSceneId() == sceneId){
            System.out.println("Deleting Outcome ID: " + o.getId());
            outcomeRepository.delete(o);
          }
        }
        return "Outcomes removed.";
      }
    }else{
      if(!outcomeRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find Outcome with id " + id);
      }
      outcomeRepository.deleteById(id);
      System.out.println("Deleting Outcome ID: " + id);
      return "Outcome removed.";
    }
  }
}
