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
@RequestMapping(path="/api/actors")
public class ActorController {
  @Autowired 
  public ActorRepository actorRepository;

  @GetMapping(path="")
  public @ResponseBody Iterable<Actor> getAllActors(@RequestParam(required = false) Integer id) {
    if(id == null){
        return actorRepository.findAll();
    }else{
        if(!actorRepository.existsById(id)){
          throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find actor with id " + id);
        }
        ArrayList<Actor> result = new ArrayList<Actor>();
        result.add(actorRepository.findById(id).get());
        return result;
    }
  }

  @PostMapping(path="/add") 
  public @ResponseBody Actor AddNewActor (@RequestBody Actor newActor) {
    return actorRepository.save(newActor);
  }

  @PostMapping(path="/remove") 
  public @ResponseBody String RemoveActor (@RequestParam(required = false) Integer id) {
    if(id == null){
      for (Actor p : actorRepository.findAll()) {
        System.out.println("Deleting actor ID: " + p.getId());
        actorRepository.delete(p);
      }
      return "Actors removed.";
    }else{
      if(!actorRepository.existsById(id)){
        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Could not find actor with id " + id);
      }
      actorRepository.deleteById(id);
      return "Removed actor";
    }
  }

  @PostMapping(path="/set-environment") 
  public @ResponseBody Actor SetEnvironment (@RequestParam(required = false) Integer id, @RequestBody String environment) {
    if(id == null){
      for(Actor a : actorRepository.findAll()){
        a.setEnvironment(environment);
        actorRepository.save(a);
      }
      return null;
    }
    if(actorRepository.existsById(id)){
      Actor actor = actorRepository.findById(id).get();
      actor.setEnvironment(environment);
      actorRepository.save(actor);
      return actor;
    }else{
      throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Unable to find actor with id '" + id + "'");
    }
  }
}
