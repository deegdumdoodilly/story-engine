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
@RequestMapping(path="/api/flags")
public class FlagController {
  @Autowired
  private FlagRepository flagRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Flag> GetFlags (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer actorId){
    if(id == null){
      if(actorId == null){
        return flagRepository.findAll();
      }else{
        Iterable<Flag> allFlags = flagRepository.findAll();
        ArrayList<Flag> result = new ArrayList<Flag>();
        for(Flag f : allFlags){
          if(f.getActorId().intValue() == actorId){
            result.add(f);
          }
        }
        return result;
      }
    }else{
      if(!flagRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find flag with id " + id);
      }
      ArrayList<Flag> result = new ArrayList<Flag>();
      result.add(flagRepository.findById(id).get());
      return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Flag AddNewFlag (@RequestBody Flag newFlag) {
    for(Flag f : flagRepository.findAll()){
        if(f.getActorId().intValue() == newFlag.getActorId().intValue() && f.getKey().equals(newFlag.getKey())){
            f.setValue(newFlag.getValue());
            flagRepository.save(f);
            System.out.println(f.toString());
            return f;
        }
    }
    System.out.println(newFlag.toString());
    return flagRepository.save(newFlag);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveFlag (@RequestParam(required = false) Integer id, @RequestParam(required = false) Integer actorId) {
    if(id == null){
      if(actorId == null){
        for (Flag p : flagRepository.findAll()) {
          System.out.println("Deleting flag ID: " + p.getId());
          flagRepository.delete(p);
        }
        return "Flags removed.";
      }else{
        for (Flag p : flagRepository.findAll()) {
          if(p.getActorId().intValue() == actorId){
            System.out.println("Deleting flag ID: " + p.getId());
            flagRepository.delete(p);
          }
        }
        return "Flags removed.";
      }
    }else{
      if(!flagRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find flag with id " + id);
      }
      flagRepository.deleteById(id);
      System.out.println("Deleting flag ID: " + id);
      return "Flag removed.";
    }
  }
}
