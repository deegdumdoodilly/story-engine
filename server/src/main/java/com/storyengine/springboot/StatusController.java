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
@RequestMapping(path="/api/statuses")
public class StatusController {
  @Autowired
  private StatusRepository statusRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Status> GetStatuses (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer actorId){
    if(id == null){
      if(actorId == null){
        return statusRepository.findAll();
      }else{
        Iterable<Status> allStatuses = statusRepository.findAll();
        ArrayList<Status> result = new ArrayList<Status>();
        for(Status s : allStatuses){
          if(s.getActorId().intValue() == actorId){
            result.add(s);
          }
        }
        return result;
      }
    }else{
      if(!statusRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find status with id " + id);
      }
      ArrayList<Status> result = new ArrayList<Status>();
      result.add(statusRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Status AddNewStatus (@RequestBody Status newStatus) {
    for(Status s : statusRepository.findAll()){
      if(s.getActorId().intValue() == newStatus.getActorId().intValue() && s.getStatus().equals(newStatus.getStatus())){
        System.out.println(s.toString());
        return s;
      }
    } 
    System.out.println(newStatus.toString());
    return statusRepository.save(newStatus);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveStatus (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer actorId) {
    if(id == null){
      if(actorId == null){
        for (Status p : statusRepository.findAll()) {
          System.out.println("Deleting status ID: " + p.getId());
          statusRepository.delete(p);
        }
        return "Statuses removed.";
      }else{
        for (Status p : statusRepository.findAll()) {
          if(p.getActorId().intValue() == actorId){
            System.out.println("Deleting status ID: " + p.getId());
            statusRepository.delete(p);
          }
        }
        return "Statuses removed.";
      }
    }else{
      if(!statusRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find status with id " + id);
      }
      statusRepository.deleteById(id);
      System.out.println("Deleting status ID: " + id);
      return "Status removed.";
    }
  }
}
