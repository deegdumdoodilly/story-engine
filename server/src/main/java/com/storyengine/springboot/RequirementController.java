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
@RequestMapping(path="/api/requirements")
public class RequirementController {
  @Autowired
  private RequirementRepository requirementRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Requirement> GetRequirements (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer sceneId){
    if(id == null){
      if(sceneId == null){
        return requirementRepository.findAll();
      }else{
        Iterable<Requirement> allRequirements = requirementRepository.findAll();
        ArrayList<Requirement> result = new ArrayList<Requirement>();
        for(Requirement o : allRequirements){
          if(o.getSceneId() == sceneId){
            result.add(o);
          }
        }
        return result;
      }
    }else{
      if(!requirementRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find Requirement with id " + id);
      }
      ArrayList<Requirement> result = new ArrayList<Requirement>();
      result.add(requirementRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Requirement AddNewRequirement (@RequestBody Requirement newRequirement) {
    System.out.println(newRequirement.toString());
    return requirementRepository.save(newRequirement);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveRequirement (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer sceneId) {
    if(id == null){
      if(sceneId == null){
        for (Requirement r : requirementRepository.findAll()) {
          System.out.println("Deleting Requirement ID: " + r.getId());
          requirementRepository.delete(r);
        }
        return "Requirements removed.";
      }else{
        for (Requirement p : requirementRepository.findAll()) {
          System.out.println("Deleting Requirement ID: " + p.getId());
          requirementRepository.delete(p);
        }
        return "Requirements removed.";
      }
    }else{
      if(!requirementRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find Requirement with id " + id);
      }
      requirementRepository.deleteById(id);
      System.out.println("Deleting Requirement ID: " + id);
      return "Requirement removed.";
    }
  }
}
